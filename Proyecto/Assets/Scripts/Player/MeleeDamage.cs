using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour {

    public AudioClip sound;
    AudioSource source;
    public int damage = 50;
    Health found;

    private void Start()
    {
        source = gameObject.GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        found = collision.gameObject.GetComponent<Health>();

        //Si colisiona con un enemigo le daña
        if (found != null)
        {
            found.RecieveDamage(damage);
            source.clip = sound;
            source.Play();
        }
    }
}