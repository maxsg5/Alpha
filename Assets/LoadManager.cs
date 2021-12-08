using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public Slider progressBar; // Progress bar
    
    /// <summary>
    /// start the coroutine for loading the main scene
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-07
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// Loads the next scene asynchronously
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-07
    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1Prototype");
        while (!asyncLoad.isDone)
        {
            progressBar.value = asyncLoad.progress;
            yield return null;
        }
        progressBar.value = 1f;
    }
}
