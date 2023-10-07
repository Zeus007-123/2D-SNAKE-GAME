using UnityEngine;

public enum Power
{
        Speed,
        Shield,
        Score
}

public class PowerUp : Consumable
{
        [SerializeField] private Power power;
        [SerializeField] private float activeTime;

        private void OnEnable()
        {
            SetPosition();
        }
        public override void Consume(SnakeController snake)
        {
            snake.ActivatePowerUp(power, activeTime);
            gameObject.SetActive(false);
        }
        protected override void ResetOnScreenTimer()
        {
            base.ResetOnScreenTimer();
            gameObject.SetActive(false);
        }
}