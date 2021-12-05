using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class is used to manage the inputs from the UI menu buttons.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-05
public class MenuManager : MonoBehaviour
{
    #region Public Variables
    public GameObject mainMenu; // The main menu.
    public GameObject instructionsMenu; // The instructions menu.
    public GameObject creditsMenu; // The credits menu.
    #endregion
    
    #region Methods

    /// <summary>
    /// When the game starts we want the main menu to be shown.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void Start()
    {
        mainMenu.SetActive(true); // Show the main menu.
        instructionsMenu.SetActive(false); // Hide the instructions menu.
        creditsMenu.SetActive(false); // Hide the credits menu.
    }


    /// <summary>
    /// Shows the instructions menu.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void ShowInstructions()
    {
        mainMenu.SetActive(false); // Hide the main menu.
        instructionsMenu.SetActive(true); // Show the instructions menu.
    }

    /// <summary>
    /// This function is used to take the player back to the main menu. regardless of which menu is currently active.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    /// <summary>
    /// This function transitions the player to the next scene which is the main game.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Shows the credits menu.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    public void ShowCredits()
    {
        mainMenu.SetActive(false); // Hide the main menu.
        creditsMenu.SetActive(true); // Show the credits menu.
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

    #endregion
}
