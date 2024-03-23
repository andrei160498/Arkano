using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameController gameController;
    public float fallSpeed = 5f; // Скорость падения шарика

    void Start()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
            if (gameController == null)
            {
                Debug.LogError("GameController not found!");
            }
        }

        // Добавим скорость падения шарика при старте
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -fallSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            gameController.AddScore(1);
            Destroy(gameObject); // Уничтожаем шарик при столкновении с платформой
        }
        else if (other.CompareTag("Boundary"))
        {
            gameController.LoseLife();
            Destroy(gameObject); // Уничтожаем шарик при столкновении с границей
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            gameController.AddScore(1);
            Destroy(gameObject); // Уничтожаем шарик при столкновении с платформой
        }
    }
}
