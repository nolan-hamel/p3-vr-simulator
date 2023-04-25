using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.AI;
using UnityEngine.AI;
public class wolfScript : MonoBehaviour
{

    // Internally managed levels
    private int hungerLevel;
    private int breedingLevel;
    private int age;

    // Navigation
    private NavMeshAgent agent = null;
    private Vector3 dest;
    private LayerMask targetMask;
    private Transform closest = null;

    [SerializeField] LayerMask obstructionMask;
    [SerializeField] float radius;

    // unused
    //[Range(0, 360)]
    //public float angle;


    // Breeding
    private bool female = true;

    [SerializeField] GameObject offspringPrefab;

    

    // Settable Levels
    [SerializeField] int hungerStartingLevel = 100;
    [SerializeField] int maxAge = 200;
    [SerializeField] int breedingStartingLevel;
    public int WolfHungerValue= 100; // value provided when eaten

    private float hungerFrequency = 1;
    //private float prevHungerFrequency;
    private float breedingFrequency = 1;
    private float agingFrequency = 1;



    private void Awake()
    {
        breedingLevel = breedingStartingLevel;
        hungerLevel = hungerStartingLevel;
        age = maxAge;
        int check = Random.Range(0, 2);
        if (check == 0) female = false;
        else female = true;

    }

    void Start()
    {
        //prevHungerFrequency = hungerFrequency;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("Hunger", 0, hungerFrequency);
        InvokeRepeating("Breed", 0, breedingFrequency);
        InvokeRepeating("Age", 0, agingFrequency);
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        //if (hungerFrequency != prevHungerFrequency)
        //{
        //    prevHungerFrequency = hungerFrequency;
        //    InvokeRepeating("Hunger", 0, hungerFrequency);
        //} 
    }

    public void DestroySelf()
    {
        Debug.Log("destroySelf method");
        Destroy(this.gameObject);
    }

    // Timers

    private void Hunger()
    {
        hungerLevel -= 1;
        if (hungerLevel == 0)
        {
            Debug.Log("hunger level");
            DestroySelf();
        }
    }

    private void Breed()
    {
        breedingLevel -= 1;
        if (breedingLevel == 0 && hungerLevel >= 50 && female)
        {
            Transform n = this.transform;
            n.position = new Vector3(n.position.x, n.position.y, n.position.z + 3);
            Instantiate(offspringPrefab, n.position, Quaternion.identity);
            breedingLevel = breedingStartingLevel;
        }
    }

    private void Age()
    {
        age -= 1;
        if (age == 0)
        {
            Debug.Log("age");
            DestroySelf();
        }
    }

    // Hunting

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rabbit")
        {
            if (collision.transform.TryGetComponent(out rabbitScript val))
            {
                hungerLevel = hungerLevel + val.hungerValue;
                if (hungerLevel > 100) hungerLevel = 100;
                hungerFrequency = Random.Range(1, 5);
            }
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if (hungerLevel < 60)
            {
                FieldOfViewCheck();
            }
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks;
        targetMask = 1 << 7;
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

