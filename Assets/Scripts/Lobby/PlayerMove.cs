using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController chPlayer;
    private Animator anim;
    private float turnVelocity;
    private Vector3 velocity;
    private float gravity = -9.8f;
    public bool inFloor;

    [Header("Stats")]
    public float speedMovemente;
    public float turnTime = 0.2f;
    public float jumpHeight = 3;
    public float jumpForce = -2;

    [Header("References")]
    public Transform floor;
    public float floorDistance = 0.1f;
    public LayerMask layerFloor;


    private void Awake()
    {
        chPlayer = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Jump();
        anim.SetFloat("velocityY", velocity.y);
    }

    private void PlayerMovement()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(X, 0, Z);

        if (direction != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref turnVelocity, turnTime);

            Vector3 movementDirection = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0,angle, 0);

            chPlayer.Move(movementDirection.normalized * speedMovemente * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }

    public void DisableJump()
    {
        anim.SetBool("isJumping", false);
    }


    private void Jump()
    {
        inFloor = Physics.CheckSphere(floor.position, floorDistance, layerFloor);
        anim.SetBool("inFloor", inFloor);

        if (inFloor && velocity.y < 0)
        {
            velocity.y = jumpForce;

        }

        if (Input.GetButtonDown("Jump") && inFloor) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * jumpForce * gravity);
            anim.SetBool("isJumping", true);

        }

        velocity.y += gravity * Time.deltaTime;

        chPlayer.Move(velocity*Time.deltaTime);
    }



}

