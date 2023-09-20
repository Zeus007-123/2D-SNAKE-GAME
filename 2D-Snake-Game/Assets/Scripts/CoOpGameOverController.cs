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
        //SoundManager.Instance.Play(Sounds.PlayerDeath);
        //SoundManager.Instance.Play(Sounds.DeathMusic);
        GameOverScreen.SetActive(true);
        isGameOver = true;
        Score1.SetActive(false);
        Score2.SetActive(false);
        
    }

    private void RestartLevel()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log(" Reloading Active Scene ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoHome()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log(" Going to Home Lobby Screen/Scene ");
        SceneManager.LoadScene(0);
    }
}