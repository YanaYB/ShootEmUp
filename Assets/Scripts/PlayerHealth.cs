using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    private bool isPoisoned = false;
    private float poisonDamageInterval = 1f;
    private float nextPoisonDamageTime = 0f;
    public int score = 0; // ������� ���� ������
    public TextMeshProUGUI scoreText;
    [SerializeField] private Score currentScore= null;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateScoreUI();
    }

    void Update()
    {
        // ��������� ���
        if (isPoisoned && Time.time >= nextPoisonDamageTime)
        {
            TakeDamage(1);
            nextPoisonDamageTime = Time.time + poisonDamageInterval;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
        FindObjectOfType<HPBar>().UpdateHPBar(); // ��������� HP ���
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StopPoison(); // ������������� �� ��� ��������� �������
        UpdateHealthUI();
        FindObjectOfType<HPBar>().UpdateHPBar(); // ��������� HP ���
    }

    public void ApplyPoison()
    {
        isPoisoned = true;
    }

    public void StopPoison()
    {
        isPoisoned = false;
    }

    public void Die()
    {
        
        Debug.Log("����� �����!");
        SceneManager.LoadSceneAsync(2); // �������� ����� ���������

    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void LoseScore(int amount)
    {
        score -= amount;
        UpdateScoreUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null && currentHealth >= 0)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // ����� ��� ��������� �������� 
    public void OnBossKilled(int bossLevel)
    {
        switch (bossLevel) { 
            case 0: AddScore(10); break;
            case 1: AddScore(30); break;
            case 2: AddScore(60); break;
            case 3: 
                AddScore(90);
                ShowVictoryScreen();
                break;
        }
    }

    // ����� ��� �������� ������
    private void ShowVictoryScreen()
    {
        currentScore.score.Add(score);
        currentScore.WriteToFile("scoreData.txt");
        Debug.Log("������! ��� ����� �����.");
        SceneManager.LoadSceneAsync(3); // �������� ����� ������ (������� ���� ����� �����)
    }

}
