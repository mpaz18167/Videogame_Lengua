using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public TextMeshProUGUI puntostext;
    public TextMeshProUGUI vidastext;
    public TextMeshProUGUI timertext;
    public GameObject GameOver;
    public TextMeshProUGUI puntosGameOver;
    public GameObject panelDatos;
    public MousePosition mousePositionScript;

    public AudioClip puntosSound;
    public AudioClip vidaSound;
    public AudioClip GameOverSound;
    public AudioClip GameOverSoundTIMER;

    public AudioSource audioSourcepuntos;
    public AudioSource audioSourcevidas;
    public AudioSource audioSourceGameOver;
    public AudioSource audioSourceGameOverTimer;

    private int puntos = 0;
    private int vidas = 3;
    private float tiempoInicial = 60f;
    private float tiempoRestante = 60f;
    private float tiempoRojo = 10f;

    void Start()
    {
        ActualizarUI();
        GameOver.SetActive(false);
        panelDatos.SetActive(true);

        if (mousePositionScript == null)
        {
            mousePositionScript = FindObjectOfType<MousePosition>();
        }

        StartCoroutine(ContadorTiempo());
    }
    private void Update()
    {
        if (tiempoRestante <= tiempoRojo)
        {
            timertext.color = Color.red;
        }
        else
        {
            float t= tiempoRestante / tiempoInicial;
            timertext.color = Color.Lerp(Color.red,Color.green, t);
        }
    }

    public void ActualizarUI()
    {
        puntostext.text = puntos.ToString();
        vidastext.text = vidas.ToString();
    }

    public void IncrementarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();
        audioSourcepuntos.PlayOneShot(puntosSound);

    }

    public void DecrementarVidas()
    {
        vidas--;
        ActualizarUI ();
        audioSourcevidas.PlayOneShot(vidaSound);
        if(vidas <= 0)
        {
            MostrarGameOver();
            audioSourceGameOver.PlayOneShot(GameOverSound);
        }
    }

    public void MostrarGameOver()
    {
        GameOver.SetActive(true);
        panelDatos.SetActive(false);
        Time.timeScale = 0f;

        puntosGameOver.text = puntos.ToString();


        if(mousePositionScript != null)
        {
            mousePositionScript.Deactivate();
        }
        
    }
    IEnumerator ContadorTiempo()
    {
        while(tiempoRestante > 0)
        {
            timertext.text = Mathf.CeilToInt(tiempoRestante).ToString();

            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;
        }

        timertext.text = "0";

        audioSourceGameOver.PlayOneShot(GameOverSoundTIMER);
        MostrarGameOver();
        
    }
}
