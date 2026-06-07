using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Настройки здоровья врага")]
    public int health = 100;

    [SerializeField] private int currentHealth;

    private bool isDead = false;

    void Start()
    {
        currentHealth = health;
        Debug.Log("Enemy spawned. HP = " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("ВРАГ ПОЛУЧИЛ УРОН: " + damage);
        Debug.Log("Текущее HP врага: " + currentHealth + " / " + health);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Enemy died!");

        EnemyAI enemyAI = GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }

        Collider2D enemyCollider = GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        Destroy(gameObject);
    }
}