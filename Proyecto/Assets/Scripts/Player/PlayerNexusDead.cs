using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNexusDead : MonoBehaviour
{
    public AudioClip sound;
    AudioSource source;

    Transform respawn;
    PlayerMovement player;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        respawn = GameObject.FindWithTag("Spawn").transform;

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
        source.clip = sound;
        source.Play();
        gameObject.transform.position = respawn.transform.position;// llevamos al jugador a la posición inicial
    }

    public void NexusDestroy()
    {
        Destroy(gameObject);
    }
}