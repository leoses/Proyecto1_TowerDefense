using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 move;
    public float speed;
    bool W;
    bool A;
    bool S;
    bool D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Input de las teclas de movimiento
        W = Input.GetKey(KeyCode.W);
        A = Input.GetKey(KeyCode.A);
        S = Input.GetKey(KeyCode.S);
        D = Input.GetKey(KeyCode.D);

        //Se resetea el vector move para que cada frame el input sea nuevo
        move = Vector2.zero;

        //Se fija el valor del vector de movimiento sumando los valores dados por las teclas pulsadas (A + D = 0 movimiento)
        if (W)
            move = new Vector2(move.x, move.y + speed);
        if (A)
            move = new Vector2(move.x - speed, move.y);
        if (S)
            move = new Vector2(move.x, move.y - speed);
        if (D)
            move = new Vector2(move.x + speed, move.y);
    }

    void FixedUpdate()
    {
        //Movimientos físicos -> FixedUpdate
        rb.velocity = move;
    }
}
