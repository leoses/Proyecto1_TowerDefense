using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Summoner : MonoBehaviour
{
    public AudioClip sound;
    AudioSource source;

    public Enemy summoningEnemy;
    Enemy summoner;
    //public int averageWalk = 6;
    public float walkTime = 6.5f;
    public float summoningRate = 1.5f;
    public int summons = 4;
    Vector2 dir;
    float time = 0; //Tiempo que lleva caminando
    float summTime = 0; //Tiempo desde la última invocación
    float timesDone = 0; //Número de veces que ha invocado desde que se detuvo
    //float limit;
    bool stopped = false;
    private Animator sprite;

    private void Start()
    {
        summoner = gameObject.GetComponent<Enemy>();
        source = gameObject.GetComponent<AudioSource>();
        //Random rnd = new Random();
        //limit = rnd.Next(averageWalk - 1, averageWalk + 2);
        sprite = this.gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        time += Time.deltaTime;
        //Tras caminar x tiempo
        if (time >= walkTime)
        {
            summTime += Time.deltaTime;

            //Si no está parado se detiene
            if (!stopped)
            {
                sprite.SetBool("StartSummoning", true);
                dir = summoner.PassDir();
                summoner.enabled = false;
                stopped = true;
            }

            //Si se dan las condiciones adecuadas, invoca un número de enemigos
            if (timesDone < summons && summTime >= summoningRate)
            {
                Invoke("Summon", 0);
                timesDone++;
                summTime = 0;
            }

            //Si la invocación ha acabado, continúa caminando
            else if (timesDone >= summons && summTime >= summoningRate)
            {
                time = 0;
                timesDone = 0;
                sprite.SetBool("StartSummoning", false); //<--------------------------------
                summoner.enabled = true;
                summoner.NewDir(dir);
                stopped = false;
            }
        }
    }

    /*private void Update()
    {
        if (!stopped)
        {
            time += Time.deltaTime;
            if (time > limit)
            {
                sprite.SetBool("StartSummoning", true);
                dir = summoner.PassDir();
                summoner.enabled = false;
                stopped = true;
                InvokeRepeating("Summon", 1, summoningRate);
            }
        }
    }*/

    //Se reproduce un sonido, se crea el prefab de la invocación y se cambian los valores pertinentes
    void Summon()
    {
        source.clip = sound;
        source.Play();
        Enemy newEnemy = Instantiate(summoningEnemy, transform.position, transform.rotation);
        newEnemy.Rotation(dir);
        newEnemy.NewDir(dir);
        newEnemy.gameObject.GetComponent<Health>().Summoned();
        WaveManager.instance.LessEnemy(-1);
    }

    //Al detectar el nexo se queda estacionario invocando indefinidamente (1000)
    public void Stop()
    {
        time = 100;
        summTime = 0;
        summons = 1000;
    }
}