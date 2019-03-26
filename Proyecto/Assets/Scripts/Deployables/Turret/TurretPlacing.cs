using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacing : MonoBehaviour {

    public int cost = 150;
    public GameObject turretPref;
    Transform player;
    Vector2 distance;

    private void Start()
    {
        //Se busca el transform del jugador usando el tag
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    //Al hacer click en este objeto
    void OnMouseDown()
    {
        //Distancia entre posiciones
        distance = new Vector2(Mathf.Abs(transform.position.x - player.position.x), Mathf.Abs(transform.position.y - player.position.y));

        if (Input.GetKey(GameManager.areaKey) && distance.magnitude <= GameManager.playerRange && GameManager.instance.dinero >= cost) //Si se está a rango y se tiene el dinero
        {
            //Se sustituye la pared sin torreta por una pared con torreta
            Instantiate(turretPref, transform.position, transform.rotation, transform.parent);
            GameManager.instance.GanaDinero(-cost);
            Destroy(gameObject);
        }
    }
}
