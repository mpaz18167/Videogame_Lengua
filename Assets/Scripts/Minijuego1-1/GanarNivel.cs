using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanarNivel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<GameOver>().MostrarGanaste();
        }
    }
    
}
