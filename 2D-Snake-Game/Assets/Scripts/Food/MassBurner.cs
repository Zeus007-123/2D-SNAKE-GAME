public class MassBurner : Food
{
    protected override void ConsumeEffect(SnakeController snake)
    {
        for (int i = 0; i < amount; i++)
        {
            snake.Shrink();
            SoundManager.Instance.Play(Sounds.pickup);
        }
        // Call IncreaseScore on the snake's ScoreController
        if (snake.scoreController != null)
        {
            int score = GetScore();
            snake.scoreController.DecreaseScore(score);
        }
    }
}
