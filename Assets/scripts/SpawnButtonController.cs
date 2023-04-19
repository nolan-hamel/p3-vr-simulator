using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnButtonController : MonoBehaviour
{
    // Variables
    // Setup
    [SerializeField] GameObject spawnTarget;
    [SerializeField] BoxCollider boundsCollider;
    // config
    [SerializeField] private float spawnHeight = 0.1f;
    [SerializeField] private float distanceToCheck = 25f;
    

    public void SpawnCarrotRandomly()
    {
        Vector3 randomPoint;
        NavMeshHit hit;
        bool success;
        Debug.Log("Clicked Button");
        do
        {
            randomPoint = RandomPointInBounds(boundsCollider.bounds);
            success = NavMesh.SamplePosition(randomPoint, out hit, distanceToCheck, NavMesh.AllAreas); // returns a bool indicating success or failure
        } while (!success);
       
        Debug.Log("Spawn at " + randomPoint);
        randomPoint.y = spawnHeight;
        Instantiate(spawnTarget, randomPoint, Quaternion.identity);
    }



    private static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
