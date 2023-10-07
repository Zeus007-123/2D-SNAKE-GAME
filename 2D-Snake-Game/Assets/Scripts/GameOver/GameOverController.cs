using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SnakeType
{
        Snake0,
        Snake1,
        Snake2 
}
public class GameOverController : MonoBehaviour
{
    public GameObject GameOverScreen;

    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonHome;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(RestartLevel);
        buttonHome.onClick.AddListener(GoHome);
    }
    public void SnakeDied(SnakeType snake)
    {
         if (snake == SnakeType.Snake0)
         {
             GameOverScreen.SetActive(true);
         }
         else if (snake == SnakeType.Snake1)
         {
             GameOverScreen.SetActive(true);
         }
         else if (snake == SnakeType.Snake2)
         {
             GameOverScreen.SetActive(true);
         }
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