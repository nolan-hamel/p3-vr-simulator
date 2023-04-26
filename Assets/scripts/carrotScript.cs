using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class carrotScript : MonoBehaviour
{

    [SerializeField] ChallengeManager manager;
    public int hungerValue = 10;


    private void Awake()
    {
        // getting challenge manager
        manager = transform.parent.GetComponent<ChallengeManager>();

        // setting challenge manager values
        hungerValue = manager.activeChallenge.rabbitHungerValue;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
