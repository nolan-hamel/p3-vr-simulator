using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.AI;
using UnityEngine.AI;
public class rabbitScript : MonoBehaviour
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

    [SerializeField] float detectionRadius;
    //[Range(0, 360)] // unused
    //public float angle;
    public LayerMask obstructionMask;

    private bool female = true;

    [SerializeField] GameObject offspringPrefab;

    // Challenge Manager Fields
    [SerializeField] ChallengeManager manager;

    // Settable Levels
    [SerializeField] int breedingHungerRequirement = 50;
    [SerializeField] int hungerStartingLevel = 100;
    [SerializeField] int breedingStartingLevel = 10;
    [SerializeField] int maxAge = 200;
    public int hungerValue = 10; // amount of hunger provided by eating

    // Frequencies (unchangeable publicly because starting levels basically do that
    private float breedingFrequency = 1;
    private float agingFrequency = 1;
    private float hungerFrequency = 1;


    private void Awake()
    {
        // getting challenge manager
        manager = transform.parent.GetComponent<ChallengeManager>();

        // assigning starting values based on challenge manager
        breedingHungerRequirement = manager.activeChallenge.rabbitBreedingHungerRequirement;
        hungerStartingLevel = manager.activeChallenge.rabbitHungerStartingLevel;
        maxAge = manager.activeChallenge.rabbitMaxAge;
        breedingStartingLevel = manager.activeChallenge.rabbitBreedingStartingLevel;
        hungerValue = manager.activeChallenge.rabbitHungerValue;

        // assign starting levels
        breedingLevel = breedingStartingLevel;
        hungerLevel = hungerStartingLevel;
        age = maxAge;

        // setting gender
        int check = Random.Range(0, 2);
        if (check == 0) female = false;
        else female = true;

    }

    void Start()
    {
        // navigation
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        // setting up repeating routines
        InvokeRepeating("Hunger", 0, hungerFrequency);
        if (female)
            InvokeRepeating("Breed", 0, breedingFrequency);
        InvokeRepeating("Age", 0, agingFrequency);

        StartCoroutine(FOVRoutine());
    }

    void Update()
    {

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
        // die if no hunger
        if (hungerLevel == 0)
        {
            Debug.Log("hunger level");
            DestroySelf();
        }
    }

    private void Breed()
    {
        breedingLevel -= 1;
        // reproduce if requirements right
        if(breedingLevel <= 0 && hungerLevel >= breedingHungerRequirement && female)
        {
            Transform n = this.transform;
            n.position = new Vector3(n.position.x, n.position.y, n.position.z + 3);
            Instantiate(offspringPrefab, n.position, Quaternion.identity, manager.transform);
            breedingLevel = breedingStartingLevel;
            Debug.Log($"{transform.name} reproduced!");
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

    // Hunting

    private void OnCollisionEnter(Collision collision)
    {
        // checking if something to eat
        if (collision.gameObject.CompareTag("Carrot"))
        {
            if (collision.transform.TryGetComponent(out carrotScript val))
            {
                hungerLevel += val.hungerValue;
                if (hungerLevel > hungerStartingLevel) hungerLevel = hungerStartingLevel;
                //hungerFrequency = Random.Range(1, 5);
            }
            //rabbitEatEvent.Invoke();
            Destroy(collision.gameObject);
            Debug.Log($"{transform.name} ate a carrot!");
        }
    }

    // Navigation

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
        rangeChecks = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
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

