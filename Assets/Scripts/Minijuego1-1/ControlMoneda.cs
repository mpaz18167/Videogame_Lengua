using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMoneda : MonoBehaviour
{
    private float velocidadRotacion = 80f;
    [SerializeField] private int puntosMoneda= 50;
    [SerializeField] private AudioClip sonidoRecolectar;
    private AudioSource audioMoneda;
    private bool recolectada = false;

    private void Start()
    {
        audioMoneda = GetComponent<AudioSource>();
        if (sonidoRecolectar != null)
        {

            audioMoneda.clip = sonidoRecolectar;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!recolectada)
        {
            transform.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
        }

    }
        

     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            recolectada=true;

            DatosDelJugador.datosDelJugadorInstancia.IncrementarMonedas(puntosMoneda);
            audioMoneda.Play();

            GetComponentInChildren<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject,sonidoRecolectar.length);

        }
    }
}
