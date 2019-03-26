using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public int speed;
    Rigidbody2D rb2D;
    Vector2 disparo;
    public int damage;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        disparo = transform.up * speed;
        rb2D.velocity = -disparo;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health enemigo = collision.gameObject.GetComponent<Health>();
        
        //Si el gameObject con el que colisionan tiene salud, le causa daño a no ser que sea el nexo 
        if (enemigo != null && collision.gameObject.tag != "Nexus" ) enemigo.RecieveDamage(damage);
        
        //Al colisionar con cualquiero objeto, las balas se destruyen
        Destroy(this.gameObject);
    }
}
