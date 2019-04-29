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

    BarricadePlacing barPlace;
    Inhibitor inh;
    Enemy enemy;
    public Healing heal;
    public int health;
    public int money;
    public int dropProb;
    public float hurtTime;
    float time = 0;
    bool dead = false;
    Animator sprite;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        sprite = this.gameObject.GetComponentInChildren<Animator>();
        enemy = this.gameObject.GetComponent<Enemy>();
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
                barPlace.Destroyed(transform.parent.position);
                source.clip = barricadeDestr;
                source.Play();
                Destroy(gameObject, 0.1f);
            }

            //Si no, es enemigo
            else
            {
                sprite.SetBool("IsDead", true);
                enemy.enabled = false;
                
                if (!dead)
                {
                    // Llamamos al wave manager para que reste uno a los enemigos que quedan
                    WaveManager.instance.LessEnemy(1);

                    // llamamos al gameManager para que sume dinero
                    GameManager.instance.GanaDinero(money);

                    //Se elige aleatoriamente si se droppea un botiquín
                    if (rnd.Next(1, 101) <= dropProb)
                        Instantiate(heal, transform.position, Quaternion.identity);

                    dead = true;
                }

                //Si es un inhibidor se reactiva la torreta
                if (inh != null)
                {
                    inh.Reactivate();
                }

                Destroy(this.gameObject, 0.5f);
            }
        }

        //Si no se ha producido un sonido en x tiempo y el objeto recibe daño, reproduce una pista
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

    //Los enemigos invocados no dan dinero
    public void Summoned()
    {
        money = 0;
    }

    public void Created(BarricadePlacing bar)
    {
        barPlace = bar;
    }
}