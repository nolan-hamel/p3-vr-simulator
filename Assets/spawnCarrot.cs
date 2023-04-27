using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spawnCarrot : MonoBehaviour
{
    [SerializeField] ChallengeManager manager;

    public GameObject myPrefab;
    public GameObject plane;
    public float carrotSpawnInterval;

    private void Start()
    {
        InvokeRepeating("instantiatePrefab", 0, carrotSpawnInterval);
    }

    private void OnEnable()
    {
        carrotSpawnInterval = manager.activeChallenge.carrotSpawnInterval;
    }

    private static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            1,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void instantiatePrefab()
    {
        Vector3 randomPoint = RandomPointInBounds(plane.GetComponent<MeshRenderer>().bounds);
        Instantiate(myPrefab, randomPoint, Quaternion.identity, manager.transform);
    }
}
