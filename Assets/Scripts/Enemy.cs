using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Скорость врага
    private Transform target; // Цель, к которой движется враг (игрок)
    public int health = 3; // Здоровье врага
    public GameObject bloodSplatterPrefab; // Префаб кляксы крови
    public int damage = 5; // Урон врага
    public int bossLevel = 0; // Является ли враг боссом

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Поиск игрока по тегу
    }

    void Update()
    {
        if (target != null)
        {
            // Направление движения к игроку
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime; // Движение к игроку
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Уменьшение здоровья врага
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (bossLevel > 0)
        {
            // Появление кляксы крови
            SpawnBloodSplatter();
        }
        

        Destroy(gameObject); // Уничтожаем врага
    }

    void SpawnBloodSplatter()
    {
        if (bloodSplatterPrefab != null)
        {
            // Берем позицию префаба кляксы и инстанцируем в ней
            Instantiate(bloodSplatterPrefab, bloodSplatterPrefab.transform.position, Quaternion.identity);
        }
    }


    // Метод для обработки столкновения с игроком
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            Spawn heartSpawner = FindObjectOfType<Spawn>(); // Найти экземпляр Spawn

            if (playerHealth != null)
            {
                if (bossLevel>0)
                {
                    playerHealth.Die(); // Если враг - босс, игрок умирает
                }
                else
                {
                    playerHealth.TakeDamage(damage); // Наносим урон игроку
                    playerHealth.ApplyPoison(); // Накладываем эффект отравления
                    heartSpawner.SpawnHeart(); // Создаем сердечко
                }
                Die(); // Уничтожаем врага после атаки
            }
        }
    }

}
