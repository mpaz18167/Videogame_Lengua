using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float changeDirectionInterval = 2f;
    [SerializeField] private float minDirectionChange = 0.5f; //Evitar Atascos
    [SerializeField] private float boundaryBuffer = 0.05f; //Buffer para limites de pantalla
    [SerializeField] private float escapeTime = 5f; //Tiempo antes de escapar por arriba
    [SerializeField] private float destroyDelay = 2f; //Tiempo antes de destruirse despues de escapar

    [SerializeField] private Camera mainCamera;

    [SerializeField] public int puntos = 10;
    [SerializeField] public int vidas = 3;
    [SerializeField] private bool pregCorrect;
    [SerializeField] private string nombreEscena;




    private Vector3 moveDirection;
    private float changeDirectionTimer;
    private float lastShootTime;
    private bool escaping;

    private UI ui;




    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        ChangeDirection(true);
        lastShootTime = Time.time;
        escaping = false;

        mainCamera = FindObjectOfType<Camera>();
        
        

        ui = FindObjectOfType<UI>();
        if (ui == null)
        {
            Debug.LogError("No se encontro el componente UI");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!escaping)
        {
            MoveDuck();
            UpdateDirectionChange();
            CheckBoundaries();

        }
        CheckEscape();

    }

    private void ChangeDirection(bool forceChange = false)
    {
        Vector3 newDirection;
        do
        {
            newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;

        } while (!forceChange && Vector3.Distance(moveDirection, newDirection) < minDirectionChange);

        moveDirection = newDirection;
        changeDirectionTimer = Random.Range(changeDirectionInterval * 0.5f, changeDirectionInterval * 1.5f);


    }

    private void MoveDuck()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

    }

    private void UpdateDirectionChange()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0f)
        {
            ChangeDirection(true);


        }
    }

    private void CheckBoundaries()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        if (screenPos.x < boundaryBuffer || screenPos.x > 1f - boundaryBuffer ||
            screenPos.y < boundaryBuffer || screenPos.y > 1f - boundaryBuffer ||
            transform.position.y < boundaryBuffer)
        {
            ChangeDirection(true);

            Vector3 clampedPosition = mainCamera.ViewportToWorldPoint(new Vector3(
                Mathf.Clamp(screenPos.x, boundaryBuffer, 1 - boundaryBuffer),
                Mathf.Clamp(screenPos.y, boundaryBuffer, 1 - boundaryBuffer),
                screenPos.z
            ));
            clampedPosition.y = Mathf.Max(clampedPosition.y, 0f);
            transform.position = clampedPosition;
        }


    }

    private void CheckEscape()
    {
        if (Time.time - lastShootTime > escapeTime)
        {
            escaping = true;
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            Destroy(gameObject, destroyDelay);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnDuckShoot(pregCorrect);

            Collider bulletCollider = collision.gameObject.GetComponent<Collider>();
            bulletCollider.enabled = false;
            

        }
    }


    private void OnDuckShoot(bool estadoCorrecto)
    {

        if (estadoCorrecto)
        {
            IncrementarPuntos();
            
            Debug.Log("Le diste +10 punto");
        }
        else
        {
            DecrementarVidas();
            
            Debug.Log("Perdista -1 vida");

        }

        Destroy(gameObject);
        lastShootTime = Time.time;


    }

    private void IncrementarPuntos()
    {
        FindAnyObjectByType<UI>()?.IncrementarPuntos(puntos);
    }

    private void DecrementarVidas()

    {
        FindObjectOfType<UI>()?.DecrementarVidas();
    }
   
}
