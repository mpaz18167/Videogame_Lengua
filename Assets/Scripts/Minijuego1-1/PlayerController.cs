using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 6f;
    private float inputHorizontal;


    public float gravedad = 9.8f;

    public float fuerzaExtra = 2f;

    public float fuerzaSalto = 8f;
    public bool enelAire = false;
    public bool saltoDoble = false;

    public float velocidadDash = 5f;
    private float duracionDash = 0.15f;

    private bool dashActivo = false;

    private bool coyoteActivo = true;
    private float tiempoCoyote = 0f;
    public float duaracionCoyoteTime = 0.015f;

    private float duracionBufferSalto = 0.05f;
    private float tiempoBufferSalto = 0f;

    private float fuerzaTrampolin = 35f;

    public bool enLaPared = false;

    public Vector3 posicionRaycast;
   // private float centradoRaycast = 0.2f;
    private float alturaCabezaRaycast = 0.6f;
    //private float centroCabezaRaycast = 0.10f;
    private float longitudRaycast = 0.5f;

    public LayerMask paredLayerMask;
    private float saltoParedLateral = 18f;
   // private float separacionParedSalto = 0.5f;


    private Quaternion rotacionPersonaje;
    private CharacterController chControler;
    private Vector3 movimiento;
    void Start()
    {
        chControler = this.GetComponent<CharacterController>();
        Application.targetFrameRate = 60;

    }

    void Update()
    {
        MoverPersonaje();

    }

    void MoverPersonaje()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (!enLaPared)
        {
            movimiento.z = inputHorizontal * velocidad;
        }

        //Gravedad 
        if (chControler.isGrounded)
        {
            enelAire = false;
            saltoDoble = false;
            dashActivo = false;
            enLaPared = false;
            coyoteActivo = true;
            //ANIMACION DE IDLE
            if (tiempoBufferSalto > 0)
            {
                Salto(fuerzaSalto);
            }
        }
        else
        {
            enelAire = true;
            ComprobarColisionPared();
            ComprobarTecho();
            if (coyoteActivo)
            {
                coyoteActivo = false;
                tiempoCoyote = Time.time;
                movimiento.y = 0;
            }
            if (tiempoCoyote + duaracionCoyoteTime < Time.time)
            {

                if (saltoDoble && Input.GetButtonDown("Jump"))
                {
                    saltoDoble = false;
                    movimiento.y = fuerzaSalto;
                }
                if (!saltoDoble && Input.GetButtonDown("Jump"))
                {
                    tiempoBufferSalto = duracionBufferSalto;
                }
                tiempoBufferSalto -= Time.deltaTime;
                movimiento.y -= (gravedad * fuerzaExtra) * Time.deltaTime;
            }

        }

        //Rotacion de personaje con quaternion
        if (inputHorizontal != 0)
        {
            rotacionPersonaje = Quaternion.LookRotation(new Vector3(0, 0, inputHorizontal));
            this.transform.rotation = rotacionPersonaje;
            if(!enelAire && !enLaPared)
            {
                //ANIMACION CAMINANDO TRUE
            }

        }
        if (Input.GetButtonDown("Jump") && !enelAire)
        {
            Salto(fuerzaSalto);
        }
        if (Input.GetButtonDown("Fire3") && !dashActivo)
        {
            StartCoroutine(Dash());
        }

        chControler.Move(movimiento * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        float tiempoInicial = Time.time;
        dashActivo = true;

        while (Time.time < tiempoInicial + duracionDash)
        {
            movimiento = this.transform.TransformDirection(Vector3.forward * velocidadDash);
            movimiento.y = 0;
            chControler.Move(movimiento * Time.deltaTime);
            yield return null;

        }
    }


    void Salto(float fuerza)
    {
        ComprobarColisionPared();
        
        enelAire = true;
        saltoDoble = true;
        coyoteActivo = false;
        if (enLaPared)
        {
            rotacionPersonaje = Quaternion.LookRotation(this.transform.TransformDirection(Vector3.forward * -1));
            this.transform.rotation = rotacionPersonaje;
            movimiento = this.transform.TransformDirection(Vector3.forward * saltoParedLateral * velocidad);
        }
        enLaPared = false;
        tiempoCoyote -= duaracionCoyoteTime;
        movimiento.y = fuerza;
        tiempoBufferSalto = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Trampolin"))
        {
            Salto(fuerzaTrampolin);
        }
    }

    void ComprobarColisionPared()
    {
        //REPARAR BUG
        //posicionRaycast = this.transform.position;
        //posicionRaycast.y += centradoRaycast;



        //Debug.DrawRay(posicionRaycast, Vector3.forward * longitudRaycast, Color.blue);
        //if (Physics.Raycast(posicionRaycast, transform.TransformDirection(Vector3.forward), longitudRaycast, paredLayerMask.value))
        //{
            
        //    if (chControler.isGrounded)
        //    {
               
        //        movimiento = transform.TransformDirection(Vector3.forward * separacionParedSalto * velocidad  * -1);

        //    }
        //    else
        //    {
        //        //DESACTIVAR ANIMACIONES DE SALTAR Y CAMINAR Y ACTIVAR ANIMACION DE SALTOPARED
        //        if (!enLaPared)
        //        {
        //            movimiento.y = 0;
        //        }
        //        enLaPared = true;
        //        enelAire = false;
        //        gravedad = 1.8f;
        //    }

        //}
        //else
        //{
        //    enLaPared = false;
        //    //desactivar animacion de walljump
        //    gravedad = 9.8f;

        //}
    }

    void ComprobarTecho()
    {
        //posicionRaycast = transform.TransformPoint(Vector3.forward * centroCabezaRaycast);
        //posicionRaycast.y += alturaCabezaRaycast;
        posicionRaycast = transform.position + Vector3.up * alturaCabezaRaycast;

        Debug.DrawRay(posicionRaycast, Vector3.up * longitudRaycast, Color.red);

        if (Physics.Raycast(posicionRaycast, Vector3.up, longitudRaycast, paredLayerMask.value))
        {
            movimiento.y = -0.5f;
        }
    }
}
