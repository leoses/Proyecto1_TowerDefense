using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour {

    public int damage = 50;
    Health found;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        found = collision.gameObject.GetComponent<Health>();

        //Si colisiona con un enemigo le daña
        if (found != null)
            found.RecieveDamage(damage);
    }
}