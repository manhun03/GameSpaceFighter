using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text txtScore;

    void Start()
    {
        if (ScoreKeeper.Instance != null)
            txtScore.text = "Final Score: " + ScoreKeeper.Instance.GetScore().ToString();
        else
            txtScore.text = "Final Score: 0";
    }
}
