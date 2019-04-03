using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Update
{
    public int cost;
    public int damage;
    public float fireRate;
}

public class TurretUpdate : MonoBehaviour {

    public AudioClip sound;
    public AudioSource source;

    [SerializeField]
    Update first;

    [SerializeField]
    Update second;


    int level = 1;
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

        switch (level)
        {
            case 1:
                if (Input.GetKey(GameManager.areaKey) && distance.magnitude <= GameManager.playerRange && GameManager.instance.dinero >= first.cost && level < 3)
                {
                    gameObject.GetComponent<TurretShooting>().TurretUpdate(first.damage, first.fireRate);
                    GameManager.instance.GanaDinero(-first.cost);
                    level++;
                }
                    break;

            case 2:
                if (Input.GetKey(GameManager.areaKey) && distance.magnitude <= GameManager.playerRange && GameManager.instance.dinero >= second.cost && level < 3)
                {
                    gameObject.GetComponent<TurretShooting>().TurretUpdate(second.damage, second.fireRate);
                    GameManager.instance.GanaDinero(-second.cost);
                    level++;
                }
                    break;
        }
        source.clip = sound;
        source.Play();
    }
}
