using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text txtScore;

    void Start()
    {
        if (GameManagerDemo.Instance != null)
        {
            int score = GameManagerDemo.Instance.GetScore();
            txtScore.text = "final score: " + score.ToString();
        }
        else
        {
            txtScore.text = "final score: 0";
        }
    }
}
