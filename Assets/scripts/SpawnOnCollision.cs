using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private int numToSpawn = 1;
    [SerializeField] private float spawnVelocityThreshold = 10f;
    [SerializeField] private bool destroyOnSpawn = true;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private AudioClip SpawnAudio;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float shrapnelMass = 0.1f;
    [SerializeField] private float shrapnelDrag = 0.1f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= spawnVelocityThreshold)
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                // Calculate a random position within the specified radius
                Vector3 spawnPosition = collision.contacts[0].point + Random.insideUnitSphere * spawnRadius;
                spawnPosition.y += spawnRadius;
                // Spawn the prefab at the random position
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                // Apply an explosion force to the spawned prefab
                Rigidbody spawnedPrefabRigidbody = spawnedPrefab.GetComponent<Rigidbody>();
                // Set the mass and drag properties of the spawned prefab Rigidbody
                spawnedPrefabRigidbody.mass = shrapnelMass;
                spawnedPrefabRigidbody.drag = shrapnelDrag;

                // Calculate the direction of the explosion force
                Vector3 explosionDirection = (spawnedPrefab.transform.position - collision.contacts[0].point).normalized;

                // Apply an explosion force to the spawned prefab
                spawnedPrefabRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);

                AudioSource.PlayClipAtPoint(SpawnAudio,spawnPosition);

            }

            if (destroyOnSpawn)
            {
                Destroy(gameObject);
            }
        }
    }
}

