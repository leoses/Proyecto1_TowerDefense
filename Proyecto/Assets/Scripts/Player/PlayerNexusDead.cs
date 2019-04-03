using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNexusDead : MonoBehaviour {

    public GameObject respawn;
    PlayerMovement player;

    void Start()
    {
        player = gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            GameManager.FindPlayer(this);
        }

        /*else
        {
            GameManager.FindNexus(this);
    
        }*/
    }

    public void PlayerRespawn()
    {
        gameObject.transform.position = respawn.transform.position;// llevamos al jugador a la posición inicial

        // GameManager.instance.Penalizacion(penalizacionSeg,penalizacionDin)
    }

    public void NexusDestroy()
    {
        Destroy(gameObject);
    }
}
