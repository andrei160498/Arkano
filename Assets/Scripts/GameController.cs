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
    private int currentLevel = 1; // ������� ����
    private int ballsSpawned = 0; // ���������� ��������� �����

    void Start()
    {
        // �������� ����������� ������
        lives = PlayerPrefs.GetInt("Lives", 3);
        score = PlayerPrefs.GetInt("Score", 0);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        finalScore = PlayerPrefs.GetInt("FinalScore", 0); // �������� ���������� �����

        // ���������� ����������
        UpdateUI();

        // �������� ���� � ��������� �������� ���������
        SpawnBall();
        platformRb = GetComponent<Rigidbody2D>();

        // ���������� ������� �������
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

        // ���������, ��������� �� ����� ����� �� ������
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
        finalScore = score; // ���������� ���������� �����
        PlayerPrefs.SetInt("FinalScore", finalScore); // ���������� ���������� �����
        gameOverScene.SetActive(true); // ��������� ����� ��������� ����
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("FinalScore");
        currentLevel = 1; // ����� �������� ������
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
