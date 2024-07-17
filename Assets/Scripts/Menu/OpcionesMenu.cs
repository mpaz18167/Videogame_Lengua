using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpcionesMenu : MonoBehaviour
{

    public void EmpezarNivel(string NombreNivel)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(NombreNivel);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Se cerro el juego");
    }
}
