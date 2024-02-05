using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static public GameController Instance { get; set; }
    [SerializeField] private Image timerImage;
    [SerializeField] private float gameTime;

    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("High Score Components")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    private int highScore;

    [Header("Gameplay Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] gameplayAudio;

    [Header("Game over components")]
    [SerializeField] private GameObject gameOverScreen;

    private float sliderCurrentFillAmount = 1f;

    private int score = 0;

    public enum GameState
    {
        Waiting,
        Playing,
        Over
    }

    public static GameState currentGameStatus;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }

        if(Instance == null)
            Instance = this;
        currentGameStatus = GameState.Waiting;
    }

    private void Update()
    {
        if(currentGameStatus == GameState.Playing)
        {
            AdjustTimer();
        }
    }

    private void AdjustTimer()
    {
        timerImage.fillAmount = sliderCurrentFillAmount - (Time.deltaTime / gameTime);
        sliderCurrentFillAmount = timerImage.fillAmount;

        if(sliderCurrentFillAmount <= 0f)
        {
            GameOver();
        }
    }

    public void UpdateScore(int points)
    {
        if (currentGameStatus != GameState.Playing)
            return;
        score += points;
        scoreText.SetText(score.ToString());
    }

    public void StartGame()
    {
        currentGameStatus = GameState.Playing;
        PlayGameAudio(gameplayAudio[1]);
    }
    public void ResetGame()
    {
        // reset the status
        currentGameStatus=GameState.Waiting;

        // reset the score
        score = 0;
        scoreText.SetText(score.ToString());

        // reset the timer
        sliderCurrentFillAmount = 1f;
        timerImage.fillAmount = sliderCurrentFillAmount;

        PlayGameAudio(gameplayAudio[0], true);
    }

    public void GameOver()
    {
        currentGameStatus = GameState.Over;

        // show the game over screen
        gameOverScreen.SetActive(true);

        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
        }

        PlayGameAudio(gameplayAudio[2], false);
    }

    private void PlayGameAudio(AudioClip clipToPlay, bool shouldLoop = true)
    {
        audioSource.clip = clipToPlay;
        audioSource.loop = shouldLoop;
        audioSource.Play();
    }
}
