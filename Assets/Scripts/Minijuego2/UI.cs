using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Enemy enemyScript;
    public TextMeshProUGUI puntostext;
    public TextMeshProUGUI vidastext;

    void Start()
    {
        if (enemyScript == null)
        {
            Debug.LogError("No se ha asignado el script Enemy en el inspector");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript != null && puntostext != null && vidastext!= null)
        {
            puntostext.text = enemyScript.puntos.ToString();
            vidastext.text = enemyScript.vidas.ToString();
        }
    }
}
