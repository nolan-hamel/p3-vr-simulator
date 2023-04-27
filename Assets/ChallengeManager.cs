using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] List<Challenge> challenges;
    public Challenge activeChallenge;

    [SerializeField] spawnEntities spawnScript;

    // Timing
    [SerializeField] float runningTime = 0;
    [SerializeField] bool runningChallenge = false;

    // Position
    [SerializeField] GameObject player;
    [SerializeField] Vector3 menuLocation;
    [SerializeField] Vector3 challengeLocation;

    // Stats 
    [SerializeField] float popPollDelay = 1;  
    private float timeSincePopPoll;
    private List<PopulationStatsStruct> popStats;

    // Score
    [SerializeField] TextMeshProUGUI scoreDisplayText;
    public int activeScore = 0;

    void Start()
    {
        //DeactivateChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (runningChallenge)
        {
            // Time Limit
            runningTime += Time.deltaTime;
            if (runningTime >= activeChallenge.timeLimit)
            {
                DeactivateChallenge();
            }

            // population stats
            timeSincePopPoll += Time.deltaTime;
            if (timeSincePopPoll >= popPollDelay)
            {
                timeSincePopPoll = 0;
                PopulationStatsStruct stats = new PopulationStatsStruct();
                stats.rabbitPop = GameObject.FindGameObjectsWithTag("Rabbit").Length;
                stats.wolfPop = GameObject.FindGameObjectsWithTag("Wolf").Length;
                stats.carrotPop = GameObject.FindGameObjectsWithTag("Carrot").Length;
                popStats.Add(stats);

            }
        }
        
    }

    public void SetActiveChallenge(int index)
    {
        activeChallenge = challenges[index];
    }

    public void ActivateChallenge()
    {
        // Enabling entities
        spawnScript.enabled = true;

        // Timing
        runningTime = 0;
        runningChallenge = true;

        timeSincePopPoll = 0;

        // teleporting to challenge location
        player.transform.position = challengeLocation;

        // Scoring
        activeScore = 0;
        popStats = new List<PopulationStatsStruct>();
    }

    public void DeactivateChallenge()
    {
        // Deleting all the children
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Destroy(child);
        }
        spawnScript.enabled = false;

        // Timing
        runningTime = 0;
        runningChallenge = false;

        // teleporting to main menu
        player.transform.position = menuLocation;

        CalculateScore();
    }

    public void CalculateScore()
    {
        // Getting Ratios
        List<float> carrotRabbitRatios= new List<float>();
        List<float> rabbitWolfRatios = new List<float>();
        foreach(PopulationStatsStruct popStat in popStats)
        {
            float carrotRabbitRatio = popStat.carrotPop / popStat.rabbitPop;
            carrotRabbitRatios.Add(carrotRabbitRatio);
            float rabbitWolfRatio = popStat.rabbitPop / popStat.wolfPop;
            rabbitWolfRatios.Add(rabbitWolfRatio);
        }

        // Calculating total difference between ratios
        float carrotRabbitTotalDiff = 0;
        float rabbitWolfTotalDiff = 0;
        for (int i = 0; i < popStats.Count - 1; i++)
        {
            carrotRabbitTotalDiff += Mathf.Abs(carrotRabbitRatios[i] - carrotRabbitRatios[i + 1]);
            rabbitWolfTotalDiff += Mathf.Abs(rabbitWolfRatios[i] - rabbitWolfRatios[i + 1]);
        }

        // Calculating Score based on values
        activeScore = (int) Mathf.Ceil((carrotRabbitTotalDiff + rabbitWolfTotalDiff) * 10);
        scoreDisplayText.text = activeScore.ToString();

    }
}
