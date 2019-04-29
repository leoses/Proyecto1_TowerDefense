using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemigo con movimiento cinemático para evitar que si el jugador colisiona con el
    //pueda desplazarlo por la pantalla

    public float speed;
    public Vector2 dir;

    Vector2 prevDir;
    PlayerNexusDetection detectionArea;
    Summoner sum;
    Animator sprite;
    bool summoning = false;

    //Por defecto, los enemigos comenzarán a moverse hacia la derecha del eje x
    void Start()
    {
        sum = gameObject.GetComponent<Summoner>();
        detectionArea = gameObject.GetComponentInChildren<PlayerNexusDetection>();
        sprite = gameObject.GetComponentInChildren<Animator>();
        Rotation(dir);
    }
    void Update()
    {
        if (!summoning)
            transform.Translate(dir * speed * Time.deltaTime);
    }

    //Método para parar al enemigo cuando detecta que el jugador o el núcleo están dentro de su área de ataque 
    //y guardar la dirección que llevaba
    public void StopEnemy(bool nexus)
    {
        prevDir = dir;
        dir = Vector2.zero;

        if (sum != null && nexus)
            sum.Stop();

        sprite.SetBool("Idle", true);
    }

    //Método para devolverle al enemigo la velocidad que llevaba antes de detenerse por detectar al jugador
    public void NewDir(Vector2 direc)
    {
        if (direc == Vector2.zero)
        {
            dir = prevDir;
            sprite.SetBool("Idle", false);
        }
        else
            dir = direc;
    }

    //Método para cambiar la rotación del area de ataque fisico y la orientación de los sprites de los enemigos
    public void Rotation(Vector2 newdir)
    {
        //Melees invocados por el distancia (¿Pierden la referencia?)
        if (detectionArea == null) detectionArea = this.gameObject.GetComponentInChildren<PlayerNexusDetection>();
        if (sprite == null) sprite = this.gameObject.GetComponentInChildren<Animator>();

        //Mismo eje, sentido contrario
        if (dir == -1 * newdir)
        {
            detectionArea.ChangeRotation(180);
            sprite.transform.Rotate(0, 0, 180);
        }
        //Mismo eje misma direccion
        else if (dir == newdir) detectionArea.ChangeRotation(0);
        //Cambio de eje, pasamos de movernos en y a movernos en x
        else if (dir.x == 0)
        {
            if (newdir.x == dir.y)
            {
                detectionArea.ChangeRotation(-90);
                sprite.transform.Rotate(0, 0, -90);
            }
            else
            {
                detectionArea.ChangeRotation(90);
                sprite.transform.Rotate(0, 0, 90);
            }
        }
        //Cambio de eje, pasamos de movernos en x a movernos en y
        else
        {
            if (newdir.y == dir.x)
            {
                detectionArea.ChangeRotation(90);
                sprite.transform.Rotate(0, 0, 90);
            }
            else
            {
                detectionArea.ChangeRotation(-90);
                sprite.transform.Rotate(0, 0, -90);
            }
        }
    }

    public Vector2 PassDir()
    {
        if (dir == Vector2.zero)
            return (prevDir);

        else
            return (dir);
    }

    public void ToggleSummon()
    {
        if (summoning)
            summoning = false;

        else
            summoning = true;
    }
}