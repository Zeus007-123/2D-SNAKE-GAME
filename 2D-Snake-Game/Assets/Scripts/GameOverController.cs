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
        GameOverScreen.SetActive(true);
        Score.SetActive(false);
    }

    private void RestartLevel()
    {
        Debug.Log(" Reloading Active Scene ");
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