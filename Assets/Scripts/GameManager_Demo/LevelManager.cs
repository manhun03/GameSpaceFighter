using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Continue();
            else PauseGame();
        }
    }
    private void Awake()
    {
        // Đảm bảo game chạy bình thường khi start
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // Gọi từ nút Pause (trong game)
    public void PauseGame()
    {
        if (isPaused) return;
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    // Gọi từ nút Continue
    public void Continue()
    {
        if (!isPaused) return;
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game Continued");
    }
    //public void LoadGame()
    //{
    //    if (ScoreKeeper.Instance != null)
    //        ScoreKeeper.Instance.ResetScore();
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("Scene1 1");
    //}
    public void LoadMapSelection()
    {
        SceneManager.LoadScene("MapSelection");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", 2f));
    }
    public void SelectMap1()
    {
        SceneManager.LoadScene("Scene1 1");
    }
    public void SelectMap2()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void LoadGameWin()
    {
        StartCoroutine(WaitAndLoad("GameWin", 2f));
    }

    public void LoadMainMenu()
    {
        if (ScoreKeeper.Instance != null)
            ScoreKeeper.Instance.ResetScore();
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
