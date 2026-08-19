using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float gameDuration = 60f;

    public float TimeRemaining { get; private set; }
    public int Score { get; private set; }
    public bool IsGameActive { get; private set; }

    public event System.Action<int> OnScoreChanged;
    public event System.Action<float> OnTimeChanged;
    public event System.Action OnGameEnd;




    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    void Start()
    {
        StartGame();
        AudioManager.Instance.rtpcPlayerSpeed.SetValue(gameObject, 1f);
        AudioManager.Instance.ambienceEvent.Post(gameObject);
        AudioManager.Instance.musicEvent.Post(gameObject);
    }

    void Update()
    {
        if (!IsGameActive) return;

        TimeRemaining -= Time.deltaTime;
        OnTimeChanged?.Invoke(TimeRemaining);

        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            EndGame();
        }
    }

    public void StartGame()
    {
        Score = 0;
        TimeRemaining = gameDuration;
        IsGameActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void AddScore(int points)
    {
        if (!IsGameActive) return;
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    void EndGame()
    {
        AudioManager.Instance.footstepsPlayerStop.Post(gameObject);
        IsGameActive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnGameEnd?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.uiClickEvent.Post(gameObject);
        AudioManager.Instance.GameState(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.uiClickEvent.Post(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
