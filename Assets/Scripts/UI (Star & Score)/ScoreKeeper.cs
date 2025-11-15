using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ điểm khi đổi scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int scoreToAdd)
    {
        score += scoreToAdd;
        score = Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log($"🏆 Điểm hiện tại: {score}");

        // Kiểm tra gọi boss
        if (GameManagerDemo.Instance != null)
        {
            GameManagerDemo.Instance.CheckForBossSpawn(score);
        }
    }

    public void ResetScore()
    {
        score = 0;
        Debug.Log("ScoreKeeper đã reset điểm về 0");
    }
}
