using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JugadorLobby : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    public float gravityScale = 5f;

    public float speed = 5f;
    public float jumpForce=15f;
    public float rotateSpeed = 5f;

    private int nJumps = 2;
    private int nJumpsValue;

    public float coyoteTime = 0.15f;
    private bool coyoteTimeBool;
    private float tempoCoyote;

    public float tiempobufferSalto = 0.1f;
    private float tiempobuffer;

    private Vector3 moveDirection;
    public CharacterController charController;
    public Camera playerCamera;
    public GameObject playerModel;


    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float yStore = moveDirection.y;
        //MOVIMIENTO
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = ((transform.forward * verticalInput) + (transform.right * horizontalInput));
        moveDirection.Normalize();
        moveDirection = moveDirection * speed;
        moveDirection.y = yStore;

        //SALTO
        ControlBufferSalto();

        if (charController.isGrounded)
        {
            moveDirection.y = 0f;
            nJumpsValue = nJumps;
            //COYOTETIME
            coyoteTimeBool = true;
            tempoCoyote = 0;
        }
        //COYOTE TIME
        if(!charController.isGrounded && coyoteTimeBool) 
        {
            tempoCoyote += Time.deltaTime;  
            if (tempoCoyote > coyoteTime) coyoteTimeBool=false;
        
        }

        if (tiempobuffer>=0 && (nJumpsValue > 0 || coyoteTimeBool))
        {
            moveDirection.y = jumpForce;
            nJumpsValue--;
            tiempobuffer = 0;
        }

        //GRAVEDAD
        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        

        //ROTACION CON CAMARA
        if(Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") != 0)
        {
           transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
            //ROTACION A LA DERECHA O LA IZQUIERDA
         
           Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation,newRotation, rotateSpeed*Time.deltaTime);
        }

        charController.Move(moveDirection * Time.deltaTime);


    }

    private void ControlBufferSalto()
    {
        if (Input.GetButtonDown("Jump")) tiempobuffer = tiempobufferSalto;
        else tiempobuffer -= Time.deltaTime;
    }
}
