using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class wolfScript : MonoBehaviour
{
    public float WolfHungerValue;
    public float hungerFrequency;
    public float hungerLevel;
    private UnityEngine.AI.NavMeshAgent agent = null;
    private Vector3 dest;
    Transform closest = null;
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeeRabbit;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("Hunger", 0, hungerFrequency);
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (hungerLevel == 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if(hungerLevel < 90)
            {
                FieldOfViewCheck();
            }
            
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = getClosest(rangeChecks);
            SeekRabbit(closest);
        }
    }

    private void Hunger()
    {
        hungerLevel -= 1;
    }

    //public UnityEvent wolfEatEvent;
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rabbit")
        {
            if (collision.transform.TryGetComponent(out rabbitScript val))
            {
                hungerLevel = (hungerLevel + val.RabbitHungerValue) % 100;
                hungerFrequency = Random.Range(1, 5);
            }
            //wolfEatEvent.Invoke();
            Destroy(collision.gameObject);

        }
    }

    private Transform getClosest(Collider[] x)
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Collider t in x)
        {
            Vector3 diff = t.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = t.transform;
                distance = curDistance;
            }
        }
        return closest;
    }


    private void SeekRabbit(Transform closest)
    {
        dest = new Vector3(closest.position.x, closest.position.y, closest.position.z);
        UnityEngine.AI.NavMeshHit hit;
        float distanceToCheck = 1;
        UnityEngine.AI.NavMesh.SamplePosition(dest, out hit, distanceToCheck, UnityEngine.AI.NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
}