public class MassBurner : Food
{
        protected override void ConsumeEffect(SnakeController snake)
        {
            for (int i = 0; i < amount; i++)
            {
                snake.Shrink();
                SoundManager.Instance.Play(Sounds.pickup);
            }
            if (snake.scoreController != null)
            {
                int score = Score;
                snake.scoreController.DecreaseScore(score);
            }
        }
}