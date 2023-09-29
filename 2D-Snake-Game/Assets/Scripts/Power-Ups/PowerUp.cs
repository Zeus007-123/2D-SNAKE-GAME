using UnityEngine;

public enum Power
{
    Speed,
    Shield,
    Score
}

public class PowerUp : Consumable
{
    [Header("Type Of Power-Up")]
    [SerializeField] private Power power;
    [Header("Power-Up Active Time")]
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
