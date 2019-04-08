using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Health : MonoBehaviour
{
    static Random rnd = new Random();

    public AudioClip hurt1;
    public AudioClip hurt2;
    public AudioClip hurt3;
    public AudioClip barricadeDestr;
    AudioSource source;

    Inhibitor inh;
    public Healing heal;
    public int health;
    public int money;
    public int dropProb;
    public float hurtTime;
    float time = 0;
    Animator sprite;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        sprite = this.gameObject.GetComponentInChildren<Animator>();
        if (transform.childCount > 0)
            inh = gameObject.transform.GetChild(0).gameObject.GetComponent<Inhibitor>();
    }

    private void Update()
    {
        if (time < hurtTime)
            time += Time.deltaTime;
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
                transform.Translate(200, 200, 0);
                source.clip = barricadeDestr;
                source.Play();
                Destroy(gameObject, 0.1f);
            }

            //Sino es enemigo
            else
            {
                sprite.SetBool("IsDead", true);
                if (rnd.Next(1, 101) <= dropProb)
                    Instantiate(heal, transform.position, Quaternion.identity);

                GameManager.instance.GanaDinero(money);

                if (inh != null)
                {
                    inh.Reactivate();
                }
                Destroy(this.gameObject, 1f);

            }
        }

        else if (time >= hurtTime)
        {
            switch (rnd.Next(1, 4))
            {
                case 1:
                    source.clip = hurt1;
                    break;

                case 2:
                    source.clip = hurt2;
                    break;

                case 3:
                    source.clip = hurt3;
                    break;
            }
            source.Play();
            time = 0;
        }
    }
}