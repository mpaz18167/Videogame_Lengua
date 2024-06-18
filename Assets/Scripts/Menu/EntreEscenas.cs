using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntreEscenas : MonoBehaviour
{
    private void Awake()
    {
        var noDestruirEntreEscenas = FindObjectsOfType<EntreEscenas>();
        if (noDestruirEntreEscenas.Length >1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
