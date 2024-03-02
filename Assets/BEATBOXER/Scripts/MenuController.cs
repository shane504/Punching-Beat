using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void LoadGame()
    {
        StartCoroutine(StartNextScene(1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        StartCoroutine(StartNextScene(0));
    }

    public void GameLose()
    {
        StartCoroutine(StartNextScene(3));
    }

    public void WinGame()
    {
        StartCoroutine(StartNextScene(2));
    }

    IEnumerator StartNextScene(int levelIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
