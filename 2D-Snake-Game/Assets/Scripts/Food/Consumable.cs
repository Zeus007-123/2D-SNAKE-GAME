using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    [Header("Consumables Manager")]
    [SerializeField] private ConsumableManager manager;
    [Header("Consumables Time On Screen")]
    [SerializeField] private float timeOnScreen;
    private float timer;

    private void Start()
    {
        timer = 0;
    }

    public void SetManager(ConsumableManager manager) => this.manager = manager;
    public void SetPosition() => transform.position = manager.RandomizePosition();

    // single consumable script
    protected virtual void Update()
    {
        UpdateOnScreenTimer();
    }

    public abstract void Consume(SnakeController snake);

    public void UpdateOnScreenTimer()
    {
        timer += Time.deltaTime;
        if (timer > timeOnScreen)
        {
            ResetOnScreenTimer();
        }
        // time the object is displayed on screen. 
        // if time over then change position
    }

    protected virtual void ResetOnScreenTimer()
    {
        timer = 0;
    }
}
