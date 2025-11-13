using TMPro;
using UnityEngine;

public class GameWinUI : MonoBehaviour
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
