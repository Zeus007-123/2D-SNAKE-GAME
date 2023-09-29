public class MassGainer : Food
{
    protected override void ConsumeEffect(SnakeController snake)
    {
        for (int i = 0; i < amount; i++)
        {
            snake.Grow();
            SoundManager.Instance.Play(Sounds.pickup);
        }
        // Call IncreaseScore on the snake's ScoreController
        if (snake.scoreController != null)
        {
            int score = GetScore();
            snake.scoreController.IncreaseScore(score);
        }
    }
}
