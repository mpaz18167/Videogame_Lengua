using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPies : MonoBehaviour
{
    public AudioSource footSteps;
    private void Update()
    {
        if ((Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)) 
        {
            footSteps.enabled = true;
        }
        else
        {
            footSteps.enabled = false;
        }

    }

}
