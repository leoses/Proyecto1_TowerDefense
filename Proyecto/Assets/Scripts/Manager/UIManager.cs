using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //variables del inventaro
    public Text moneyText;
    public Text ammoText;
    public Text timeText;
    public Text waveText;
    public Image healthBar;
    public Image nexusBar;
    public GameObject pausePanel;
    bool active = false;

    //En el start hacemos saber al GameManager que este componente es el encargado de
    //actualizar la interfaz
    void Start()
    {
        GameManager.instance.SetUIManager(this);
    }

    void Update()
    {
        //Al pulsar el espacio se entra/sale del menú de pausa
        if (Input.GetKeyDown("escape"))
        {
            if (!active)
            {
                pausePanel.SetActive(true);
                active = true;
                Time.timeScale = 0;
            }

            else
            {
                pausePanel.SetActive(false);
                active = false;
                Time.timeScale = 1;
            }
        }
    }

    //Muestra el dinero
    public void ActualizaDinero(int Dinero)
    {
        moneyText.text = "Dinero: " + Dinero.ToString();
    }

    //Muestra la munición
    public void ActualizaMuni(int ammo)
    {
        ammoText.text = "Munición: " + ammo.ToString();
    }

    //Reduce proporcionalmente la barra de vida del jugador y/o del nucleo respecto de la vida restante.
    public void Damage(float Health, Image bar, float maxHealth)
    {
        bar.fillAmount = Health / maxHealth;
    }

    // actualiza y muestra la informacion de las oleadas
    public void Oleada(int oleada, int tiempo, int totaloleada, int tiempoact)
    {
        timeText.text = "Tiempo:" + tiempoact + "/" + tiempo;
        waveText.text = "Oleada:" + oleada + "/" + totaloleada;
    }
}