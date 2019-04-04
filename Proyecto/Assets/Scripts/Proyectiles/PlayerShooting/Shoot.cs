using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public AudioClip sound;
    AudioSource source;

    //Disparo de la pistola
    float tiempo;
    public float cadencia;
    public Bala bala;
    private GameObject pool;

    private void Start()
    {
        source = gameObject.GetComponentInParent<AudioSource>();
        pool = GameObject.FindGameObjectWithTag("bulletpool");
        tiempo = cadencia;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && tiempo >= cadencia)
        {
            Bala balaNueva = Instantiate<Bala>(bala, transform.position, transform.rotation, pool.transform);
            tiempo = 0;
            source.clip = sound;
            source.Play();
        }
    }
}
