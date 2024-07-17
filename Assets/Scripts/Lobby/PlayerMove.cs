using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controladorPersonaje_lobby;
    private Animator animacion_lobby;

    private float velocidad=10.5f;
    private float inputVertical;
    private float inputHorizontal;

    private Quaternion rotacionPersonaje;
    private float velocidadRotacion = 10f;
    private float gravedad = 10f;

    private Vector3 movimiento;
    

    [Header("PARTICLES")]
    [SerializeField] private ParticleSystem polvoPies;
    private ParticleSystem.EmissionModule emisionPolvoPies;
    [Header("SOUNDSVFX")]
    [SerializeField] private AudioSource footSteps;





    private void Start()
    {
        controladorPersonaje_lobby = GetComponent<CharacterController>();
        animacion_lobby = GetComponent<Animator>();
        emisionPolvoPies = polvoPies.emission;
        Application.targetFrameRate = 60;

    }


    void Update()
    {
        MoverPersonaje();
        
    }

    private void MoverPersonaje()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        movimiento = new Vector3(inputHorizontal, 0, inputVertical).normalized * velocidad;

        if (controladorPersonaje_lobby.isGrounded)
        {
            animacion_lobby.SetBool("isMoving", false);
            
        }
        else
        {
            movimiento.y -= gravedad;
        }
        

        if (inputHorizontal!=0 || inputVertical!=0)
        {
            //Rotacion del personaje
            rotacionPersonaje = Quaternion.LookRotation(new Vector3(inputHorizontal, 0, inputVertical));
            transform.rotation = Quaternion.Slerp(transform.rotation,rotacionPersonaje,Time.deltaTime*velocidadRotacion);

            animacion_lobby.SetBool("isMoving", true);
            emisionPolvoPies.rateOverTime = 50f;
            footSteps.enabled = true;

        }
        else
        {
            footSteps.enabled = false;
            emisionPolvoPies.rateOverTime = 0f;
        }

        controladorPersonaje_lobby.Move(movimiento * Time.deltaTime);



    }

    
}



