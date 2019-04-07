using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escopeta : MonoBehaviour
{

    public AudioClip sound;
    AudioSource source;

    //Disparo de la escopeta
    public int n;
    float tiempo;
    public float cadencia;
    //public Bala bala;
    public BalaEscopeta bala;
    int angle = 20;
    float anguloN;
    Vector3 v;
    Transform pool;

    private void Start()
    {
        source = gameObject.GetComponentInParent<AudioSource>();
        pool = GameObject.FindGameObjectWithTag("bulletpool").transform;
        tiempo = cadencia;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            tiempo += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && GameManager.instance.ammo > 0 && tiempo >= cadencia)
            {
                for (int i = 0; i < n; i++)
                {
                    float angle_ = 0.0f;
                    Vector3 axis = Vector3.forward;
                    transform.rotation.ToAngleAxis(out angle_, out axis);
                    anguloN = (2 * angle) / (n - 1);   //Dividimos el ángulo total entre las balas de la escopeta                
                    v = ((angle_ - angle) + (anguloN * i)) * axis;
                    Quaternion rotation = Quaternion.Euler(v);  //Es igual que antes, solo que ahora valora tb los ejes sacados del ToAngleAxis y va mejor
                    Instantiate<BalaEscopeta>(bala, transform.position, rotation, pool.transform);
                }
                source.clip = sound;
                source.Play();
                tiempo = 0;
                GameManager.instance.CambiaMunicion(-1);
            }
        }
    }
}