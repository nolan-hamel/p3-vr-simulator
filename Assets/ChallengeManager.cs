using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] List<Challenge> challenges;
    public Challenge activeChallenge;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveChallenge(int index)
    {
        activeChallenge = challenges[index];
    }
}
