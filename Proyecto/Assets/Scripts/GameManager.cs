using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    Music music;
    static PlayerNexusDead player;
    //static PlayerNexusDead nexus;

    //Distintos valores generales del juego
    static public string areaKey = "space";
    public const float playerRange = 4;
    public const float turretRange = 4;
    public int ammo = 5;
    public int dinero = 0;
    public float vidaMax = 300;
    public float vidaJugador;
    float vidaNucleoMax = 3000;
    public float vidaNucleo;
    public int penaSeg, penaDin; // penalizaciones por muerte de tiempo y dinero
    static private int oleada, totaloleada, tiempoact, tiempo;

    //Variable que contiene la referencia al uiManager
    static UIManager uiManager;


    //Metodo para asignar quien es el uiManager de la escena
    public void SetUIManager(UIManager newUI)
    {
        uiManager = newUI;
    }

    void Awake()
    //Se instancia el Game Manager
    {
        if (instance == null)
        {
            instance = this;
        }

        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        //Al inicio se muestra la munición y dinero que se ha fijado como incial del jugador
        uiManager.ActualizaMuni(ammo);
        uiManager.ActualizaDinero(dinero);
        vidaJugador = vidaMax;
        vidaNucleo = vidaNucleoMax;
        music = GameObject.FindWithTag("Music").GetComponent<Music>();
    }

    //Metodo que termina el juego
    public void EndGame(bool win)
    {
        Time.timeScale = 0;
        uiManager.End(win);
        music.PlayMusic(win);
    }

    // metodo que cambia de escena en el juego
    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    // metodo que aumenta la cantidad de dinero del jugador
    public void GanaDinero(int valor)
    {
        dinero = dinero + valor;
        uiManager.ActualizaDinero(dinero); //Actualiza el UI
    }

    //reduce la vida del jugador y llama al uiManager para reducir la barra de vida del jugador del HUD
    public void PierdeVidaJugador(float daño)
    {
        vidaJugador -= daño;
        uiManager.Damage(vidaJugador, uiManager.healthBar, vidaMax);
        //si la vida es menor o igual a 0 el jugador respawnea y la vida y la propia barra se ponen al máximo
        if (vidaJugador <= 0)
        {
            player.PlayerRespawn();
            Penalización();
            vidaJugador = vidaMax;
            uiManager.healthBar.fillAmount = 1;
        }
    }

    //reduce la vida del núcleo y llama al uiManager para reducir la barra de vida del núcleo del HUD
    public void PierdeVidaNucleo(int daño)
    {
        vidaNucleo -= daño;
        uiManager.Damage(vidaNucleo, uiManager.nexusBar, vidaNucleoMax);
        //si la vida del núcleo es menor o igual a 0 entonces termina el juego.
        if (vidaNucleo <= 0)
            EndGame(false);
    }

    public void CambiaMunicion(int cant)
    {
        ammo += cant;
        uiManager.ActualizaMuni(ammo); //Actualiza el UI
    }

    /*public static void FindNexus(PlayerNexusDead nexus1)
    {
        nexus = nexus1;
    }*/

    public static void FindPlayer(PlayerNexusDead player1)
    {
        player = player1;
    }

    // penalización por muerte del jugador
    public void Penalización()
    {
        dinero = dinero - penaDin;
        uiManager.ActualizaDinero(dinero);
        WaveManager.instance.PenalizacionT(penaSeg);
    }

    // metodo para llevar cuenta de las oleadas
    public void Oleadas(int Oleada, float Tiempo, int TotalOleada, float tmpact)
    {
        tiempoact = (int)tmpact;
        tiempo = (int)Tiempo;
        oleada = Oleada;
        totaloleada = TotalOleada;

        uiManager.Oleada(oleada, tiempo, totaloleada, tiempoact);
    }

    public int RetMoney()
    {
        return (dinero);
    }

    
}