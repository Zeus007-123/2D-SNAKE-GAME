using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score = 0;
    private int scoreMultiplier = 1; // Default multiplier is 1

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        score = 0;
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        score += increment * scoreMultiplier; // Multiply the increment by the scoreMultiplier
        RefreshUI();
    }
    public void ActivateScoreBoost()
    {
        scoreMultiplier = 2; // Set the score multiplier to 2
        RefreshUI();
    }

    public void DeactivateScoreBoost()
    {
        scoreMultiplier = 1; // Set the score multiplier back to 1
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
        scoreText.text = "Score : " + score;
    }

    

}