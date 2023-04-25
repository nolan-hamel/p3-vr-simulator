using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    // Assignable Values
    [SerializeField] List<carrotScript> carrotValues;
    [SerializeField] List<spawnCarrot> spawnCarrotValues;
    [SerializeField] List<rabbitScript> rabbitValues;
    [SerializeField] List<wolfScript> wolfValues;

    // Active Values
    public carrotScript activeCarrotValues;
    public spawnCarrot activeSpawnCarrotValues;
    public rabbitScript activeRabbitValues;
    public wolfScript activewolfValues;
    public int ActiveChallenge
    {
        get => _activeChallenge;
        set
        {
            // setting value
            _activeChallenge = value;
            // setting challenge active values
            activeCarrotValues = carrotValues[value];
            activeSpawnCarrotValues= spawnCarrotValues[value];
            activeRabbitValues = rabbitValues[value];
            activewolfValues = wolfValues[value];
        }
    }
    private int _activeChallenge;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
