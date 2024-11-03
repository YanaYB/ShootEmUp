using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject[] healthPrefabs; // ������ �������� ����������� ��� HP
    private GameObject currentHealthImage; // ������� ������ ����������� ��� HP

    private PlayerHealth playerHealth; // ������ �� PlayerHealth ��� ������������ ��������

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // �������� ������ �� PlayerHealth
        UpdateHPBar(); // �������������� HP ���
    }

    void Update()
    {
        UpdateHPBar(); // ��������� HP ��� � ������ �����
    }

    public void UpdateHPBar()
    {
        int currentHealth = playerHealth.currentHealth; // �������� ������� ��������
        int maxHealth = playerHealth.maxHealth; // ������������ ��������

        // ���������� ���������� ������ ����������� (���� ����)
        if (currentHealthImage != null)
        {
            Destroy(currentHealthImage);
        }
        int index = Mathf.FloorToInt((currentHealth / (float)maxHealth) * healthPrefabs.Length);
        index = Mathf.Clamp(index, 0, healthPrefabs.Length - 1); // ���������, ��� ������ �� ������� �� �������

        // ������� ����� ������ �����������
        currentHealthImage = Instantiate(healthPrefabs[index], healthPrefabs[index].transform.position, Quaternion.identity, transform);
    }
}
