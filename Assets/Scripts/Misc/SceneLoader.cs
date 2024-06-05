using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(int buildIndex)
    {
        int sceneToLoad = LoopBuildIndex(buildIndex);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void LoadNextScene()
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        sceneToLoad = LoopBuildIndex(sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    private int LoopBuildIndex(int buildIndex)
    {
        if (buildIndex >= SceneManager.sceneCountInBuildSettings || buildIndex < 0)
        {
            Debug.Log("Invalid build index, Loading scene 0");
            buildIndex = 0;
        }

        return buildIndex;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}