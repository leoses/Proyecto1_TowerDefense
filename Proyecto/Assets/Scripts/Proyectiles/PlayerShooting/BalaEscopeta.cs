using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscopeta : MonoBehaviour
{

    public int speed;
    Rigidbody2D rb2D;
    Vector2 disparo, angulo;
    public int damage;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        disparo = transform.up * speed;
        Debug.Log("disparo: " + disparo);
        rb2D.velocity = -disparo;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health enemigo = collision.gameObject.GetComponent<Health>();
        Shield shield = collision.gameObject.GetComponent<Shield>();

        //Si el gameObject con el que colisionan tiene salud, le causa daño a no ser que sea o el Escudo o el nexo
        if (enemigo != null && shield == null && collision.gameObject.tag != "Nexus") enemigo.RecieveDamage(damage);

        ////Al colisionar con cualquiero objeto, las balas se destruyen
        //Destroy(this.gameObject);

        if (collision.gameObject.tag == "Muro")     //Para que solo lo tenga en cuenta cuando se trate de la escopeta
        {
            ContactPoint2D puntoColision = collision.GetContact(0);
            //Debug.Log("normal: " + puntoColision.normal);
            //Debug.Log("tangente: " + puntoColision.tangentImpulse);
            if (puntoColision.normal.x == 0)
            {
                if (puntoColision.normal.y > 0)    //Normal: (0, 1)
                {
                    if (puntoColision.tangentImpulse > 0)
                        angulo = new Vector2(-1f, 1f);
                    else
                        angulo = new Vector2(1f, 1f);
                }
                else    //Normal: (0, -1)
                {
                    if (puntoColision.tangentImpulse > 0)
                        angulo = new Vector2(1f, -1f);
                    else
                        angulo = new Vector2(-1f, -1f);
                }
            }
            else
            {
                if (puntoColision.normal.x > 0)    //Normal: (1, 0)
                {
                    if (puntoColision.tangentImpulse > 0)
                        angulo = new Vector2(1f, 1f);
                    else
                        angulo = new Vector2(1f, -1f);
                }
                else    //Normal: (-1, 0)
                {
                    if (puntoColision.tangentImpulse > 0)
                        angulo = new Vector2(-1f, -1f);
                    else
                        angulo = new Vector2(-1f, 1f);
                }
            }
            Vector2 rebote = (angulo * speed);
            //Debug.Log("rebote: " + rebote);
            rb2D.velocity = rebote;
        }
    }
}
