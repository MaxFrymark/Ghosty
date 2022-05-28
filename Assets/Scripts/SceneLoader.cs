using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
