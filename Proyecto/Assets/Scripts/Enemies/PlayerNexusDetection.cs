﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNexusDetection : MonoBehaviour
{
    Enemy enemy;
    Ghost ghost;
    PlayerMovement player;
    DistanceShooting distanceEnemy;
    bool bandera = true;
    bool chase = false;

    private void Start()
    {
        ghost = gameObject.GetComponentInParent<Ghost>();
        enemy = gameObject.GetComponentInParent<Enemy>();
        distanceEnemy = gameObject.GetComponentInChildren<DistanceShooting>();
    }

    //Cuando el jugador entre dentro del rango de ataque del enemigo, se parará 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerMovement>();

        if (ghost != null && player != null)
        {
            ghost.Chase();
        }

        else
        {
            enemy.StopEnemy(collision.CompareTag("Nexus"));
            if (distanceEnemy != null && bandera)
            {
                distanceEnemy.ChangeBool(true);
                bandera = false;
            }
        }
    }

    //Al salir del radio de ataque del enemigo, este comenzará a moverse con la dirección que
    //llevaba antes de pararse
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!chase)
            enemy.NewDir(Vector2.zero);
    }

    //Método para cambiar la rotación del area del ataque físico
    public void ChangeRotation(float rotation)
    {
        transform.Rotate(0, 0, rotation);
    }
}