using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DatosDelJugador : MonoBehaviour
{
    public static DatosDelJugador datosDelJugadorInstancia;
    public TextMeshProUGUI monedas;

    private int numeroMonedas = 0;

    private void Awake()
    {
        if (datosDelJugadorInstancia == null)
        {
            datosDelJugadorInstancia = this;

        }
    }

    public void IncrementarMonedas(int m)
    {
        numeroMonedas += m;
        monedas.text = "Monedas:  " + numeroMonedas;
    }
}
