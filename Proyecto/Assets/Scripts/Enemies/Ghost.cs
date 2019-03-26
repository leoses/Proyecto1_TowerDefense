using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Ghost : MonoBehaviour
{
    Transform pos;
    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    Transform target;
    public float speed = 5f;
    bool active = false;

    void Start()
    {
        Random rnd = new Random();
        pos = GameObject.FindWithTag("Position " + rnd.Next(1, 4)).transform;
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (active)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, 0);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1f, speed);
        }
    }

    public void Chase()
    {
        gameObject.GetComponent<Enemy>().enabled = false;
        active = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            target.transform.position = pos.position;
            Destroy(gameObject);
        }
    }
}