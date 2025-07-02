using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float sceneLoadDelay = 1f;

    public void LoadMainMenu()
    {
        ScoreKeeper.Instance.ResetScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadHallOfFame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void LoadGameOver(float delay)
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }
    public void LoadWinScene()
    {
        StartCoroutine(WaitAndLoad("WinGame", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
