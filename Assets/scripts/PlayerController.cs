using UnityEngine;
using System.Collections;
using Unity.AI;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent = null;
    private Bounds floorRange;
    private Vector3 dest;
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        floorRange = GameObject.Find("Plane").GetComponent<Renderer>().bounds;
        RandDest();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
            RandDest();
        }
    }

    private void RandDest()
    {
        float rx = Random.Range(floorRange.min.x, floorRange.max.x);
        float rz = Random.Range(floorRange.min.z, floorRange.max.z);
        dest = new Vector3(rx, this.transform.position.y, rz);
        //agent.SetDestination(dest);
        NavMeshHit hit;
        float distanceToCheck = 10;
        if (NavMesh.SamplePosition(dest, out hit, distanceToCheck, NavMesh.AllAreas))
        {
            NavMeshHit check;
            if (NavMesh.SamplePosition(transform.position, out check, 1.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                Debug.Log("Agent not on mesh!");
            }
        }
        else
        {
            Debug.Log($"Sample Position failed! for {transform.name}");
        }
        
    }
}
