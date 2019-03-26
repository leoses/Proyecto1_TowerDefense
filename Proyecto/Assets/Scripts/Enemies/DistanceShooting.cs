using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Solo faltaría lo de aumentar la frecuencia de disparo con el paso del tiempo

public class DistanceShooting : MonoBehaviour {

    private GameObject bulletpool, target;
    public Bullet bulletprefab;
    Vector2 dir;
    public int damage;
    private bool bandera = false;
    public float higherCadence;
    public float disminutionRange;
    public float lowerCadence;
    private float time = 0;

    private void Start()
    {
        bulletpool = GameObject.FindGameObjectWithTag("bulletpool");
        target = GameObject.FindGameObjectWithTag("Nexus");
        if (higherCadence < lowerCadence) Debug.LogError("La cadencia máxima es menor que la mínima, cambiadlo");
    }

    private void Update()
    {
        if (bandera)
        {
            time += Time.deltaTime;
            if(time >= higherCadence)
            {
                Shooting();
                if (higherCadence > lowerCadence) higherCadence -= disminutionRange;
                time = 0;
            }
        }
    }

    private void Shooting()
    {
        dir = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        Bullet newBullet = Instantiate<Bullet>(bulletprefab, transform.position, Quaternion.identity, bulletpool.transform);
        newBullet.newValues(dir, damage);
    }

    public void ChangeBool(bool boolean)
    {
        bandera = boolean;
    }
}
