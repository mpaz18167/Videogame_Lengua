using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminarNivel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            FindAnyObjectByType<GameOver>().MostrarGameOver();
        }
    }

    
}
