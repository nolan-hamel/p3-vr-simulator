using UnityEngine;
using System.Collections;
using Unity.AI;

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
        floorRange = GameObject.Find("TestPlane").GetComponent<Renderer>().bounds;
        randDest();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath == false && flag == false)
        {
            flag = true;
            randDest();
        }
    }

    private void randDest()
    {
        float rx = Random.Range(floorRange.min.x, floorRange.max.x);
        float rz = Random.Range(floorRange.min.z, floorRange.max.z);
        dest = new Vector3(rx, this.transform.position.y, rz);
        //agent.SetDestination(dest);
        flag = false;
        UnityEngine.AI.NavMeshHit hit;
        float distanceToCheck = 10;
        UnityEngine.AI.NavMesh.SamplePosition(dest, out hit, distanceToCheck, UnityEngine.AI.NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
}
