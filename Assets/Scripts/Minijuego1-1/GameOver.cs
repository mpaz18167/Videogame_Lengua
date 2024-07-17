using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI puntosGameOver;
    public TextMeshProUGUI puntosGanaste;

    public TextMeshProUGUI puntosLeaderBoard;


    public TextMeshProUGUI puntosJuego;
    public GameObject gameOverPanel;
    public GameObject datosPanel;
    public GameObject ganastePanel;
    public GameObject highScorePanel;

    public AudioClip sonidoGameOver;
    public AudioClip sonidoGanaste;

    private AudioSource audioSource;


    private LeaderBoardController leaderboard;
    private void Start()
    {
        leaderboard = FindObjectOfType<LeaderBoardController>();
        audioSource = GetComponent<AudioSource>();
    }
    public void MostrarGameOver()
    {
        Time.timeScale = 0.0f;
        gameOverPanel.SetActive(true);
        datosPanel.SetActive(false);
        ganastePanel.SetActive(false);
        puntosGameOver.text = puntosJuego.text;
        ReproducirSonido(sonidoGameOver);
        
    }

    public void MostrarGanaste()
    {
        Time.timeScale = 0.0f;
        ganastePanel.SetActive(true);
        datosPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        puntosGanaste.text = puntosJuego.text;

        ReproducirSonido(sonidoGanaste);
    }

    public void MostrarHighScore()
    {
        highScorePanel.SetActive(true);
        puntosLeaderBoard.text = puntosJuego.text;
        datosPanel.SetActive(false);
        ganastePanel.SetActive(false);
        gameOverPanel.SetActive(false);

       
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverLobby(string escena)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(escena);
    }

    private void ReproducirSonido(AudioClip clip)
    {
        if (clip !=null && audioSource!=null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }


    
}
