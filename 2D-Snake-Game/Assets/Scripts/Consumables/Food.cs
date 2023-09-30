using UnityEngine;

public abstract class Food : Consumable
{
        [SerializeField] protected int amount;
        [SerializeField] private int score;
        public override void Consume(SnakeController snake)
        {
            ConsumeEffect(snake);
            SetPosition();
        }
        public int Score => score;
        protected override void ResetOnScreenTimer()
        {
            base.ResetOnScreenTimer();
            SetPosition();
        }
        protected abstract void ConsumeEffect(SnakeController snake);
}