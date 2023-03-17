using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.AI;

public class rabbitScript : MonoBehaviour
{
    //public UnityEvent rabbitEatEvent;

    public float hungerFrequency;
    private float origHungerFrequency;
    public float hungerLevel;
    public float RabbitHungerValue = 50;
    public List<GameObject> huntable;
    public bool flag = false;
    private UnityEngine.AI.NavMeshAgent agent = null;
    private Vector3 dest;

    void Start()
    {
        origHungerFrequency = hungerFrequency;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        InvokeRepeating("Hunger", 0, hungerFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if(hungerFrequency != origHungerFrequency)
        {
            InvokeRepeating("Hunger", 0, hungerFrequency);
        }
        if (hungerLevel == 0)
        {
            Destroy(gameObject);
        }
        else if (hungerLevel < 80 && !flag)
        {
            SeekCarrot();
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void Hunger()
    {
        hungerLevel -= 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Carrot")
        {
            if(collision.transform.TryGetComponent(out carrotScript val))
            {
                hungerLevel = (hungerLevel + val.HungerValue) % 100;
                hungerFrequency = Random.Range(1, 5);
            }
            //rabbitEatEvent.Invoke();
            Destroy(collision.gameObject);
            huntable.Remove(collision.gameObject);
            flag = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Carrot")
        {
            huntable.Add(other.gameObject);
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Carrot")
        {
            huntable.Remove(other.gameObject);
            other.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }

    private void SeekCarrot()
    {
        Transform closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject t in huntable)
        {
            if(gameObject != null)
            {
                Vector3 diff = t.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = t.transform;
                    distance = curDistance;
                }
            }
            else
            {
                huntable.Remove(t);
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
