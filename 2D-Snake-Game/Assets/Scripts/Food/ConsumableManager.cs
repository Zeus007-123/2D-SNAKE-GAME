using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
    [Header("Grid Area")]
    [SerializeField] private BoxCollider2D gridArea;

    [Header("Snake Controller")]
    [SerializeField] private SnakeController snakeController;
    
    [Header("Food")]
    [SerializeField] private Food[] foods;

    [Header("Power Up")]
    [Range(0,10)]
    [SerializeField] private float spawnInterval;
    [SerializeField] private PowerUp[] powerUps;

    private float timer;
    private PowerUp lastSpawnedPowerUp;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        InitializeFood();
        InitializePowerUp();   
    }

    private void InitializePowerUp()
    {
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUps[i].SetManager(this);
            powerUps[i].gameObject.SetActive(false);
        }
    }

    private void SpawnPowerUp()
    {
        timer = 0;
        PowerUp newPowerUp;

        do
        {
            int range = Random.Range(0, powerUps.Length);
            newPowerUp = powerUps[range];
        } while (newPowerUp == lastSpawnedPowerUp);

        lastSpawnedPowerUp = newPowerUp;
        newPowerUp.gameObject.SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            SpawnPowerUp();
        }
    }

    private void InitializeFood()
    {
        for (int i = 0; i < foods.Length; i++)
        {
            foods[i].SetManager(this);
            foods[i].SetPosition();
        }
    }

    public Vector2 RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        // Pick a random position inside the bounds
        // Round the values to ensure it aligns with the grid
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        // Prevent the food from spawning on the snake
        while (snakeController.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;

                if (y > bounds.max.y)
                {
                    y = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }

        return new Vector2(x, y);
    }
}
