using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNexusDead : MonoBehaviour {

    Transform respawn;
    PlayerMovement player;

    private void Awake()
    {
        respawn = GameObject.FindWithTag("Spawn").transform;
    }

    void Start()
    {
        player = gameObject.GetComponent<PlayerMovement>();
        if (player == null)
        {
            GameManager.FindNexus(this);
        }

        else
        {
            GameManager.FindPlayer(this);
    
        }
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
