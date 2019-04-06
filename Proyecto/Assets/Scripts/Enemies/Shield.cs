using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public AudioClip sound;
    AudioSource source;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BalaEscopeta esc = collision.gameObject.GetComponent<BalaEscopeta>();

        if (esc != null)
        {
            source.clip = sound;
            source.Play();
        }
    }
}
