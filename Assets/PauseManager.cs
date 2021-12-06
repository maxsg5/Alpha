using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script is used to pause the game.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-05
public class PauseManager : MonoBehaviour
{
    #region Public Variables
    public GameObject pauseMenu; // The pause menu
    #endregion

    #region Private Variables
    private bool isPaused = false; // Is the game paused?
    #endregion
    
    #region Methods

    /// <summary>
    /// checks if the game is paused or not
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    void Update()
    {
        if(isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    /// <summary>
    /// Flips the pause bool
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void Pause()
    {
        isPaused = !isPaused;
    }

    /// <summary>
    /// This function will quit the game.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
