using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNexusDetection : MonoBehaviour {
    //Vector que guardará la dirección que lleva el enemigo antes de detectar al jugador
    Vector2 guardaMov;
    Enemy enemigo;
    Ghost ghost;
    PlayerMovement player;
    DistanceShooting distanceEnemy;
    bool bandera = true;
    bool chase = false;
    private void Start()
    {
        ghost = gameObject.GetComponentInParent<Ghost>();
        enemigo = gameObject.GetComponentInParent<Enemy>();
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
            guardaMov = enemigo.StopEnemy();
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
            enemigo.NewDir(guardaMov);
    }

    //Método para cambiar la rotación del area del ataque físico
    public void ChangeRotation(float rotation)
    {
        transform.Rotate(0, 0, rotation);
    }
}
