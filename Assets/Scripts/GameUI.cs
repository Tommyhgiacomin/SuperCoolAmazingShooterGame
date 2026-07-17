using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI weaponText;

    [Header("End Screen")]
    public GameObject endScreenPanel;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;
    public Button mainMenuButton;

    [Header("Crosshair")]
    public GameObject crosshair;

    WeaponSystem weaponSystem;

    void Start()
    {
        endScreenPanel.SetActive(false);

        GameManager gm = GameManager.Instance;
        gm.OnScoreChanged += UpdateScore;
        gm.OnTimeChanged += UpdateTimer;
        gm.OnGameEnd += ShowEndScreen;

        restartButton.onClick.AddListener(() => gm.RestartGame());
        mainMenuButton.onClick.AddListener(() => gm.LoadMainMenu());

        weaponSystem = FindFirstObjectByType<WeaponSystem>();

        UpdateScore(0);
        UpdateTimer(gm.gameDuration);
    }

    void Update()
    {
        if (weaponSystem != null && weaponText != null)
            weaponText.text = weaponSystem.CurrentBulletType.ToString();
    }

    void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimer(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        timerText.text = seconds.ToString();
    }

    void ShowEndScreen()
    {
        endScreenPanel.SetActive(true);
        finalScoreText.text = "Final Score\n" + GameManager.Instance.Score;
        if (crosshair != null) crosshair.SetActive(false);
    }
}
