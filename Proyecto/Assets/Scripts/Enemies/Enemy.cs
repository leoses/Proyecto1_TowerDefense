using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //Enemigo con movimiento cinemático para evitar que si el jugador colisiona con el
    //pueda desplazarlo por la pantalla

    public float speed;
    public Vector2 dir;
    PlayerNexusDetection DetectionArea;

    //Por defecto, los enemigos comenzarán a moverse hacia la derecha del eje x
    private void Start()
    {
        DetectionArea = gameObject.GetComponentInChildren<PlayerNexusDetection>();
        Rotation(dir);
    }
    void Update ()
    {
        transform.Translate(dir * speed * Time.deltaTime);
	}

    //Método para parar al enemigo cuando detecta que el jugador o el núcleo están dentro de su área de ataque 
    //y guardar la dirección que llevaba
    public Vector2 StopEnemy()
    {
        Vector2 mov = dir;
        dir = new Vector2(0, 0);
        return mov;
    }

    //Método para devolverle al enemigo la velocidad que llevaba antes de detenerse por detectar al jugador
    public void NewDir(Vector2 nuevo)
    {
        dir = nuevo;
    }
    //Método para cambiar la rotación del area de ataque fisico
    //En el distancia no es necesario puesto que su area es un circulo con centro en el pivote
    //del padre
    public void Rotation(Vector2 newdir)
    {
        //Mismo eje, sentido contrario

        if (dir == -1 * newdir) DetectionArea.ChangeRotation(180);
        //Mismo eje misma direccion
        else if (dir == newdir) DetectionArea.ChangeRotation(0);
        //Cambio de eje, pasamos de movernos en y a movernos en x
        else if (dir.x == 0)
        {
            if (newdir.x == dir.y) DetectionArea.ChangeRotation(-90);
            else DetectionArea.ChangeRotation(90);
        }
        //Cambio de eje, pasamos de movernos en x a movernos en y
        else
        {
            if (newdir.y == dir.x) DetectionArea.ChangeRotation(90);
            else DetectionArea.ChangeRotation(-90);
        }
    }

    public Vector2 PassDir()
    {
        return (dir);
    }
}
