using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] List<Challenge> challenges;
    public Challenge activeChallenge;

    [SerializeField] spawnEntities spawnScript;

    // Timing
    [SerializeField] float runningTime = 0;
    [SerializeField] bool runningChallenge = false;

    // Teleporting (Moving Around)
    [SerializeField] GameObject playerReference;
    [SerializeField] Transform menuLocation;
    [SerializeField] Transform challengeLocation;
    [SerializeField] const float MENU_X_ADJ = 1.625f;
    [SerializeField] const float MENU_Y_ADJ = 1;
    [SerializeField] const float MENU_Z_ADJ = -0.625f;

    // Stats 
    [SerializeField] float popPollDelay = 1;  
    private float timeSincePopPoll;
    private List<PopulationStatsStruct> popStats;

    // Score
    [SerializeField] TextMeshProUGUI scoreDisplayText;
    public int activeScore = 0;

    void Start()
    {
 
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
        // moving 
        challengeLocation.position = new Vector3(playerReference.transform.position.x, playerReference.transform.position.y - 1, playerReference.transform.position.z);
        menuLocation.position = new Vector3(1000, 1000, 1000);
        MeshRenderer mesh = menuLocation.GetComponent<MeshRenderer>();
        if (mesh)
        {
            mesh.ResetBounds();
        }

        // Enabling entities
        spawnScript.enabled = true;

        // Timing
        runningTime = 0;
        runningChallenge = true;

        timeSincePopPoll = 0;

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
        menuLocation.position = new Vector3(playerReference.transform.position.x + MENU_X_ADJ, playerReference.transform.position.y + MENU_Y_ADJ, playerReference.transform.position.z + MENU_Z_ADJ);
        challengeLocation.position = new Vector3(1000, 1000, 1000);


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
