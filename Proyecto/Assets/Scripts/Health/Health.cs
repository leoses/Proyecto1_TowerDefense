using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Health : MonoBehaviour
{
    Inhibitor inh;
    public Healing heal;
    public int health;
    public int money;
    public int dropProb;

    private void Start()
    {
        if (transform.childCount > 0)
            inh = gameObject.transform.GetChild(0).gameObject.GetComponent<Inhibitor>();
    }

    //Metodo que resta vida y destruye o respawnea cuando esta llega a 0;
    public void RecieveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //Si es una barricada
            if (gameObject.CompareTag("Barricade"))
            {
                transform.Translate(100, 100, 0);
                Destroy(gameObject, 0.1f);
            }

            //Sino es enemigo
            else
            {
                Random rnd = new Random();

                if (rnd.Next(1, 101) <= dropProb)
                    Instantiate(heal, transform.position, Quaternion.identity);

                GameManager.instance.GanaDinero(money);

                if (inh != null)
                {
                    inh.Reactivate();
                }
                Destroy(gameObject);
            }
        }
    }
}