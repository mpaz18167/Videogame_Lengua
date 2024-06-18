using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;

    public float shotForce = 1500f;
    public float shotRate = 0.5f;

    [SerializeField] private float shotRateTime = 0f;


    private void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject newBullet;
        newBullet=Instantiate(bullet,spawnPoint.position,spawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward* shotForce);
        shotRateTime = Time.time + shotRate;

        Destroy(newBullet,2f);
    }

   
}
