using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float posZ;
    [SerializeField] private Transform gun;

    [SerializeField] private Vector3 screenPosition;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();

        RotateGun();

    }
    private void FollowCursor()
    {
        screenPosition = Input.mousePosition;

        screenPosition.z = posZ;
        transform.position = mainCamera.ScreenToWorldPoint(screenPosition);
        
        
    }

    private void RotateGun()
    {
        // Obtener la posici�n del cursor en el mundo
        Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, posZ));

        // Calcular la direcci�n desde el arma hacia la posici�n del cursor
        Vector3 direction = cursorPosition - gun.position;

        // Obtener la rotaci�n hacia la direcci�n del cursor
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Aplicar la rotaci�n al arma
        gun.rotation = targetRotation;
    }
}
    
