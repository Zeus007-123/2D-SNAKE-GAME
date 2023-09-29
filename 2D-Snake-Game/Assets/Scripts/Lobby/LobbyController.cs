using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [Header("Play Button")]
    [SerializeField] private Button buttonPlay;
    [Header("Quit Button")]
    [SerializeField] private Button buttonQuit;
    [Header("SingleMode Button")]
    [SerializeField] private Button buttonSingle;
    [Header("CoOpMode Button")]
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
        Time.timeScale = 0.7f;
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(1);
    }

    private void CoOp()
    {
        Time.timeScale = 0.7f;
        SoundManager.Instance.Play(Sounds.buttonClick);
        SceneManager.LoadScene(2);
    }
}