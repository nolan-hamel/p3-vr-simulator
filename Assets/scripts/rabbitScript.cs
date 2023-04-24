using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.AI;
using UnityEngine.AI;
public class rabbitScript : MonoBehaviour
{
    //public UnityEvent rabbitEatEvent;

    public float hungerFrequency;
    private float prevHungerFrequency;
    private float hungerLevel;
    public float hungerOrigLevel;
    public float RabbitHungerValue;
    private NavMeshAgent agent = null;
    private Vector3 dest;
    public GameObject myPrefab;
    private int breedTimer;
    public int breedOrig;
    public float radius;
    [Range(0, 360)]
    public float angle;
    private LayerMask targetMask;
    public LayerMask obstructionMask;
    private Transform closest = null;
    //breeding and age stuff
    private bool female = true;
    private int age;
    public int maxAge;

    private void Awake()
    {
        breedTimer = breedOrig;
        hungerLevel = hungerOrigLevel;
        age = maxAge;
        int check = Random.Range(0, 2);
        if (check == 0) female = false;
        else female = true;

    }

    void Start()
    {
        prevHungerFrequency = hungerFrequency;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("Hunger", 0, hungerFrequency);
        InvokeRepeating("Timer", 0, 1);
        InvokeRepeating("Age", 0, 1);
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        if(hungerFrequency != prevHungerFrequency)
        {
            prevHungerFrequency = hungerFrequency;
            InvokeRepeating("Hunger", 0, hungerFrequency);
        }
        if (hungerLevel == 0)
        {
            Debug.Log("hunger level");   
            Destroy(this.gameObject);
        }
    }
    
    public void DestroySelf()
    {
        Debug.Log("destroySelf method");
        Destroy(this.gameObject);
    }

    //timers

    private void Hunger()
    {
        hungerLevel -= 1;
    }

    private void Timer()
    {
        breedTimer -= 1;
        if(breedTimer == 0 && hungerLevel >= 50 && female)
        {
            Transform n = this.transform;
            n.position = new Vector3(n.position.x, n.position.y, n.position.z + 3);
            Instantiate(myPrefab, n.position, Quaternion.identity);
            breedTimer = breedOrig;
        }
    }

    private void Age()
    {
        age -= 1;
        if(age == 0)
        {
            Debug.Log("age");
            DestroySelf();
        }
    }

    //hunting

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Carrot")
        {
            if (collision.transform.TryGetComponent(out carrotScript val))
            {
                hungerLevel = hungerLevel + val.HungerValue;
                if (hungerLevel > 100) hungerLevel = 100;
                hungerFrequency = Random.Range(1, 5);
            }
            //rabbitEatEvent.Invoke();
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if (hungerLevel < 90)
            {
                FieldOfViewCheck();
            } 
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks;
        targetMask = 1 << 6;
        rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            getClosest(rangeChecks);
            Seek();
        }
    }

    private void getClosest(Collider[] x)
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
    }

    private void Seek()
    {
        dest = new Vector3(closest.position.x, closest.position.y, closest.position.z);
        NavMeshHit hit;
        float distanceToCheck = 1;
        NavMesh.SamplePosition(dest, out hit, distanceToCheck, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
}

