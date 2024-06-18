using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PantallaCompleta : MonoBehaviour
{
    public Toggle toggle;

    public TMP_Dropdown resolucionDropdown;
    Resolution[] resoluciones;

    // Start is called before the first frame update
    void Start()
    {
        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn =false;
        }

        RevisarResolucion();
    }

    public void ActivarPantallaCompleta(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual =0;
        for(int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " X " +resoluciones[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolucionDropdown.AddOptions(opciones);
        resolucionDropdown.value = resolucionActual;
        resolucionDropdown.RefreshShownValue();

        resolucionDropdown.value = PlayerPrefs.GetInt("resolucion", 2
            );

    }



    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("resolucion", resolucionDropdown.value);
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

}
