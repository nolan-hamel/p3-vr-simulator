using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class wolfScript : MonoBehaviour
{
    public float WolfHungerValue;
    public float hungerFrequency;
    public float hungerLevel;
    public List<GameObject> huntable;
    private bool flag = false;
    private UnityEngine.AI.NavMeshAgent agent = null;
    private Vector3 dest;
    Transform closest = null;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("Hunger", 0, hungerFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if (hungerLevel == 0)
        {
            Destroy(gameObject);
        }
        else if (hungerLevel < 50 && !flag)
        {
            SeekRabbit();
        }
        else if (flag)
        {
            dest = new Vector3(closest.position.x, closest.position.y, closest.position.z);
            UnityEngine.AI.NavMeshHit hit;
            float distanceToCheck = 1;
            UnityEngine.AI.NavMesh.SamplePosition(dest, out hit, distanceToCheck, UnityEngine.AI.NavMesh.AllAreas);
            agent.SetDestination(hit.position);
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
            //wolfEatEvent.Invoke();
            Destroy(collision.gameObject);
            flag = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rabbit")
        {
            huntable.Add(other.gameObject);
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rabbit")
        {
            huntable.Remove(other.gameObject);
            other.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }

    private void SeekRabbit()
    {
        
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject t in huntable)
        {
            Vector3 diff = t.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = t.transform;
                distance = curDistance;
            }
        }
        dest = new Vector3(closest.position.x, closest.position.y, closest.position.z);
        UnityEngine.AI.NavMeshHit hit;
        float distanceToCheck = 1;
        UnityEngine.AI.NavMesh.SamplePosition(dest, out hit, distanceToCheck, UnityEngine.AI.NavMesh.AllAreas);
        agent.SetDestination(hit.position);
        flag = true;
    }
}