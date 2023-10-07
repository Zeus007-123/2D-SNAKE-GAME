using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    [RequireComponent(typeof(BoxCollider2D))]
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private SoundManager audiocontroller;
        private Vector2 direction = Vector2.right;
        private List<Transform> segments = new List<Transform>();
        private float nextUpdate;
        private bool canMoveLeft = false; 
        private bool canMoveUp = true;
        private bool isShieldActive = false; 
        private float initialSpeed; 

        [SerializeField] private int snakeNumber;

        [Header("Keys")]
        [SerializeField] private KeyCode upKey;
        [SerializeField] private KeyCode downKey;
        [SerializeField] private KeyCode leftKey;
        [SerializeField] private KeyCode rightKey;

        [SerializeField] private Transform segmentPrefab;
        [SerializeField] private float speed = 20f;
        [SerializeField] private float speedMultiplier = 1f;
        [SerializeField] private int initialSize = 4;
        [SerializeField] private bool moveThroughWalls = false;

        [SerializeField] private GameObject shieldIcon;  
        [SerializeField] private TextMeshProUGUI scoreBoostText;
        [SerializeField] private TextMeshProUGUI speedUpText;

        public ScoreController scoreController;
        public GameOverController gameOverController;

        private void Start()
        {
            initialSpeed = speed;
            ResetState();
        }
        private void Update()
        {
            if (Input.GetKeyDown(upKey) && canMoveUp == true)
            {
                direction = Vector2.up;
                canMoveUp = false; 
                canMoveLeft = true; 
            }
            else if (Input.GetKeyDown(downKey) && canMoveUp == true)
            {
                direction = Vector2.down;
                canMoveUp = false;
                canMoveLeft = true;
            }
            else if (Input.GetKeyDown(leftKey) && canMoveLeft == true)
            {
                direction = Vector2.left;
                canMoveUp = true;
                canMoveLeft = false;
            }
            else if (Input.GetKeyDown(rightKey) && canMoveLeft == true)
            {
                direction = Vector2.right;
                canMoveUp = true;
                canMoveLeft = false;
            }
        }
        private void FixedUpdate()
        {
            if (Time.time < nextUpdate)
            {
                return;
            }

            Vector2 hpos = transform.position;

            int x = (int)(Mathf.RoundToInt(transform.position.x) + (direction.x * 2));
            int y = (int)(Mathf.RoundToInt(transform.position.y) + (direction.y * 2));
            transform.position = new Vector2(x, y);

            for (int i = segments.Count - 1; i > 1; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            segments[1].position = hpos;

            nextUpdate = Time.time + (1f / (speed * speedMultiplier));
        }
        public void Grow()
        {
            Vector3 newPos = Vector2.zero;
            newPos += segments[segments.Count - 1].position;
            Transform segment = Instantiate(segmentPrefab, newPos, Quaternion.identity);
            segments.Add(segment);
        }
        public void Shrink()
        {
            if (segments.Count <= 3)
                return;
    
            int lastIndex = segments.Count - 1;
            Destroy(segments[lastIndex].gameObject);
            segments.RemoveAt(lastIndex);
        }
        public void ActivatePowerUp(Power power, float activeTime)
        {
            if (power == Power.Speed)
            {
                SoundManager.Instance.Play(Sounds.pickup);
                speed *= 2f;
                speedUpText.gameObject.SetActive(true);
            }
            else if (power == Power.Shield)
            {
                SoundManager.Instance.Play(Sounds.pickup);
                isShieldActive = true;
                shieldIcon.SetActive(true);
            }
            else
            {
                SoundManager.Instance.Play(Sounds.pickup);
                scoreController.ActivateScoreBoost();
                scoreBoostText.gameObject.SetActive(true);
            }

            StartCoroutine(PowerUpActive(power, activeTime));
        }
        private IEnumerator PowerUpActive(Power power, float time)
        {
            yield return new WaitForSeconds(time);
            DeactivatePowerUp(power);
        }
        private void DeactivatePowerUp(Power power)
        {
            if (power == Power.Speed)
            {
                speed = initialSpeed;
                speedUpText.gameObject.SetActive(false);
            }
            else if (power == Power.Shield)
            {
                isShieldActive = false;
                shieldIcon.SetActive(false);
            }
            else
            {
                scoreController.DeactivateScoreBoost();
                scoreBoostText.gameObject.SetActive(false);
            }
        }
        private void ResetState()
        {
            // Start at 1 to skip destroying the head
            for (int i = 1; i < segments.Count; i++)
            {
                Destroy(segments[i].gameObject);
            }

            segments.Clear();
            segments.Add(transform);

            // -1 since the head is already in the list
            for (int i = 0; i < initialSize - 1; i++)
            {
                Grow();
            }
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
            if (!isShieldActive && collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log(" Snake Died");
                SoundManager.Instance.Play(Sounds.gameOver);
                Load_Scene();
                ResetState();
            }
            if (collision.gameObject.CompareTag("Wall"))
            {
                if (moveThroughWalls)
                {
                    Traverse(collision.transform);
                }
                else
                {
                    Load_Scene();
                    ResetState();
                }
            }
            if (collision.TryGetComponent<Consumable>(out var consumable))
            {
                CameraShake.Instance.ShakeCamera();
                consumable.Consume(this);
            }
        }
        private void Load_Scene()
        {
        if (!isShieldActive)
        { 
            Debug.Log(" Reloading Current Active Scene ");

            SnakeType snakeType = SnakeType.Snake0;
            gameOverController.SnakeDied(snakeType);
        }
        }
}