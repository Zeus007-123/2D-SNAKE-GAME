using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FoodController : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private SnakeController snakeController;
   

    private void Awake()
    {
        snakeController = FindObjectOfType<SnakeController>();
       
    }

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
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

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController snakeController = collision.gameObject.GetComponent<SnakeController>();

        if (collision.gameObject.CompareTag("Snake"))
        {
            snakeController.PickUpFood();
            RandomizePosition();
        }
    }
}
