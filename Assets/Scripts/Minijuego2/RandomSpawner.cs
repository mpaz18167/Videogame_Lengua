using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Collider spawnArea;

    [Header ("Obejetivos Correctos")]
    [SerializeField] GameObject[] PositivePointsToSpawn;

    [Header("Objetivos Incorrectos")]
    [SerializeField] GameObject[] NegativePointsToSpawn;

    [Header("Variables")]
    [Range(0f, 1f)]
    [SerializeField] private float NPChance = 0.05f;

    [SerializeField] private float minSpawnDelay = 0.25f;
    [SerializeField] private float maxSpawnDelay = 1f;
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while(enabled)
        {
            GameObject prefab = PositivePointsToSpawn[Random.Range(0,PositivePointsToSpawn.Length)];
            if (Random.value<NPChance)
            {
                prefab = NegativePointsToSpawn[Random.Range(0, NegativePointsToSpawn.Length)];
            }

            Vector3 position = new Vector3(
                Random.Range(spawnArea.bounds.min.x,spawnArea.bounds.max.x), 
                spawnArea.bounds.center.y,
                spawnArea.bounds.center.z
                );
            Instantiate(prefab, position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }
    }
}
