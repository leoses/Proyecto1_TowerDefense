using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhibitor : MonoBehaviour
{
    public AudioClip sound;
    AudioSource source;

    Enemy enemy;
    TurretShooting turret;
    public float range = 4;
    bool worked = false;

    void Start()
    {
        source = gameObject.GetComponentInParent<AudioSource>();
        enemy = gameObject.GetComponentInParent<Enemy>();
        gameObject.GetComponent<CircleCollider2D>().radius = range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        turret = collision.gameObject.GetComponent<TurretShooting>();

        if (turret != null && !turret.RetState())
        {
            enemy.enabled = false;
            turret.Disabled();
            worked = true;
            source.clip = sound;
            source.Play();
        }
    }

    public void Reactivate()
    {
        if (turret != null && worked)
            turret.Disabled();
    }
}