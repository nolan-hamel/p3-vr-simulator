using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // description: Loads the main game
    public void PlayGame()
    {
        // Load next scence in the queue
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // description: Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }



}
