using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class TurretArea : MonoBehaviour
{
    public AudioClip sound;
    AudioSource source;

    public Tilemap tilemap;
    public int cost = 150;
    public GameObject turretPref;
    Transform player;
    Vector2 distance;
    Plane plane;
    Vector3 distanceFromCamera;

    Vector3[] usedTiles = new Vector3[100]; //Lista de posiciones en las que ya se ha construido una torreta
    int ind = 0;

    int segments = 40;
    float xradius = GameManager.playerRange; //El área de construcción tiene los radios dados en el Game Manager
    float yradius = GameManager.playerRange;
    LineRenderer line;
    bool done = false; //¿Se ha representado ya el área?
    float t; //Una variable para que el metodo de oner torretas no ponga muchas de una sola vez

    void Start()
    {
        source = gameObject.GetComponentInParent<AudioSource>();
        line = gameObject.GetComponent<LineRenderer>(); //Se coge el componente line renderer que es la representación visual del área de construcción
        line.useWorldSpace = false; //No se usan coordenadas del mundo

        distanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - -10);
        plane = new Plane(Vector3.forward, distanceFromCamera);
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            t += Time.deltaTime;
            //El booleano done aumenta la eficiencia del script (muy a menudo no se ejecutará ningún if debido a él)
            if (!done && Input.GetKey(GameManager.areaKey)) //Si no está representada el área y se pulsa el espacio se crea el círculo
            {
                line.positionCount = segments + 1;
                CreatePoints();
                done = true;
            }

            else if (done && !Input.GetKey(GameManager.areaKey)) //Si ya está representada y se deja de pulsar el espacio se borra
            {
                line.positionCount = 0;
                done = false;
            }

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
                            while (usedTiles[i] != Vector3.zero)
                            {
                                if (usedTiles[i] == poshit)
                                    used = true;

                                i++;
                            }

                            if (!used)
                            {
                                //Se construye una pared en el tile en cuestión y se añade poshit al vector usedTiles
                                usedTiles[ind] = poshit;
                                ind++;
                                t = 0;
                                source.clip = sound;
                                source.Play();
                                Instantiate(turretPref, poshit, Quaternion.identity);
                                GameManager.instance.GanaDinero(-cost);
                            }
                        }
                    }
                }
            }
        }
    }

    void CreatePoints() //Se usa trigonometría para crear la circunferencia dado el número de lados del polígono
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}