using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoOpFoodController : MonoBehaviour
{
    public BoxCollider2D gridArea;

    public Snake1Controller snake1Controller;
    public Snake2Controller snake2Controller;
   

    private void Awake()
    {
        snake1Controller = FindObjectOfType<Snake1Controller>();
        snake2Controller = FindObjectOfType<Snake2Controller>();
       
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
        while (snake1Controller.Occupies(x, y)
            && snake2Controller.Occupies(x, y))
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
        Snake1Controller snake1Controller = collision.gameObject.GetComponent<Snake1Controller>();
        Snake2Controller snake2Controller = collision.gameObject.GetComponent<Snake2Controller>();

        if (collision.gameObject.CompareTag("Snake1"))
        {
            snake1Controller.PickUpFood();
            RandomizePosition();
        }
        else if (collision.gameObject.CompareTag("Snake2"))
        {
            snake2Controller.PickUpFood();
            RandomizePosition();
        }
    }
}
