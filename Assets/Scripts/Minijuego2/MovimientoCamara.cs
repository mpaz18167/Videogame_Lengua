using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform playerbody;

    private float rotationX=0f, rotationY=0f;


    void Start()
    {
       //Cursor.lockState = CursorLockMode.Locked;    
    }

    // Update is called once per frame
    void Update()
    {
       float mouseX = Input.GetAxis("Mouse X") * sensitivity *Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * sensitivity *Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -45, 45);
        
        rotationY += mouseX;
        rotationY = Mathf.Clamp(rotationY, -45, 45);
        float clampedRotationY = rotationY - playerbody.eulerAngles.y;

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerbody.Rotate(Vector3.up*clampedRotationY);


    }
}
