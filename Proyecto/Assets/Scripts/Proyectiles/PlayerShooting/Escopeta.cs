using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escopeta : MonoBehaviour {

    public AudioClip sound;
    AudioSource source;

    //Disparo de la escopeta
    public int n;
    float tiempo;
    public float cadencia;
    //public Bala bala;
    public BalaEscopeta bala;
    int angg = 20;
    float anguloN;
    Vector3 v;
    GameObject pool;

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

        if (Input.GetMouseButtonDown(0) && GameManager.instance.ammo > 0 && tiempo >= cadencia)
        {
            for (int i = 0; i < n; i++)
            {
                float angle = 0.0f;
                Vector3 axis = Vector3.forward;
                transform.rotation.ToAngleAxis(out angle, out axis);
                anguloN = (2 * angg) / (n - 1);   //Dividimos el ángulo total entre las balas de la escopeta                
                v = ((angle - angg) + (anguloN * i)) * axis;
                Quaternion rotation = Quaternion.Euler(v);  //Es igual que antes, solo que ahora valora tb los ejes sacados del ToAngleAxis y va mejor
                BalaEscopeta balaNueva = Instantiate<BalaEscopeta>(bala, transform.position, rotation, pool.transform);
            }
            source.clip = sound;
            source.Play();
            tiempo = 0;
            GameManager.instance.CambiaMunicion(-1);
        }
    }
//    IEnumerator Ej()
//    {
//        print(Time.time);
//        yield return new WaitForSecondsRealtime(50);
//        print(Time.time);
//    }
  }
