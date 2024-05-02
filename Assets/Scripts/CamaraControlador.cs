using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamaraControlador : MonoBehaviour
{

    public Transform objetivo;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = objetivo.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = objetivo.position - offset;
    }
}
