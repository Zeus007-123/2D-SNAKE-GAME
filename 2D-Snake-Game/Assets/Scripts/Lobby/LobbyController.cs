using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonSingle;
    [SerializeField] private Button buttonCoOp;

    public GameObject ModeSelection;
    public void PlayGame()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        ModeSelection.SetActive(true);
    }
    public void QuitGame()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        Application.Quit();
    }
    public void SingleMode()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(1);
    }
    public void CoOp()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(2);
    }
}