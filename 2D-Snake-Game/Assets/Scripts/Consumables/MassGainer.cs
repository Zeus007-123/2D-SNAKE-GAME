public class MassGainer : Food
{
        protected override void ConsumeEffect(SnakeController snake)
        {
            for (int i = 0; i < amount; i++)
            {
                snake.Grow();
                SoundManager.Instance.Play(Sounds.pickup);
            }
            if (snake.scoreController != null)
            {
                int score = Score;
                snake.scoreController.IncreaseScore(score);
            }
        }
}