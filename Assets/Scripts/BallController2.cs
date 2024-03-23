using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2 : MonoBehaviour
{
    public GameController gameController;
    public float fallSpeed = 8f; // ����� ������� �������� ������� ������
    public int scoreValue = 10; // ������ ����� �� ������������ � ����������

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

        // ������� �������� ������� ������ ��� ������
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -fallSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            gameController.AddScore(scoreValue); // ��������� ������ �����
            Destroy(gameObject); // ���������� ����� ��� ������������ � ����������
        }
        else if (other.CompareTag("Boundary"))
        {
            gameController.LoseLife();
            Destroy(gameObject); // ���������� ����� ��� ������������ � ��������
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            gameController.AddScore(scoreValue); // ��������� ������ �����
            Destroy(gameObject); // ���������� ����� ��� ������������ � ����������
        }
    }
}
