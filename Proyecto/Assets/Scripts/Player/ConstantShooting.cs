using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantShooting : MonoBehaviour {
    public AudioClip sound;
    AudioSource source;

    //Disparo de la pistola
    public int damage;
    public float cadencia;
    public Bullet bala;
    private GameObject pool;
    private Animator animator;
    private float time = 0;

    // Use this for initialization
    void Start ()
    {
        source = gameObject.GetComponentInParent<AudioSource>();
        pool = GameObject.FindGameObjectWithTag("bulletpool");
        animator = this.transform.parent.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Si el jugador clickea
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
            time = 0;
        }
        //Si el jugador mantiene pulsado
        else if (Input.GetButton("Fire1") && time<cadencia)
        {
            time += Time.deltaTime;
            if (time > cadencia) Shooting();
            
        }
       else
            time = 0;
        
	}

    void Shooting()
    {
        animator.SetTrigger("Shoot");
        Bullet balaNueva = Instantiate<Bullet>(bala, transform.position, transform.rotation, pool.transform);
        balaNueva.newValues(-transform.up, damage);
        source.clip = sound;
        source.Play();
    }
}
