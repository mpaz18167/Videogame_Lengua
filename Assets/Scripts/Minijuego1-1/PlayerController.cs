using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animacion;
    private float velocidad = 10.5f;
    private float inputHorizontal;
    private float inputVertical;
    private float rotacionPersonaje;
    private string direccion = "Derecha";
    private CharacterController controladorPersonaje;
    private Vector3 movimiento;
    private void Start()
    {
        animacion =this.GetComponent<Animator>();
        controladorPersonaje = this.GetComponent<CharacterController>();
        //Se probara para ver si causa el error en todo el juego
        Application.targetFrameRate = 60;

    }

    private void Update()
    {
        MoverPersonaje();
    }

    private void MoverPersonaje()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //movimiento = new Vector3(inputVertical,0,inputHorizontal)*velocidad;
        movimiento.z = inputHorizontal * velocidad ;

        //MOVIMIENTO IZQUIERDA
        if(inputHorizontal > 0)
        {
            if(direccion == "Izquierda")
            {
                rotacionPersonaje = -180f;
                this.transform.Rotate(Vector3.up, rotacionPersonaje);
                direccion = "Derecha";
            }

            animacion.SetBool("isMoving", true);
            controladorPersonaje.SimpleMove(movimiento);
            return;
        }
        //MOVIMIENTO DERECHA
        if(inputHorizontal< 0)
        {
            if (direccion == "Derecha")
            {
                rotacionPersonaje = 180f;
                this.transform.Rotate(Vector3.up, rotacionPersonaje);
                direccion = "Izquierda";

            }
            animacion.SetBool("isMoving", true);
            controladorPersonaje.SimpleMove(movimiento);
            return;
        }
       
            animacion.SetBool("isMoving", false);
    }

}
