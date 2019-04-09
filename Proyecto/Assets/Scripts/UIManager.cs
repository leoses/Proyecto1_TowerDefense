﻿using System.Collections;
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
    public Text EndGameText;
    public Image healthBar;
    public Image nexusBar;
    public GameObject pausePanel;
    public GameObject EndGame;
    public Button ReplayButton, NextLevelButton;
    bool active = false;

    //En el start hacemos saber al GameManager que este componente es el encargado de
    //actualizar la interfaz
    void Start()
    {
        GameManager.instance.SetUIManager(this);
        WaveManager.instance.SetUIManager(this);
        ReplayButton.gameObject.SetActive(false);
        NextLevelButton.gameObject.SetActive(false);
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
    public void ActualizaDinero(int money)
    {
        moneyText.text = money.ToString();
    }

    //Muestra la munición
    public void ActualizaMuni(int ammo)
    {
        ammoText.text = ammo.ToString();
    }

    //Reduce proporcionalmente la barra de vida del jugador y/o del nucleo respecto de la vida restante.
    public void Damage(float Health, Image bar, float maxHealth)
    {
        bar.fillAmount = Health / maxHealth;
    }

    // actualiza y muestra la informacion de las oleadas
    public void Oleada(int wave, int time, int totalWaves, int actTime)
    {
        timeText.text = actTime + "/" + time;
        waveText.text = wave + "/" + totalWaves;
    }

    public void UpdateScene(string scene)
    {
        GameManager.instance.ChangeScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
        //Solo cierra la aplicación ejecutable (.exe) no el editor
    }

    // Metodo que abre las opciones al terminar el juego
    public void End(bool win)
    {
        EndGame.SetActive(true);
        if (win)
        {
            EndGameText.text = "You Win";
            NextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            EndGameText.text = "Game Over";
            ReplayButton.gameObject.SetActive(true);
        }
    }

    public void ExitPauseMenu()
    {
        active = false;
        pausePanel.SetActive(false);
    }
}