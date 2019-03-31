﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    //Disparo de la pistola
    public int damage;
    float tiempo;
    public float cadencia;
    public Bullet bala;
    private GameObject pool;

    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("bulletpool");
        tiempo = cadencia;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            tiempo += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && tiempo >= cadencia)
            {
                Bullet balaNueva = Instantiate<Bullet>(bala, transform.position, transform.rotation, pool.transform);
                balaNueva.newValues(-transform.up, damage);
                tiempo = 0;
            }
        }
    }
}
