using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Button restartButton;
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public GameObject gameOverScene;
    public Text finalScoreText;
    private int lives;
    private int score;
    private int bestScore;
    private int finalScore;
    private bool gameOver = false;
    public float platformSpeed = 5f;
    public float platformBoundary = 2.5f;
    private Rigidbody2D platformRb;
    private int currentLevel = 1; // Уровень игры
    private int ballsSpawned = 0; // Количество созданных шаров

    void Start()
    {
        // Загрузка сохраненных данных
        lives = PlayerPrefs.GetInt("Lives", 3);
        score = PlayerPrefs.GetInt("Score", 0);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Загрузка финального счета

        // Обновление интерфейса
        UpdateUI();

        // Создание мяча и установка скорости платформы
        SpawnBall();
        platformRb = GetComponent<Rigidbody2D>();

        // Назначение функций кнопкам
        restartButton.onClick.AddListener(RestartGame);
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
        PlayerPrefs.SetInt("Score", score);

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        // Проверяем, достигнут ли лимит шаров на уровне
        if (score >= 10 * currentLevel && ballsSpawned < currentLevel)
        {
            SpawnBall();
        }
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0 && !gameOver)
        {
            GameOver();
        }
        else
        {
            UpdateUI();
            SpawnBall();
            PlayerPrefs.SetInt("Lives", lives);
        }
    }

    void SpawnBall()
    {
        ballsSpawned++;
        Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }

    void GameOver()
    {
        gameOver = true;
        finalScore = score; // Сохранение финального счета
        PlayerPrefs.SetInt("FinalScore", finalScore); // Сохранение финального счета
        gameOverScene.SetActive(true); // Активация сцены окончания игры
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("FinalScore");
        currentLevel = 1; // Сброс текущего уровня
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        platformRb.velocity = new Vector2(moveInput * platformSpeed, 0f);

        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -platformBoundary, platformBoundary);
        transform.position = currentPosition;
    }
}
