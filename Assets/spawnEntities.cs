using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spawnEntities : MonoBehaviour
{
    [SerializeField] ChallengeManager manager;

    // CRWs Values
    [SerializeField] GameObject carrotPrefab;
    [SerializeField] GameObject rabbitPrefab;
    [SerializeField] GameObject wolfPrefab;
    [SerializeField] float carrotSpawnInterval;
    [SerializeField] float rabbitStartingCount;
    [SerializeField] float wolfStartingCount;

    // Timing
    private float timeSinceSpawn = 0;

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= carrotSpawnInterval)
        {
            timeSinceSpawn = 0;
            InstantiateCarrot();
        }
    }

    private void OnEnable()
    {
        if (!manager)
        {
            manager = GameObject.Find("Challenge Manager").GetComponent<ChallengeManager>();
            Debug.Log("YOU FORGOT TO SET MANAGER YOU DOOFUS!");
        }


        // Setting Values
        carrotSpawnInterval = manager.activeChallenge.carrotSpawnInterval;
        rabbitStartingCount = manager.activeChallenge.rabbitStartingCount;
        wolfStartingCount = manager.activeChallenge.wolfStartingCount;

        // Spawning Starting Counts
        for(int i = 0; i < rabbitStartingCount; i++)
        {
            Vector3 randomPoint = RandomPointInBounds(transform.GetComponent<MeshRenderer>().bounds, 1);
            GameObject rabbit = Instantiate(rabbitPrefab, randomPoint, Quaternion.identity, manager.transform);
            rabbit.GetComponent<rabbitScript>().female = true;
        }
        for(int i = 0; i < wolfStartingCount; i++)
        {
            Vector3 randomPoint = RandomPointInBounds(transform.GetComponent<MeshRenderer>().bounds, 1);
            GameObject wolf = Instantiate(wolfPrefab, randomPoint, Quaternion.identity, manager.transform);
            wolf.GetComponent<wolfScript>().female = true;
        }
    }

    private void OnDisable()
    {
        timeSinceSpawn = 0;
    }

    private static Vector3 RandomPointInBounds(Bounds bounds, float spawnHeight)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            spawnHeight,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void InstantiateCarrot()
    {
        Vector3 randomPoint = RandomPointInBounds(transform.GetComponent<MeshRenderer>().bounds, 0.2f);
        Instantiate(carrotPrefab, randomPoint, Quaternion.identity, manager.transform);
    }
}
