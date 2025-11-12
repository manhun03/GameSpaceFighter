using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadGame()
    {
        if (GameManagerDemo.Instance != null)
            GameManagerDemo.Instance.score = 0;
        SceneManager.LoadScene("Scene1 1");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", 2f));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
