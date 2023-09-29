using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public int snakeNumber; // Add a player number to distinguish between Snake1 and Snake2
    [Header("Restart Button")]
    [SerializeField] private Button buttonRestart;
    [Header("Home Button")]
    [SerializeField] private Button buttonHome;

    public GameObject GameOverScreen;
 
    private void Awake()
    {
        buttonRestart.onClick.AddListener(RestartLevel);
        buttonHome.onClick.AddListener(GoHome);
    }
    public void SnakeDied()
    {
        if (snakeNumber == 0)
        {
            GameOverScreen.SetActive(true);
        }
        else if (snakeNumber == 1)
        {
            // Snake1 died, implement game over for Snake1
            GameOverScreen.SetActive(true);
        }
        else if (snakeNumber == 2)
        {
            // Snake2 died, implement game over for Snake2
            GameOverScreen.SetActive(true);
        }
    }

    private void RestartLevel()
    {
        Debug.Log(" Reloading Active Scene ");
        SoundManager.Instance.Play(Sounds.buttonClick);
        // Restart the game by adjusting Time.timeScale as needed
        Time.timeScale = 0.7f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoHome()
    {
        Debug.Log(" Going to Home Lobby Screen/Scene ");
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(0);
    }
}