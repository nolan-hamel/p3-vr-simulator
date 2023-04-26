using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class carrotScript : MonoBehaviour
{
    // Public Config
    [SerializeField] ChallengeManager manager;
    [SerializeField] float agingFrequency = 1;

    // Manager Values
    [SerializeField] int maxAge = 100;
    [SerializeField] int age;
    public int hungerValue = 10;

    // Internal Values
    

    private void OnEnable()
    {
        // getting challenge manager
        if (!manager)
        {
            manager = transform.parent.GetComponent<ChallengeManager>();
        }
       
        // setting challenge manager values
        hungerValue = manager.activeChallenge.carrotHungerValue;
        maxAge = manager.activeChallenge.carrotMaxAge;

        age = maxAge;

        // Running Routines
        InvokeRepeating("Age", 0, agingFrequency);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void Age()
    {
        age--;
        if (age == 0)
        {
            DestroySelf();
        }
    }
}
