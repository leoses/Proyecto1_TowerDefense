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
    float time;
    //Los atacantes fisicos hacen daño tanto al jugador como al nucleo
    //Health nexus, player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        //nexus = GameObject.FindGameObjectWithTag("Nexus").GetComponent<Health>();
    }

    private void Update()
    {
        if (time < seconds)
            time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        planux = collision.gameObject.GetComponent<PlayerNexusDead>();
    }

    //Cuando el jugador (o el núcleo) se mantiene un determinado número de segundos dentro del rango de ataque del enemigo
    //este recibe daño
    private void OnTriggerStay2D(Collider2D collision)
    {
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
