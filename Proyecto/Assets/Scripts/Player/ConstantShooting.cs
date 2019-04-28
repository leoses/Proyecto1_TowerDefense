using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantShooting : MonoBehaviour {
    public AudioClip sound;
    AudioSource source;

    //Disparo de la pistola
    public int damage;
    public float cadencia = 1;
    public Bullet bala;
    private GameObject pool;
    private Animator animator;
    private float n= 1;
    // Use this for initialization
    void Start ()
    {
       
        
        source = gameObject.GetComponentInParent<AudioSource>();
        pool = GameObject.FindGameObjectWithTag("bulletpool");
        animator = this.transform.parent.GetComponentInChildren<Animator>();
        //InvokeRepeating("Shooting", 0, n);
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        //while(Input.GetMouseButtonDown(0) && )
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            n = cadencia;
            InvokeRepeating("Shooting", 0, n);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke();
        }
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
