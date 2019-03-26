using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    Transform Nodo;

    private void Start()
    {
        Nodo = GetComponent<Transform>();
    }
    public void generar(Object Enemy)
    {
        Instantiate(Enemy, Nodo.position, Quaternion.identity);
    }
}

