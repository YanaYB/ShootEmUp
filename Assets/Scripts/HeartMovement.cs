using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    public float speed = 3f; // �������� �������� ��������

    void Update()
    {
        // �������� �������� �����
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ����������� �������� ��� ��������������� � ��������
        if (collision.CompareTag("Boundary")) 
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject); 
    }
}
