using System.Collections;
using System.Collections.Generic;
using UnityEngine;//

public class BalaEscopeta : MonoBehaviour
{
    public int speed;
    Rigidbody2D rb2D;
    Vector2 disparo, angulo;
    public int damage;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        disparo = transform.up * speed;
        rb2D.velocity = -disparo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemigo = collision.gameObject.GetComponent<Health>();
        Shield shield = collision.gameObject.GetComponent<Shield>();

        //Si el gameObject con el que "colisionan" tiene salud, le causa daño a no ser que sea o el Escudo o el nexo
        if (enemigo != null && shield == null && collision.gameObject.tag != "Nexus")
            enemigo.RecieveDamage(damage);
        else    //En caso contrario, la bala se destruirá
            Destroy(this.gameObject);
    }
}
