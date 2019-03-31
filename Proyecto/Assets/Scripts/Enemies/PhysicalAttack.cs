using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttack : MonoBehaviour {

    PlayerNexusDead planux;
    PlayerMovement player;
    //Tiempo que tarda en realizar un ataque
    public float seconds;
    //Daño (podría ser una opción tener 2, una para el jugador y otra para el nucleo)
    public int damage;
    private float time;
    //Los atacantes fisicos hacen daño tanto al jugador como al nucleo
    //Health nexus, player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        //nexus = GameObject.FindGameObjectWithTag("Nexus").GetComponent<Health>();
    }

    //Puesto que este trigger solo detecta colisiones con el jugador y con el nexo (mirar matriz de colisiones), solo hace comprobar cual de los dos es para
    //saber a que tiene que causar daño
    private void OnTriggerEnter2D(Collider2D collision)
    {

        planux = collision.gameObject.GetComponent<PlayerNexusDead>();

        //Debug.Log(collision.gameObject.name);
        if (planux != null)
        {
            if (player.gameObject == collision.gameObject)
            {
                GameManager.instance.PierdeVidaJugador(damage);
            }

            else
            {
                GameManager.instance.PierdeVidaNucleo(damage);
            }
        }

        else if (collision.gameObject.CompareTag("Barricade"))
        {
            collision.GetComponent<Health>().RecieveDamage(damage);
        }

        time = 0;
    }

    //Cuando el jugador (o el núcleo) se mantiene un determinado número de segundos dentro del rango de ataque del enemigo
    //este recibe daño
    private void OnTriggerStay2D(Collider2D collision)
    {
        time += Time.fixedDeltaTime;
        
        if (time >= seconds)
        {
            if (planux != null)
            {
                if (player.gameObject == collision.gameObject)
                {
                    GameManager.instance.PierdeVidaJugador(damage);
                }

                else
                {
                    GameManager.instance.PierdeVidaNucleo(damage);
                }
            }

            else if (collision.gameObject.CompareTag("Barricade"))
            {
                collision.GetComponent<Health>().RecieveDamage(damage);
            }
            time = 0;
        }
    }
}
