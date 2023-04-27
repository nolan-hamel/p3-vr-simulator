using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    // Position adjustements
    const float RW_SPAWN_HEIGHT = 1.2f;
    const float C_SPAWN_HEIGHT = 0.2f;
    //[SerializeField] Vector3 challengeCenter = new Vector3(0, 210, 0);
    [SerializeField] float spawnRange = 100;

    private void Start()
    {

    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= carrotSpawnInterval)
        {
            timeSinceSpawn = 0;
            InstantiateOnMesh(carrotPrefab, C_SPAWN_HEIGHT);
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

        Invoke("SpawnInitialEntities", 2);
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

    private GameObject InstantiateOnMesh(GameObject prefab, float spawnHeightAdj)
    {
        Vector3 randomPoint = RandomPointInBounds(transform.GetComponent<MeshRenderer>().bounds, transform.position.y);
        randomPoint.y = 1000;
        RaycastHit hit;
        if (Physics.Raycast(randomPoint, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Spawnable")))
        {           
            Vector3 hitPosition = hit.point;
            hitPosition.y += spawnHeightAdj;
            GameObject instance = Instantiate(prefab, hitPosition, Quaternion.identity, manager.transform);
            if (prefab.CompareTag("Rabbit") || prefab.CompareTag("Wolf"))
                Debug.Log($"Instantating {prefab.tag} from point: {hitPosition}");
            return instance;
        }
        else
        {
            Debug.Log($"Failed to instantiate from point: {randomPoint}");
            return null;
        }
    }

    private void SpawnInitialEntities()
    {
        // Spawning Starting Counts
        for (int i = 0; i < rabbitStartingCount; i++)
        {
            GameObject rabbit = InstantiateOnMesh(rabbitPrefab, RW_SPAWN_HEIGHT);
            if (rabbit)
            {
                rabbit.GetComponent<rabbitScript>().female = true;
            }
            else
            {
                Debug.Log("Spawning of initial rabbit failed!");
            }

        }
        for (int i = 0; i < wolfStartingCount; i++)
        {
            GameObject wolf = InstantiateOnMesh(wolfPrefab, RW_SPAWN_HEIGHT);
            if (wolf)
            {
                wolf.GetComponent<wolfScript>().female = true;
            }
            else
            {
                Debug.Log("Spawning of initial wolf failed!");
            }

        }
    }
}
