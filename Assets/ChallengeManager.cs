using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float wolfHungerLim;
    [SerializeField] float wolfHungerRate;
    [SerializeField] float wolfBreedingLim;
    [SerializeField] float wolfAgeLim;
    [SerializeField] float wolfSpeed;

    [SerializeField] float rabbitHungerLim;
    [SerializeField] float rabbitHungerRate;
    [SerializeField] float rabbitBreedingLim;
    [SerializeField] float rabbitAgeLim;
    [SerializeField] float rabbitSpeed;

    [SerializeField] float carrotBreedingRate;
    [SerializeField] float carrotAgeLim;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToNextScene(int sceneId)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneId);
    }
}
