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
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        ModeSelection.SetActive(true);
    }

    private void QuitGame()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log(" Quit Application");
        Application.Quit();
    }

    private void SingleMode()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    private void CoOp()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(2);
    }
}