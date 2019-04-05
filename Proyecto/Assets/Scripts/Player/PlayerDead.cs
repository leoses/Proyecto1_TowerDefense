using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

    public GameObject revivir; // le pasamos un gameobject con el lugar donde queremos que reaparezca
    private Animator sprite;

    private void Start()
    {
        sprite = this.gameObject.GetComponentInChildren<Animator>();
    }
    //public int daño, penalizaciónSeg, penalizacionDin;

    // metodo que lleva al jugador de vuelta a su lugar de inicio y realiza la penalización por la muerte
    // 
    public void MuerteJugador()
    {
        sprite.SetTrigger("IsDead");
        transform.position = revivir.transform.position;// llevamos al jugador a la posición inicial

        // GameManager.instance.Penalizacion(penalizacionSeg,penalizacionDin)
    }
}
