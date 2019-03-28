﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour {

    public Bullet bulletPref;
    Collider2D[] contacts;
    Transform target = null;
    Vector2 dir;
    float time = 1;
    public float fireRate = 1;
    public int damage = 35; //<-------------

    void Update()
    {
        if (target != null) //Si existe objetivo encuentra el vector dirección
            dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);

        if (target != null && dir.magnitude <= GameManager.turretRange + 0.5)
        //El +0.5 soluciona el problema que se plantea al usar el centro del objeto para calcular la magnitud del vector dirección
        //Cuando el método OverlapCircleAll colisiona con cualquier parte del box collider (a cierta distancia no coinciden)
        {
            time = time + Time.deltaTime;

            if (time >= fireRate) //Cuenta el tiempo que pasa usando los deltaTimes y cuando se llega a la cantidad de tiempo fireRate se reinicia
            {
                dir = dir.normalized; //Normaliza el vector para que su magnitud no influya en la velocidad
                Bullet newBullet = Instantiate(bulletPref, transform.position, Quaternion.identity, transform);
                time = 0;
                newBullet.newValues(dir, damage); //Se manda a la bala instanciada la diracción en la que debe moverse
            }
        }

        else
        {
            //Se castea un círculo con un radio dado que guarda en el vector contacts los Colliders de los objetos encontrados en orden en la layer indicada
            contacts = Physics2D.OverlapCircleAll(transform.position, GameManager.turretRange, 256, -1, 1);

            //Si existe algún elemento se queda con el más cercano y usa su transform para conseguir la dirección dir
            if (contacts.Length > 0)
                target = contacts[0].transform;
        }
    }

    public void TurretUpdate(int impDam, float impRate)
    {
        damage = impDam;
        fireRate = impRate;
    }
}