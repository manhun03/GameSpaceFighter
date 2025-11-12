using UnityEngine;
using UnityEngine.UI;
using TMPro; // N?u b?n dùng TextMeshPro

public class UIUpdater : MonoBehaviour
{
    [Header("UI Components")]
    public Image[] stars; // 5 ngôi sao
    public Sprite emptyStar;   // ngôi sao r?ng
    public Sprite halfStar;    // ngôi sao n?a sáng
    public Sprite fullStar;    // ngôi sao sáng ??y
    public TextMeshProUGUI scoreText; // N?u dùng Text th??ng thì thay b?ng "public Text scoreText;"

    private int lastDisplayedScore = -1;

    private void Update()
    {
        if (GameManagerDemo.Instance == null) return;

        int currentScore = GameManagerDemo.Instance.GetScore();

        // Ch? update n?u ?i?m thay ??i ?? t?i ?u
        if (currentScore != lastDisplayedScore)
        {
            UpdateStars(currentScore);
            UpdateScoreText(currentScore);
            lastDisplayedScore = currentScore;
        }
    }

    private void UpdateStars(int score)
    {
        int maxScore = 1000;
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
