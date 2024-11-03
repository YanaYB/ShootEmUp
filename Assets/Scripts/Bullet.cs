using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // ��������� �������� ����
    public float lifetime = 2f; // ����� ����� ����
    public int damage = 10; // ����, ������� ������� ����
    public LayerMask whatIsSolid; // ����, � ������� ���� ����� �����������

    private void Start()
    {
        // ���������� ���� ����� ������������ �����
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // ����������� ����
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // �������� �� ������������ � �������������
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                EnemyMovement enemy = hitInfo.collider.GetComponent<EnemyMovement>(); // �������� �����, ������� ��� �������
                enemy.TakeDamage(damage); // ������� ���� �����
                Destroy(gameObject); // ���������� ���� ����� ���������

                // ������ �� ����� �������� ������� ����� ��������������� �� �����
                int level = enemy.bossLevel;

                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                playerHealth.OnBossKilled(level); // �������� ������� �����
            }
            else if (hitInfo.collider.CompareTag("Heart"))
            {
                // �������� ������ �� PlayerHealth
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(10); // ����� ������ �� 10 ������
                    playerHealth.StopPoison(); // ������������� �� ��� ��������� ������
                }

                hitInfo.collider.GetComponent<HeartMovement>().Die();
                Destroy(gameObject); // ���������� ���� ����� ���������
            }
            else
            {
                Destroy(gameObject); // ���������� ���� ��� ������������ � ����� ������ ��������
            }
        }
    }
}
