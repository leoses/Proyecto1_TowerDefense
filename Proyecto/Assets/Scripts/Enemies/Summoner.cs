using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Summoner : MonoBehaviour
{

    public AudioClip sound;
    AudioSource source;

    public Enemy summoningEnemy;
    public int averageWalk = 6;
    public float summoningRate = 1.5f;
    Vector2 dir;
    float time = 0;
    float limit;
    bool stopped = false;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        Random rnd = new Random();
        limit = rnd.Next(averageWalk - 1, averageWalk + 2);
    }

    private void Update()
    {
        if (!stopped)
        {
            time += Time.deltaTime;
            if (time > limit)
            {
                dir = gameObject.GetComponent<Enemy>().PassDir();
                gameObject.GetComponent<Enemy>().enabled = false;
                stopped = true;
                InvokeRepeating("Summon", 1, summoningRate);
            }
        }
    }

    void Summon()
    {
        source.clip = sound;
        source.Play();
        Enemy newEnemy = Instantiate(summoningEnemy, transform.position, transform.rotation);
        newEnemy.NewDir(dir);
    }

    public void Stop()
    {
        time = 100;
    }
}

