﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int speed;
    int damage;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    //Cuando se instancia la bala se invoca este método con el vector dirección que debe seguir
    public void newValues(Vector2 dir, int dam)
    {
        transform.right = dir;
        damage = dam;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //No colisiona con torretas (se destruiría inmediatamente)
        if (collision.gameObject.GetComponent<TurretShooting>() == null)
            Destroy(gameObject);

        //Comprobamos si el objeto con el que ha colisionado la bala tiene algún script de salud
        //Si es así, causamos daño a dicho objeto
        Health damagedObject = collision.gameObject.GetComponent<Health>();
        if (damagedObject != null) damagedObject.RecieveDamage(damage);

        if (collision.gameObject.CompareTag("Nexus")) GameManager.instance.PierdeVidaNucleo(damage);

    }
}