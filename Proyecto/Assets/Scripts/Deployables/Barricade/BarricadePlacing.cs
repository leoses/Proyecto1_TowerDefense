using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BarricadePlacing : MonoBehaviour
{
    public AudioClip sound;
    AudioSource source;
    public Tilemap tilemap;
    public GameObject barPref;
    Vector2 distance;
    Vector3 distanceFromCamera;
    Plane plane;
    public int cost = 50;
    public int angle;
    float t = 0;
    private float offset = 0.5f;

    Vector3[] usedTiles = new Vector3[100]; //Lista de posiciones en las que ya se ha construido una barricada

    private void Start()
    {
        source = gameObject.GetComponentInParent<AudioSource>();

        distanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - -10);
        plane = new Plane(Vector3.forward, distanceFromCamera);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            //Cambiando el color se marca en modo construcción qué paredes permiten la construcción de barricadas
            if (Input.GetKey("space") && Time.timeScale > 0)
                tilemap.color = Color.magenta;

            else
                tilemap.color = Color.white;

            t += Time.deltaTime;
            //El booleano done aumenta la eficiencia del script (muy a menudo no se ejecutará ningún if debido a él)

            if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(GameManager.areaKey))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float enter = 0f;

                if (plane.Raycast(ray, out enter))
                {
                    Vector3Int hit = Vector3Int.FloorToInt(ray.GetPoint(enter));

                    if (tilemap.ContainsTile(tilemap.GetTile(hit)))
                    {
                        //Distancia entre posiciones
                        distance = new Vector2(Mathf.Abs(hit.x - transform.position.x), Mathf.Abs(hit.y - transform.position.y));

                        if (distance.magnitude <= GameManager.playerRange && t > .5 && GameManager.instance.RetMoney() >= cost) //Si se está a rango y se tiene el dinero
                        {
                            Vector3 poshit = new Vector3(hit.x + 0.5f, hit.y + 0.5f, hit.z - 1);

                            //Se compara poshit con los elementos en el vector que guarda las posiciones usadas
                            //Si no se ha usado todavía, se construye una torreta
                            bool used = false;
                            int i = 0;
                            while (!used && usedTiles[i] != Vector3.zero)
                            {
                                if (usedTiles[i] == poshit)
                                    used = true;

                                i++;
                            }

                            if (!used)
                            {
                                //Se construye una pared en el tile en cuestión y se añade poshit al vector usedTiles
                                usedTiles[i] = poshit;
                                t = 0;
                                source.clip = sound;
                                source.Play();
                                GameObject barricade = Instantiate(barPref, poshit, Quaternion.identity);
                                barricade.transform.Rotate(0, 0, angle);
                                barricade.transform.GetChild(0).gameObject.GetComponent<Health>().Created(this);
                                GameManager.instance.GanaDinero(-cost);

                                //FeedBack del texto al colocar las barricadas
                                TextManager.instance.CreateText(new Vector3 (this.transform.position.x, this.transform.position.y + offset, this.transform.position.z), "-" + cost + "$");
                            }
                        }
                    }
                }
            }
        }
    }

    //Si la barricada hija de este objeto se destruye se permite que se vuelva a construir otra
    public void Destroyed(Vector3 pos)
    {
        int i = 0;
        while (usedTiles[i] != pos)
            i++;

        while (usedTiles[i] != Vector3.zero)
        {
            usedTiles[i] = usedTiles[i + 1];
            i++;
        }
    }
}