using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadePlacing : MonoBehaviour
{
    public AudioClip sound;
    AudioSource source;

    public GameObject barPref;
    SpriteRenderer color;
    Transform player;
    Vector2 distance;
    public int cost = 50;
    public int angle = 90;
    bool built = false;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        color = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Cambiando el color se marca en modo construcción qué paredes permiten la construcción de barricadas
        if (Input.GetKey("space") && Time.timeScale > 0)
            color.color = Color.magenta;

        else
            color.color = Color.white;
    }

    //Al hacer click en este objeto
    void OnMouseDown()
    {
        distance = new Vector2(Mathf.Abs(transform.position.x - player.position.x), Mathf.Abs(transform.position.y - player.position.y));

        if (Input.GetKey(GameManager.areaKey) && distance.magnitude <= GameManager.playerRange && !built && GameManager.instance.RetMoney() >= cost) //Si se está a rango y se tiene dinero
        {
            //Se crea la barricada en la dirección dada
            built = true; //Ya hay una barricada construida en esta pared
            Instantiate(barPref, transform.position, Quaternion.Euler(0, 0, angle), transform);
            GameManager.instance.GanaDinero(-cost);
            source.clip = sound;
            source.Play();
        }
    }

    //Si la barricada hija de este objeto se destruye se permite que se vuelva a construir otra
    public void Destroyed()
    {
        built = false;
    }
}