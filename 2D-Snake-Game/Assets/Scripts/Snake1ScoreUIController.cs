using UnityEngine;
using TMPro;

public class Snake1ScoreUIController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        score = 0;
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        RefreshUI();
    }

    public void DecreaseScore(int decrement)
    {
        score -= decrement;
        if (score < 0)
        {
            score = 0;
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = "Snake 1 : " + score;
    }

}