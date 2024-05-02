using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMoneda : MonoBehaviour
{
    private float velocidadRotacion = 80;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DatosDelJugador.datosDelJugadorInstancia.IncrementarMonedas(1);
            Destroy(this.gameObject);

        }
    }
}
