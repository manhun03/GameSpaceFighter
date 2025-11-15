using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UIUpdater : MonoBehaviour
{
    [Header("UI Components")]
    public Image[] stars; // 5 ngôi sao
    public Sprite emptyStar;   
    public Sprite halfStar;    
    public Sprite fullStar;    
    public TextMeshProUGUI scoreText;  

    private int lastDisplayedScore = -1;
    private void Update()
    {
        if (ScoreKeeper.Instance == null) return;

        int currentScore = ScoreKeeper.Instance.GetScore();

        if (currentScore != lastDisplayedScore)
        {
            UpdateStars(currentScore);
            UpdateScoreText(currentScore);
            lastDisplayedScore = currentScore;
        }
    }

    private void UpdateStars(int score)
    {
        //int maxScore = 1000;
        int pointsPerFullStar = 200;
        int pointsPerHalfStar = 100;

        for (int i = 0; i < stars.Length; i++)
        {
            int starScoreThreshold = (i + 1) * pointsPerFullStar;

            if (score >= starScoreThreshold)
            {
                stars[i].sprite = fullStar;
            }
            else if (score >= starScoreThreshold - pointsPerHalfStar)
            {
                stars[i].sprite = halfStar;
            }
            else
            {
                stars[i].sprite = emptyStar;
            }
        }
    }

    private void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
