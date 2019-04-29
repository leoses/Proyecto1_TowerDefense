using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class TurretPlaceUpgrade : MonoBehaviour
{
    [System.Serializable]
    struct Upgrade
    {
        public int cost;
        public int damage;
        public float fireRate;
    }

    [SerializeField]
    Upgrade first;

    [SerializeField]
    Upgrade second;

    public AudioClip placingSound;
    public AudioClip upgradeSound;
    AudioSource source;
    public Sprite n1, n2;

    public Tilemap tilemap;
    public int costPlacement = 150;
    public int costUpgrade = 150;
    public GameObject turretPref;
    Vector2 distance;
    Plane plane;
    Vector3 distanceFromCamera;

    GameObject[] placedTurrets = new GameObject[100]; //Lista de posiciones en las que ya se ha construido una torreta
    int ind = 0;

    int segments = 40;
    float xradius = GameManager.playerRange; //El área de construcción tiene los radios dados en el Game Manager
    float yradius = GameManager.playerRange;
    LineRenderer line;
    bool done = false; //¿Se ha representado ya el área?
    float t = 0; //Una variable para que el metodo de oner torretas no ponga muchas de una sola vez

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

                        if (distance.magnitude <= GameManager.playerRange && t > .5) //Si se está a rango
                        {
                            Vector3 poshit = new Vector3(hit.x + 0.5f, hit.y + 0.5f, hit.z - 1);

                            //Se compara poshit con los elementos en el vector que guarda las posiciones usadas
                            //Si no se ha usado todavía, se construye una torreta
                            bool used = false;
                            int i = 0;
                            while (!used && placedTurrets[i] != null && placedTurrets[i].transform.position != Vector3.zero)
                            {
                                if (placedTurrets[i].transform.position == poshit)
                                    used = true;

                                else
                                    i++;
                            }

                            if (!used && GameManager.instance.RetMoney() >= costPlacement)
                            {
                                //Se construye una pared en el tile en cuestión y se añade poshit al vector usedTiles
                                t = 0;
                                placedTurrets[ind] = Instantiate(turretPref, poshit, Quaternion.identity);
                                ind++;
                                source.clip = placingSound;
                                source.Play();
                                GameManager.instance.GanaDinero(-costPlacement);
                            }

                            else
                            {
                                int level = placedTurrets[i].GetComponent<TurretShooting>().RetLevel();
                                switch (level)
                                {
                                    case 1:
                                        //Si se tiene dinero, se está a rango y no está a nivel máximo
                                        if (GameManager.instance.RetMoney() >= first.cost && level < 3)
                                        {
                                            placedTurrets[i].GetComponent<TurretShooting>().TurretUpgrade(first.damage, first.fireRate);
                                            GameManager.instance.GanaDinero(-first.cost);
                                            placedTurrets[i].GetComponent<SpriteRenderer>().sprite = n1;
                                        }
                                        break;

                                    case 2:
                                        if (GameManager.instance.RetMoney() >= second.cost && level < 3)
                                        {
                                            placedTurrets[i].GetComponent<TurretShooting>().TurretUpgrade(second.damage, second.fireRate);
                                            GameManager.instance.GanaDinero(-second.cost);
                                            placedTurrets[i].GetComponent<SpriteRenderer>().sprite = n2;
                                        }
                                        break;
                                }

                                t = 0;
                                source.clip = upgradeSound;
                                source.Play();
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