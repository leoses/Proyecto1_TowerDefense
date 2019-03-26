using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioArma : MonoBehaviour {

    public GameObject pistola;
    public GameObject escopeta;

	// Use this for initialization
	void Start ()
    {
        // por defecto siempre empieza activa la pistola
        pistola.SetActive(true);
        escopeta.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown("1"))
        {
            pistola.SetActive(true);
            escopeta.SetActive(false);
        }

        if(Input.GetKeyDown("2"))
        {
            pistola.SetActive(false);
            escopeta.SetActive(true);
        }
	}
}
