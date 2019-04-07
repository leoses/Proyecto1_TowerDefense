using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    class Wave
    {
        public int melee = 0;
        public int distancia = 0;
        public int teletransportador = 0;
        public int invocador = 0;
        public int inhibidor = 0;
        public int escudo = 0;
        public float tiempo = 0;
        public int municion = 0;
    }


    public Spawn[] spawn;
    public static WaveManager instance = null;
    
    public Object Melee;
    public Object Distancia;
    public Object Teletransportador;
    public Object Invocador;
    public Object Inhibidor;
    public Object Escudo;



    Random rnd = new Random();

    int i;
    float tiempowave;
    float t;
    int[] cont = new int[6];

    
    [SerializeField]
    Wave[] oleada;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
            Destroy(this.gameObject);
    }

    //Variable que contiene la referencia al uiManager
    static UIManager uiManager;


    //Metodo para asignar quien es el uiManager de la escena
    public void SetUIManager(UIManager newUI)
    {
        uiManager = newUI;
    }

    void Start()
    {
        i = 0;
        tiempowave = 0;
        t = 0;

        foreach (int elem in cont) cont[elem] = 0;
    }

    void Update()
    {
        if (tiempowave < oleada[i].tiempo)
        {
            tiempowave = tiempowave + Time.deltaTime;

            t = t + Time.deltaTime;

            if (t > 1)
            {
                int r = rnd.Next(0, 6);

                if (cont[0] < oleada[i].melee && r == 0)
                {
                    t = 0;
                    spawn[rnd.Next(0, spawn.Length)].generar(Melee);
                    cont[0]++;
                }
                else if (cont[1] < oleada[i].distancia && r == 1)
                {
                    t = 0;
                    spawn[rnd.Next(0, spawn.Length)].generar(Distancia);
                    cont[1]++;
                }
                else if (cont[2] < oleada[i].teletransportador && r == 2)
                {
                    t = 0;
                    spawn[rnd.Next(0, spawn.Length)].generar(Teletransportador);
                    cont[2]++;
                }
                else if (cont[3] < oleada[i].invocador && r == 3)
                {
                    t = 0;
                    spawn[rnd.Next(0, spawn.Length)].generar(Invocador);
                    cont[3]++;
                }
                else if (cont[4] < oleada[i].inhibidor && r == 4)
                {
                    t = 0;
                    spawn[rnd.Next(0, spawn.Length)].generar(Inhibidor);
                    cont[4]++;
                }
                else if (cont[5] < oleada[i].escudo && r == 5)
                {
                    t = 0;
                    spawn[rnd.Next(0, spawn.Length)].generar(Escudo);
                    cont[5]++;
                }

            }

        }
        else
        {
            GameManager.instance.CambiaMunicion(oleada[i].municion); //*

            //foreach (int elem in cont) cont[elem-1] = 0;

            for (int i = 0; i < cont.Length; i++) cont[i] = 0;

            if (i != oleada.Length - 1)
            {
                i++;
                tiempowave = 0;
            }
            else
            {
                uiManager.End(true);
            }
        }


        /*
        if (Debug.isDebugBuild)
        {
            Debug.Log(tiempowave);
            Debug.Log(i);
        }*/

        // llamada al game Manager para pasar las variables de las oleadas para sacarlas en pantalla
        int Total = oleada.Length;
         GameManager.instance.Oleadas(i, oleada[i].tiempo, Total, tiempowave);

       
    }
    // metodo que penaliza al jugador quitandole tiempo de la oleada
    public void PenalizacionT (float penalizacion)
    {
        tiempowave = tiempowave + penalizacion;
    }
}
