using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }


    public Button playButton;
    public Button quitButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AudioManager.Instance.GameState(0);
    }

    void Start()
    {

        AudioManager.Instance.musicEvent.Post(gameObject);
        AudioManager.Instance.footstepsPlayerStop.Post(gameObject);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playButton.onClick.AddListener(Play);
        if (quitButton != null)
            quitButton.onClick.AddListener(Quit); 

    }   

    void Play()
    {
        AudioManager.Instance.uiClickEvent.Post(gameObject);
        AudioManager.Instance.GameState(1);
        SceneManager.LoadScene("Game");
    }

    void Quit()
    {
        AudioManager.Instance.uiClickEvent.Post(gameObject);
        Application.Quit();
    }
}
