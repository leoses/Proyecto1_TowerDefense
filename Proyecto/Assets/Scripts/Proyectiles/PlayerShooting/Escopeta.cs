using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escopeta : MonoBehaviour {

    //Disparo de la escopeta
    public int n;
    public float vida;
    float tiempo;
    public float cadencia;
    //public Bala bala;
    public BalaEscopeta bala;
    public int angg;
    float anguloN;
    Vector3 v;
    GameObject pool;

    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("bulletpool");
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
                    float angle = 0.0f;
                    Vector3 axis = Vector3.forward;
                    transform.rotation.ToAngleAxis(out angle, out axis);
                    anguloN = (2 * angg) / (n - 1);   //Dividimos el ángulo total entre las balas de la escopeta                
                    v = ((angle - angg) + (anguloN * i)) * axis;
                    //Vector3 v = (angle + Random.Range(-angg, angg)) * axis;
                    //Quaternion rotation = Quaternion.Euler(0, 0, angle + Random.Range(-angg,angg));
                    Quaternion rotation = Quaternion.Euler(v);  //Es igual que antes, solo que ahora valora tb los ejes sacados del ToAngleAxis y va mejor
                    Debug.Log(angle);
                    Debug.Log(transform.position + " *");
                    // Bala balaNueva = Instantiate<Bala>(bala, transform.position, rotation);
                    BalaEscopeta balaNueva = Instantiate<BalaEscopeta>(bala, transform.position, rotation, pool.transform);
                    GameObject.Destroy(balaNueva.gameObject, vida);
                    Debug.Log(balaNueva.transform.rotation);
                    //StartCoroutine(Ej());
                }
                tiempo = 0;
                GameManager.instance.CambiaMunicion(-1);
            }
        }
    }
//    IEnumerator Ej()
//    {
//        print(Time.time);
//        yield return new WaitForSecondsRealtime(50);
//        print(Time.time);
//    }
  }
