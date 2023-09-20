using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonHome;

    public GameObject GameOverScreen;
    public GameObject Score;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(RestartLevel);
        buttonHome.onClick.AddListener(GoHome);
    }
    public void SnakeDied()
    {
        //SoundManager.Instance.Play(Sounds.PlayerDeath);
        //SoundManager.Instance.Play(Sounds.DeathMusic);
        GameOverScreen.SetActive(true);
        Score.SetActive(false);
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