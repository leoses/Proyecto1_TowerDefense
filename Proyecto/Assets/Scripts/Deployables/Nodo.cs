using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Nodo : MonoBehaviour {

    public Vector2 dir1; // Primera direccion a cambiar con una probabilidad de p1
    public int p1; //p1 = 1 <=> 10% de probabilidad de cambio;
    public Vector2 dir2;//Segunda direccion que toma en caso de que no coja la primera

    static System.Random rnd = new System.Random();

    private void Start()
    {
        if (dir1.x % 1 != 0 || dir1.y % 1 != 0 || dir2.x % 1 != 0 || dir2.y % 1 != 0)
        {
            Debug.LogError("POR FAVOR, en los vectores poned: -1, 0 o 1. Gracias");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (rnd.Next(0, 11) < p1 && collision.gameObject.GetComponent<Enemy>())
            {
                enemy.Rotation(dir1);
                enemy.NewDir(dir1);
            }

            else
            {
                enemy.Rotation(dir2);
                enemy.NewDir(dir2);
            }
        }
    }
}
