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

    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
        buttonQuit.onClick.AddListener(QuitGame);
        buttonSingle.onClick.AddListener(SingleMode);
        buttonCoOp.onClick.AddListener(CoOp);
    }
    private void PlayGame()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        ModeSelection.SetActive(true);
    }
    private void QuitGame()
    {
        Debug.Log(" Quit Application");
        SoundManager.Instance.Play(Sounds.buttonClick);
        Application.Quit();
    }
    private void SingleMode()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(1);
    }
    private void CoOp()
    {
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(2);
    }
}