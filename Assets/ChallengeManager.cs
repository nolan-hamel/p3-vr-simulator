using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] List<Challenge> challenges;
    public Challenge activeChallenge;

    [SerializeField] List<GameObject> entities;
    [SerializeField] spawnCarrot carrotSpawnScript;

    void Start()
    {
        DeactivateChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveChallenge(int index)
    {
        activeChallenge = challenges[index];
    }

    public void ActivateChallenge()
    {
        foreach (GameObject entity in entities)
        {
            entity.SetActive(true);
        }
        carrotSpawnScript.enabled = true;
    }

    public void DeactivateChallenge()
    {
        foreach (GameObject entity in entities)
        {
            entity.SetActive(false);
        }
        carrotSpawnScript.enabled = false;
    }
}
