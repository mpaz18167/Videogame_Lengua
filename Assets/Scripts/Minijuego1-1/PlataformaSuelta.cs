
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaSuelta : MonoBehaviour
{

    private float esperaParaCaer = 0.4f;
    private float esperaParaDestruir = 2f;
    private float esperaParaReaparecer = 2.0f;
    private Rigidbody rb;
    private Vector3 antiguaPosicion;

    private Animator animacion;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        antiguaPosicion = this.gameObject.transform.position;
        animacion =this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Caida());
        }
    }

    private IEnumerator Caida()
    {
        yield return new WaitForSeconds(esperaParaCaer);
        rb.useGravity = enabled;
        yield return new WaitForSeconds(esperaParaDestruir);
        this.gameObject.SetActive(false);
        Invoke("Reaparecer", esperaParaReaparecer);

    }

    private void Reaparecer()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = antiguaPosicion;
        rb.useGravity = false;
        rb.velocity = new Vector3(0, 0, 0);

        animacion.SetBool("Reaparece", true);
    }

}
