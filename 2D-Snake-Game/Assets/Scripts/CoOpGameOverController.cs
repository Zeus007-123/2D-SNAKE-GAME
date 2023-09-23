using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoOpGameOverController : MonoBehaviour
{
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonHome;

    public GameObject GameOverScreen;
    public GameObject Score1;
    public GameObject Score2;
    public static bool isGameOver = false;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(RestartLevel);
        buttonHome.onClick.AddListener(GoHome);
    }

    private void Start()
    {
        isGameOver = false;
        GameOverScreen.SetActive(false);
    }
    public void SnakeDied()
    {
        GameOverScreen.SetActive(true);
        isGameOver = true;
        
    }

    private void RestartLevel()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoHome()
    {
        Debug.Log(" Going to Home Lobby Screen/Scene ");
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(0);
    }
}