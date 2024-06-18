using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamaraControlador : MonoBehaviour
{

    public Transform objetivo;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        
    }

    private void Update()
    {
        if (objetivo != null)
        {
            Vector3 targetPosition = objetivo.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref velocity,smoothTime);
        }
    }

    /*
     void Start()
    {
        offset = objetivo.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = objetivo.position - offset;
    }
    */
}
