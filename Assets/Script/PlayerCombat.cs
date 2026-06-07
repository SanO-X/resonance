using UnityEngine;

public class PLayerCombat : MonoBehaviour
{
    [Header("Настройки атаки")]
    public LayerMask enemyLayers;  // Слой, на котором находятся враги
    public Transform attackPoint;  // Точка, откуда будет исходить атака
    public Animator animator;      // Ссылка на аниматор
    
    public float attackRange = 0.5f; // Радиус атаки
    public int attackDamge = 20;     // Урон врагам

    [Header("Кулдаун атаки (Управляется WorldManager)")]
    public float attackCooldown = 1.5f; // Время в секундах между атаками
    private float nextAttackTime = 0f;  // Таймер для проверки кулдауна

    void Update()
    {
        // Проверяем, прошло ли достаточно времени с момента прошлой атаки
        if (Time.time >= nextAttackTime)
        {
            // Только если кулдаун прошел, фиксируем нажатие ЛКМ
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                
                // Задаем время, раньше которого атаковать снова будет нельзя
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        // 1. Узнаем направление взгляда из Аниматора
        float lastX = animator.GetFloat("lastHorizontal");
        float lastY = animator.GetFloat("lastVertical");

        Vector2 attackDirection = new Vector2(lastX, lastY).normalized;

        // Страховка: если персонаж еще не двигался, бьем вниз
        if (attackDirection.magnitude == 0)
        {
            attackDirection = Vector2.down;
        }

        // 2. Сдвигаем точку атаки в сторону взгляда
        attackPoint.position = (Vector2)transform.position + attackDirection * 0.6f;

        // 3. Включаем триггер анимации атаки
        animator.SetTrigger("Attack");

        // ПЕРЕДАЕМ ИНФОРМАЦИЮ В WORLD MANAGER (Регистрируем удар для изменения мира)
        if (WorldManager.Instance != null)
        {
            WorldManager.Instance.RegisterPlayerAttack();
        }

        // 4. Собираем врагов в радиусе поражения
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Наносим урон каждому задетому врагу
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamge); 
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}