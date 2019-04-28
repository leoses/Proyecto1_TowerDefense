using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{

    PlayerMovement player;
    public float healPor = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            healPor = healPor / 100;
            healPor = GameManager.instance.vidaMax * healPor;
            GameManager.instance.PierdeVidaJugador(-healPor, true);
            if(GameManager.instance.vidaJugador > GameManager.instance.vidaMax)
            {
                GameManager.instance.vidaJugador = GameManager.instance.vidaMax;
            }
            Destroy(gameObject);
        }
    }
}