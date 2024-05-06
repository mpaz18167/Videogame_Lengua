using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorLobby : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    public float gravityScale = 5f;

    public float speed = 5f;
    public float jumpForce=15f;

    private Vector3 moveDirection;
    public CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3 (horizontalInput, 0f ,verticalInput); 

        moveDirection = moveDirection * speed;

        moveDirection.y = yStore;

        charController.Move(moveDirection * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
    }
}
