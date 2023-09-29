using UnityEngine;

public abstract class Food : Consumable
{
    [Header("Grow/Shrink Amount Of Snake Segements")]
    [SerializeField] protected int amount;
    [Header("Score To Increase/Deacrease")]
    [SerializeField] private int score;
    public override void Consume(SnakeController snake)
    {
        ConsumeEffect(snake);
        SetPosition();
    }
    public int GetScore() => score;

    protected override void ResetOnScreenTimer()
    {
        base.ResetOnScreenTimer();
        SetPosition();
    }
    protected abstract void ConsumeEffect(SnakeController snake);
}
