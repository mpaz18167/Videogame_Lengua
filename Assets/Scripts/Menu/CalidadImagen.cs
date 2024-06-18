using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class CalidadImagen : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int calidad;

    // Start is called before the first frame update
    void Start()
    {
        calidad = PlayerPrefs.GetInt("calidad", 3);
        dropdown.value = calidad;
        AjustarCalidad();
    }

    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("calidad",dropdown.value);
        calidad = dropdown.value;
    }
}
