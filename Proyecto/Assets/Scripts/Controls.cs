using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controls : MonoBehaviour {

    public GameObject Movement, MeleeAtack, Turret, Barricade, Shoot, WeaponChange;
	// Use this for initialization
	void Start ()
    {
        Movement.SetActive(false);
        Turret.SetActive(false);
        MeleeAtack.SetActive(false);
        Barricade.SetActive(false);
        Shoot.SetActive(false);
        WeaponChange.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activa(GameObject objeto)
    {
        objeto.SetActive(true);
    }
    public void Desactiva(GameObject objeto)
    {
        objeto.SetActive(false);
    }

    public void UpdateScene(string scene)
    {
        GameManager.instance.ChangeScene(scene);
    }

}
