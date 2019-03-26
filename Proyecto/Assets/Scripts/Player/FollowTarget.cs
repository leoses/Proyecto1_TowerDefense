using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.1f; //Tiempo que tarda la cámara en alcanzar al objetivo
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        //Se sitúa la cámara donde el objetivo al empezar la ejecución (-10z para mantener distancia en el eje correspondiente)
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);

        //Se usa el método SmoothDamp para suavizar el movimiento de la cámara
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
