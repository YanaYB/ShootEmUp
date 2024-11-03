using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ������ �������� ������ (������� ���� � �����)
    public Transform[] spawnPoints; // ������ ����� ������ ������ (������ � ������)
    public float initialSpawnDelay = 0.5f; // �������� ����� ������ �������
    public float basicEnemySpawnInterval = 2f; // �������� ������ ������� ������
    private float timeToSpawn; // ������ ��� ������ ������
    private float bossSpawnTime; // ������ ��� ������ ������
    private int bossCounter = 0; // ������� ������
    public GameObject heartPrefab;

    void Start()
    {
        // ��������� ������� ������ ������� ������
        timeToSpawn = Time.time + initialSpawnDelay;
        bossSpawnTime = Time.time + 5f; // ������ ���� �������� ����� 30 ������
    }

    void Update()
    {
        // ������ ������ ������� ������
        if (Time.time >= timeToSpawn)
        {
            SpawnBasicEnemy(); // ������� �������� �����
            timeToSpawn = Time.time + basicEnemySpawnInterval; // ������������� ��������� ����� �������� �����
        }

        // ������ ������ ������
        if (bossCounter < 3 && Time.time >= bossSpawnTime)
        {
            SpawnBoss();
            bossSpawnTime += GetNextBossSpawnInterval(); // ������������� ����� ��� ���������� �����
        }
    }

    void SpawnBasicEnemy()
    {
        // �������� ��������� ����� ������
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ������� �������� ����� � ��������� ����� ������
        Instantiate(enemyPrefabs[0], randomSpawnPoint.position, Quaternion.identity);
    }

    void SpawnBoss()
    {
        GameObject bossPrefab = enemyPrefabs[bossCounter + 1]; // �������� ������ ��� �������� �����
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(bossPrefab, randomSpawnPoint.position, Quaternion.identity);
        bossCounter++; // ����������� ������� ������
    }

    public void SpawnHeart()
    {
        if (heartPrefab != null)
        {
            // �������� ��������� ����� ������
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(heartPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("�������� �� ��������� � ����������!");
        }
    }

    float GetNextBossSpawnInterval()
    {
        switch (bossCounter)
        {
            case 1: return 5f; // ������ ���� ����� 60 ������
            case 2: return 10f; // ������ ���� ����� 90 ������
            default: return 0f; // ��� ������� �� ����������, ��� ��� bossCounter �� ����� ���� ������ 2
        }
    }
}
