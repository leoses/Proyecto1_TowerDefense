using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //variables del inventaro
    public Text DineroText;
    public Text MunicionText;
    public Image Healthbar;
    public Image Nexusbar;
    public Text TiempoText;
    public Text OleadaText;
    public GameObject EndGame;
    public Text EndGameText;
    public Button ReplayButton, NextLevelButton;
    

    //En el start hacemos saber al GameManager que este componente es el encargado de
    //actualizar la interfaz
    void Start()
    {
        GameManager.instance.SetUIManager(this);
        EndGame.SetActive(false);
        ReplayButton.gameObject.SetActive(false);
        NextLevelButton.gameObject.SetActive(false);
    }

    //Muestra el dinero
    public void ActualizaDinero(int Dinero)
    {
        DineroText.text = "Dinero: " + Dinero.ToString();
    }

    //Muestra la munición
    public void ActualizaMuni(int ammo)
    {
        MunicionText.text = "Munición: " + ammo.ToString();
    }

    //Reduce proporcionalmente la barra de vida del jugador y/o del nucleo respecto de la vida restante.
    public void Damage(float Health, Image bar, float maxHealth)
    {
        bar.fillAmount = Health / maxHealth;
    }

    // actualiza y muestra la informacion de las oleadas
    public void Oleada(int oleada, int tiempo, int totaloleada, int tiempoact)
    {
        TiempoText.text = "Tiempo:" +tiempoact + "/" +  tiempo;
        OleadaText.text = "Oleada:" + oleada + "/" + totaloleada;
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
    public void End (bool win)
    {
        EndGame.SetActive(true);
        if(win)
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

    //public void ColorPenalization( ref int t)
    //{
    //    int n = 3;
    //    DineroText.color = Color.red;
    //    TiempoText.color = Color.red;

    //}
}
