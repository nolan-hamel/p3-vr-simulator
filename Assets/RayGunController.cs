using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayGunController : MonoBehaviour
{
    // Gun Stats
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;

    // Configs
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask hitMask;

    private void Start()
    {
        lineRenderer.gameObject.SetActive(false);
    }

    public void Shoot ()
    {      
        // producing raycast
        Debug.Log("Shooting!");
        StartCoroutine(SpawnRay());
        RaycastHit hit;
        bool hits = Physics.Raycast(transform.position, -transform.right, out hit, range, hitMask);
        if (hits)
        {
            Debug.Log($"Hit: {hit.transform.name}");
            Target target = hit.transform.GetComponent<Target>();
            if (target != null )
            {
                target.Die();
            }
        }
        else
        {
            Debug.Log($"Miss...");
        }
        
    }

    public IEnumerator SpawnRay()
    {
        lineRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        lineRenderer.gameObject.SetActive(false);

    }

}
