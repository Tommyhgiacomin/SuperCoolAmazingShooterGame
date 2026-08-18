using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void Awake()
    {

    }

    void Start()
    {

        AudioManager.Instance.stateMenu.SetValue();


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playButton.onClick.AddListener(Play);
        if (quitButton != null)
            quitButton.onClick.AddListener(Quit);

        AudioManager.Instance.musicEventGameplayStop.Post(gameObject);
        AudioManager.Instance.musicEventMenu.Post(gameObject);  

    }   

    void Play()
    {
        SceneManager.LoadScene("Game");
        AudioManager.Instance.uiClickEvent.Post(gameObject);

    }

    void Quit()
    {
        Application.Quit();
        AudioManager.Instance.uiClickEvent.Post(gameObject);
    }
}
