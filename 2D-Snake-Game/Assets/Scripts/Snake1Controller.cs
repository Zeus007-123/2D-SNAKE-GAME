using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake1Controller : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    private float nextUpdate;
    private bool canMoveLeft = false; //false because you start going to right side at the beginning of the game
    private bool canMoveUp = true;

    public Transform segmentPrefab;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public int initialSize = 4;
    public bool moveThroughWalls = false;

    public CoOpGameOverController gameOverController;
    public Snake1ScoreUIController snake1scoreUIController;

    private void Start()
    {
       ResetState();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && canMoveUp == true){
            direction = Vector2.up;
            canMoveUp = false; //now the player cannot go up or down
            canMoveLeft = true; // so they have to go left or right to make canMoveUp true again
        } else if ((Input.GetKeyDown(KeyCode.DownArrow)) && canMoveUp == true) { 
            direction = Vector2.down;
            canMoveUp = false;
            canMoveLeft = true;
        } else if ((Input.GetKeyDown(KeyCode.LeftArrow)) && canMoveLeft == true) { 
            direction = Vector2.left;
            canMoveUp = true; //now the player can go up or down
            canMoveLeft = false;
        } else if ((Input.GetKeyDown(KeyCode.RightArrow)) && canMoveLeft == true) { 
            direction = Vector2.right;
            canMoveUp = true;
            canMoveLeft = false;
        }
    }

    private void FixedUpdate()
    {
        if(CoOpGameOverController.isGameOver)
        {
            return;
        }
        // Wait until the next update before proceeding
        if (Time.time < nextUpdate)
        {
            return;
        }

        Vector2 hpos = transform.position;

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        int x = (int)(Mathf.RoundToInt(transform.position.x) + (direction.x * 2));
        int y = (int)(Mathf.RoundToInt(transform.position.y) + (direction.y * 2));
        transform.position = new Vector2(x, y);

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 1; i--) 
        {
            segments[i].position = segments[i - 1].position;
        }

        segments[1].position = hpos;
        


        // Set the next update time based on the speed
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    private void Grow()
    {
           
        Vector3 newPos = Vector2.zero;
        
        newPos += segments[segments.Count - 1].position;

        Transform segment = Instantiate(segmentPrefab, newPos, Quaternion.identity);
        

        segments.Add(segment);
    }

    private void Shrink(int amount)
    {
        if (segments.Count > 3)
        {
            // Start removing segments from the end of the list
            for (int i = 0; i < amount && segments.Count > 3; i++)
            {
                int lastIndex = segments.Count - 1;
                Destroy(segments[lastIndex].gameObject);
                segments.RemoveAt(lastIndex);
            }
        }
    }

    private void ResetState()
    {
        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }

       
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    private void Traverse(Transform wall)
    {
        Vector3 position = transform.position;

        if (direction.x != 0f)
        {
            position.x = Mathf.RoundToInt(-wall.position.x + (direction.x * (3)));
        }
        else if (direction.y != 0f)
        {
            position.y = Mathf.RoundToInt(-wall.position.y + (direction.y * (3)));
        }

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("MassGainerFood"))
        {
            Debug.Log(" Snake 1 has Eaten the Mass Gainer Food ");
            snake1scoreUIController.IncreaseScore(10);
            Grow();
        }
        else if (collision.gameObject.CompareTag("MassBurnerFood"))
        {
            if (segments.Count >= 5)
            {
                Debug.Log(" Snake 1 has Eaten the Mass Burner Food ");
                snake1scoreUIController.DecreaseScore(7);
                // Ensure the snake retains a minimum length of 3 segments
                Shrink(2);
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Invoke(nameof(Load_Scene), 0f);
            ResetState();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (moveThroughWalls)
            {
                Traverse(collision.transform);
            }
            else
            {
                Invoke(nameof(Load_Scene), 0f);
                ResetState();
            }
        }
    }

    private void Load_Scene()
    {
        Debug.Log(" Reloading Current Active Scene ");
        gameOverController.SnakeDied();
        

    }
}
