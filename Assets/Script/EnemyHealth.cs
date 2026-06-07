using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30; // ХП мыши (сделай поменьше, на 1-2 удара)
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Этот метод ОБЯЗАТЕЛЬНО должен быть public и называться строго TakeDamage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Мышь получила урон! Осталось жизней: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Мышь сдохла!");
        Destroy(gameObject); // Полностью удаляет мышь со сцены
    }
}