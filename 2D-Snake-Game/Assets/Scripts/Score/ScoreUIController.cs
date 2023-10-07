using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private int scoreMultiplier = 1;
    private SnakeType activeSnake = SnakeType.Snake0;

    private void Awake()
    {
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
    public void SetActiveSnake(SnakeType snake)
    {
        activeSnake = snake;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (activeSnake == SnakeType.Snake0)
        {
            scoreText.text = "Score : " + score;
        }
        else if (activeSnake == SnakeType.Snake1)
        {
            scoreText.text = "Snake 1 : " + score;
        }
        else if (activeSnake == SnakeType.Snake2)
        {
            scoreText.text = "Snake 2 : " + score;
        }
    }
}