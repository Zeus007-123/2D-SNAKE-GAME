using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    private float nextUpdate;
    private bool canMoveLeft = false; //false because you start going to right side at the beginning of the game
    private bool canMoveUp = true;
    
    private float shieldExpirationTime;  // Time when the shield effect will expire
    private bool isShieldActive = false; // Flag to track if the shield is active
    private bool hasStartedShieldSpawn = false;
    private float nextShieldSpawnTime; // Time to spawn the next shield power-up
    
    private bool isScoreBoostActive = false; // Flag to track if the score boost is active
    private float scoreBoostExpirationTime;  // Time when the score boost effect will expire
    private int baseScoreIncrement = 10; // The base score increment without the power-up
    private int baseScoreDecrement = 7;
    private bool hasStartedScoreBoostSpawn = false;
    private float nextScoreBoostSpawnTime;

    private bool isSpeedUpActive = false; // Flag to track if the speed up is active
    private float speedUpExpirationTime;  // Time when the speed up effect will expire
    private float initialSpeed; // Store the initial speed of the snake
    private bool hasStartedSpeedUpSpawn = false;
    private float nextSpeedUpSpawnTime;

    float minX = -66f;
    float maxX = 66f;
    float minY = -35f;
    float maxY = 35f;

    public Transform segmentPrefab;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public int initialSize = 4;
    public bool moveThroughWalls = false;
    public Transform ShieldPowerUpPrefab;
    public Transform ScoreBoostPrefab;
    public Transform SpeedUpPrefab;
    public GameObject shieldIcon;  // Reference to the shield icon or "Shield Active" text
    public TextMeshProUGUI scoreBoostText;
    public TextMeshProUGUI speedUpText;
    public float shieldDuration = 5f;  // Duration of the shield effect in seconds
    public float shieldCooldown = 10f; // Cooldown period before the shield can spawn again
    public float initialShieldSpawnDelay = 10f;
    public float scoreBoostDuration = 5f;
    public float scoreBoostCooldown = 15f;
    public float initialScoreBoostSpawnDelay = 10f;
    public float speedUpDuration = 5f; // Duration of the speed up effect in seconds
    public float speedUpCooldown = 15f; // Cooldown period before the speed up can spawn again
    public float initialSpeedUpSpawnDelay = 20f; // Initial delay before the first speed up spawns

    public GameOverController gameOverController;
    public ScoreController scoreController;

    private void Start()
    {
        nextShieldSpawnTime = Time.time + initialShieldSpawnDelay;
        nextScoreBoostSpawnTime = Time.time + initialScoreBoostSpawnDelay;
        nextSpeedUpSpawnTime = Time.time + initialSpeedUpSpawnDelay;
        initialSpeed = speed; // Store the initial speed of the snake
        ResetState();
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canMoveUp == true){
            direction = Vector2.up;
            canMoveUp = false; //now the player cannot go up or down
            canMoveLeft = true; // so they have to go left or right to make canMoveUp true again
        } else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && canMoveUp == true) { 
            direction = Vector2.down;
            canMoveUp = false;
            canMoveLeft = true;
        } else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && canMoveLeft == true) { 
            direction = Vector2.left;
            canMoveUp = true; //now the player can go up or down
            canMoveLeft = false;
        } else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && canMoveLeft == true) { 
            direction = Vector2.right;
            canMoveUp = true;
            canMoveLeft = false;
        }

        if (!isShieldActive)
        {
            // Check if it's time to spawn the shield power-up
            if (!hasStartedShieldSpawn && Time.time >= nextShieldSpawnTime)
            {
                SpawnShieldPowerUp();
                hasStartedShieldSpawn = true;
            }
            else if (Time.time > nextShieldSpawnTime + shieldCooldown)
            {
                    SpawnShieldPowerUp();
            }
        }
        else
        {
            // Check if the shield has expired
            if (Time.time >= shieldExpirationTime)
            {
                DeactivateShield();
            }
        }

        if (!isScoreBoostActive)
        {
            // Check if it's time to spawn the score boost power-up
            if (!hasStartedScoreBoostSpawn && Time.time >= nextScoreBoostSpawnTime)
            {
                SpawnScoreBoostPowerUp();
                hasStartedScoreBoostSpawn = true;
            }
            else if (Time.time > nextScoreBoostSpawnTime + scoreBoostCooldown)
            {
                SpawnScoreBoostPowerUp();
            }
        }
        else
        {
            // Check if the score boost has expired
            if (Time.time >= scoreBoostExpirationTime)
            {
                DeactivateScoreBoost();
            }
        }

        if (!isSpeedUpActive)
        {
            // Check if it's time to spawn the speed up power-up
            if (!hasStartedSpeedUpSpawn && Time.time >= nextSpeedUpSpawnTime)
            {
                SpawnSpeedUpPowerUp();
                hasStartedSpeedUpSpawn = true;
            }
            else if (Time.time > nextSpeedUpSpawnTime + speedUpCooldown)
            {
                SpawnSpeedUpPowerUp();
            }
        }
        else
        {
            // Check if the speed up has expired
            if (Time.time >= speedUpExpirationTime)
            {
                DeactivateSpeedUp();
            }
        }
    }
    private void FixedUpdate()
    {
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
    private void SpawnShieldPowerUp()
    {
        Debug.Log(" Shield Power Up is Spawned");
        // Generate a random position within the grid area
        Vector3 spawnPosition = new Vector3 (Random.Range(minX, maxX),
                                            Random.Range(minY, maxY),
                                            0f);

        // Instantiate the shield power-up object at the spawn position
        // Ensure that your "ShieldPowerUpPrefab" is assigned in the Unity Inspector
        Transform powerup = Instantiate(ShieldPowerUpPrefab, spawnPosition, Quaternion.identity);

        // Set the next spawn time and flag the power-up as spawned
        Debug.Log("Time since last Shield spawn: " + (Time.time - nextShieldSpawnTime));
        nextShieldSpawnTime = Time.time + Random.Range(5f, 15f);

        // Automatically destroy the power-up object after a certain delay (e.g., 10 seconds)
        Destroy(powerup.gameObject, 7f); // Adjust the delay as needed
    }
    private void SpawnScoreBoostPowerUp()
    {
        Debug.Log(" Score Boost Power Up is Spawned");
        // Generate a random position within the grid area
        Vector3 newPosition = new Vector3(Random.Range(minX, maxX),
                                            Random.Range(minY, maxY),
                                            0f);

        // Instantiate the shield power-up object at the spawn position
        // Ensure that your "ScoreBoostPrefab" is assigned in the Unity Inspector
        Transform scorepowerup = Instantiate(ScoreBoostPrefab, newPosition, Quaternion.identity);

        // Set the next spawn time and flag the power-up as spawned
        Debug.Log("Time since last Score Boost spawn: " + (Time.time - nextScoreBoostSpawnTime));
        nextScoreBoostSpawnTime = Time.time + Random.Range(5f, 15f);

        // Automatically destroy the power-up object after a certain delay (e.g., 10 seconds)
        Destroy(scorepowerup.gameObject, 7f); // Adjust the delay as needed
    }
    private void SpawnSpeedUpPowerUp()
    {
        Debug.Log(" Speed Up Power Up is Spawned");
        // Generate a random position within the grid area
        Vector3 newPos = new Vector3(Random.Range(minX, maxX),
                                            Random.Range(minY, maxY),
                                            0f);

        // Instantiate the speed up power-up object at the spawn position
        // Ensure that your "SpeedUpPrefab" is assigned in the Unity Inspector
        Transform speedup = Instantiate(SpeedUpPrefab, newPos, Quaternion.identity);

        // Set the next spawn time and flag the power-up as spawned
        Debug.Log("Time since last Speed Up spawn: " + (Time.time - nextSpeedUpSpawnTime));
        nextSpeedUpSpawnTime = Time.time + Random.Range(5f, 15f);

        // Automatically destroy the power-up object after a certain delay (e.g., 10 seconds)
        Destroy(speedup.gameObject, 10f); // Adjust the delay as needed
    }
    private void CollectShieldPowerUp(GameObject powerUp)
    {
        Debug.Log(" Shield Power Up is Collected ");
        Debug.Log(" Shield Effect is Active ");
        // Deactivate the power-up object
        powerUp.SetActive(false);

        // Activate the shield effect
        isShieldActive = true;
        shieldIcon.SetActive(true);
        shieldExpirationTime = Time.time + shieldDuration;
    }
    private void CollectScoreBoostPowerUp(GameObject powerUp)
    {
        Debug.Log(" Score Boost Power Up is Collected ");
        Debug.Log(" Score Boost Effect is Active ");
        // Deactivate the power-up object
        powerUp.SetActive(false);

        // Activate the score boost effect
        isScoreBoostActive = true;
        scoreBoostExpirationTime = Time.time + scoreBoostDuration;
        // Activate the score boost effect
        scoreController.ActivateScoreBoost(); // Set score increment to 2 times
        scoreBoostText.gameObject.SetActive(true); // Show the "2X" text
    }
    private void CollectSpeedUpPowerUp(GameObject powerUp)
    {
        Debug.Log(" Speed Up Power Up is Collected ");
        Debug.Log(" Speed Up Effect is Active ");
        // Deactivate the power-up object
        powerUp.SetActive(false);

        // Activate the speed up effect
        isSpeedUpActive = true;
        speedUpText.gameObject.SetActive(true); // Show the "SPEED UP" text
        speedUpExpirationTime = Time.time + speedUpDuration;

        // Modify the snake's speed to make it faster
        speed *= 2f; // You can adjust the speed multiplier as needed
    }
    private void DeactivateShield()
    {
        Debug.Log(" Shield Effect is Deactivated ");
        isShieldActive = false;
        shieldIcon.SetActive(false);
    }
    private void DeactivateScoreBoost()
    {
        Debug.Log(" Score Boost Effect is Deactivated ");
        isScoreBoostActive = false;
        scoreController.DeactivateScoreBoost(); // Reset score increment to default
        scoreBoostText.gameObject.SetActive(false); // Hide the "2X" text
    }
    private void DeactivateSpeedUp()
    {
        Debug.Log(" Speed Up Effect is Deactivated ");
        isSpeedUpActive = false;
        speedUpText.gameObject.SetActive(false); // Hide the "SPEED UP" text

        // Restore the snake's initial speed
        speed = initialSpeed;
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

        // Reset the movement flags
        canMoveUp = true;
        canMoveLeft = false;
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
            Debug.Log(" Snake has Eaten the Mass Gainer Food ");
            scoreController.IncreaseScore(baseScoreIncrement);
            Grow();
        }
        else if (collision.gameObject.CompareTag("MassBurnerFood"))
        {
            if (segments.Count >= 5)
            {
                Debug.Log(" Snake has Eaten the Mass Burner Food ");
                scoreController.DecreaseScore(baseScoreDecrement);
                // Ensure the snake retains a minimum length of 3 segments
                Shrink(2);
                
            }
        }
        else if (!isShieldActive && collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log(" Snake Died");
            //gameOverController.SnakeDied();
            Invoke(nameof(Load_Scene), 0f);
            ResetState();
        }
        else if (collision.gameObject.CompareTag("ShieldPowerUp"))
        {
            // Collect the shield power-up
            CollectShieldPowerUp(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ScoreBoostPowerUp"))
        {
            // Collect the score boost power-up
            CollectScoreBoostPowerUp(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SpeedPowerUp"))
        {
            // Collect the speed up power-up
            CollectSpeedUpPowerUp(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (moveThroughWalls)
            {
                Traverse(collision.transform);
            }
            else
            {
                //gameOverController.SnakeDied();
                Invoke(nameof(Load_Scene), 0f);
                ResetState();
            }
        }
    }
    private void Load_Scene()
    {
        if (!isShieldActive)
        {
            Debug.Log(" Reloading Current Active Scene ");
            gameOverController.SnakeDied();
        }

    }
}
