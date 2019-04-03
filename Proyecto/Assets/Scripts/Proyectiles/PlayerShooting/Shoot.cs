using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    //Disparo de la pistola
    float tiempo;
    public float cadencia;
    public Bala bala;
    private GameObject pool;

    private void Start()
    {
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
        }
    }
}
