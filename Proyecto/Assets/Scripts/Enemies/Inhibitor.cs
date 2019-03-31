using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhibitor : MonoBehaviour
{

    Enemy enemy;
    TurretShooting turret;
    public float range = 4;

    void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
        gameObject.GetComponent<CircleCollider2D>().radius = range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        turret = collision.gameObject.GetComponent<TurretShooting>();

        if (turret != null)
        {
            enemy.enabled = false;
            turret.Disabled();
        }
    }

    public void Reactivate()
    {
        if (turret != null)
            turret.Disabled();
    }
}