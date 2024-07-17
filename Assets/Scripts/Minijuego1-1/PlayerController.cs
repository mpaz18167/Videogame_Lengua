using Cinemachine.Utility;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animacion;
    
    private float inputHorizontal;
    private float duracionBufferSalto = 0.1f;
    private float tiempoBufferSalto = 0f;
    

    [Header("ParametrosMovimiento")]
    [SerializeField] private float gravedad = 9.8f;
    [SerializeField] private float velocidad = 10.5f;
    [SerializeField] private float fuerzaSalto = 10f;
    [SerializeField] private float fuerzaTrampolin = 15f;
    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidopasos;
    [SerializeField] private AudioClip sonidoSalto;
    [SerializeField] private AudioClip sonidoTrampolin;





    private AudioSource audioSource;

    private Quaternion rotacionPersonaje;
    
    private bool enElAire = false;
    private bool saltoDoble = false;

    
    private CharacterController controladorPersonaje;
    private Vector3 movimiento;
    
    private void Start()
    {
        animacion =this.GetComponent<Animator>();
        controladorPersonaje = this.GetComponent<CharacterController>();
        audioSource = this.GetComponent<AudioSource>();
        
        Application.targetFrameRate = 60;

    }

    private void Update()
    {
        MoverPersonaje();
    }

    private void MoverPersonaje()
    {
        animacion.SetBool("isJumping", true);
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        movimiento.z = inputHorizontal * velocidad;

        if (controladorPersonaje.isGrounded)
        {
            enElAire=false;
            saltoDoble=false;
            
            
            
            animacion.SetBool("isJumping", false);
            animacion.SetBool("isMoving", false);

            if (tiempoBufferSalto > 0)
            {
                Salto(fuerzaSalto);
                ReproducirSonido(sonidoSalto, false,1f);
            }

            if (inputHorizontal!=0)
            {
                if (!audioSource.isPlaying || audioSource.clip != sonidopasos)
                {
                    ReproducirSonido(sonidopasos, true,1f);
                }
            }
            else
            {
                if (audioSource.clip == sonidopasos)
                {
                    audioSource.Stop();
                }
            }

        } 
        else
        {

            enElAire = true;
            
            if (saltoDoble && Input.GetButtonDown("Jump"))
            {
                saltoDoble = false;
                movimiento.y = fuerzaSalto;
                ReproducirSonido(sonidoSalto, false,1f);
            }

            if(!saltoDoble && Input.GetButtonDown("Jump"))
            {
                tiempoBufferSalto = duracionBufferSalto;
            }
            //Gravedad
            tiempoBufferSalto -=Time.deltaTime;
            movimiento.y -= gravedad * Time.deltaTime;


        }

        if (inputHorizontal !=0)
        {
            rotacionPersonaje = Quaternion.LookRotation(new Vector3(0,0,inputHorizontal));
            this.transform.rotation = rotacionPersonaje;
            animacion.SetBool("isMoving", true);
        }

        if(Input.GetButtonDown("Jump") && !enElAire)
        {
            Salto(fuerzaSalto);
            ReproducirSonido(sonidoSalto, false,1f);

        }
        
        controladorPersonaje.Move(movimiento * Time.deltaTime);

    }

    private void Salto(float fuerza)
    {
        enElAire = true;
        saltoDoble = true;

        
        animacion.SetBool("isJumping", true);
        movimiento.y = fuerza;
        tiempoBufferSalto = 0f;

        

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Trampolin"))
        {
            Salto(fuerzaTrampolin);
            ReproducirSonido(sonidoTrampolin, false,0.5f);
        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (audioSource.clip == sonidopasos)
            {
                
                audioSource.Stop();
            }

        }
    }

    private void ReproducirSonido(AudioClip clip, bool loop, float volumen)
    {
        if (clip !=null && audioSource !=null)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.volume = volumen;
            audioSource.Play();
        }
    }



    

}
