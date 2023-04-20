using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class carrotScript : MonoBehaviour
{
    public float HungerValue = 10;
    [SerializeField] BoxCollider boundsCollider;
    public float spawnFrequency;

    void Start()
    {
        InvokeRepeating("Spawn", 10, spawnFrequency);
    }

    private void Update()
    {
    
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void Spawn()
    {
        Vector3 randomPoint;
        randomPoint = RandomPointInBounds(boundsCollider.bounds);
        Instantiate(this, randomPoint, Quaternion.identity);
    }
}
