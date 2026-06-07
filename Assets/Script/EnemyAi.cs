using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Настройки движения")]
    public float speed = 2.5f;
    public float chaseRadius = 5f;
    public float attackRadius = 1.5f;

    [Header("Настройки атаки")]
    public int damage = 10;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;

    private Transform player;
    private PlayerHealth playerHealth;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();

            Debug.Log("EnemyAI: Player найден: " + playerObj.name);

            if (playerHealth != null)
            {
                Debug.Log("EnemyAI: PlayerHealth найден на Player");
            }
            else
            {
                Debug.LogError("EnemyAI: Player найден, но на нем НЕТ PlayerHealth!");
            }
        }
        else
        {
            Debug.LogError("EnemyAI: объект с тегом Player НЕ найден!");
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Состояние 1: Мышь летит за игроком
        if (distanceToPlayer <= chaseRadius && distanceToPlayer > attackRadius)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            if (anim != null)
            {
                anim.SetFloat("speed", speed);
            }

            // Разворот спрайта
            if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
            else if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }
        // Состояние 2: Мышь рядом и атакует
        else if (distanceToPlayer <= attackRadius)
        {
            if (anim != null)
            {
                anim.SetFloat("speed", 0f);
            }

            if (Time.time >= nextAttackTime)
            {
                Debug.Log("EnemyAI: игрок в радиусе атаки. Дистанция = " + distanceToPlayer);

                if (anim != null)
                {
                    anim.SetTrigger("Attack");
                }

                AttackPlayer();

                nextAttackTime = Time.time + attackCooldown;
            }
        }
        // Состояние 3: Игрок далеко
        else
        {
            if (anim != null)
            {
                anim.SetFloat("speed", 0f);
            }
        }
    }

    void AttackPlayer()
    {
        Debug.Log("EnemyAI: мышь пытается нанести урон игроку");

        if (playerHealth == null)
        {
            Debug.LogError("EnemyAI: playerHealth == null, урон НЕ нанесен");
            return;
        }

        Debug.Log("EnemyAI: урон игроку нанесен: " + damage);
        playerHealth.TakeDamage(damage);
    }

    public void Die()
    {
        Debug.Log("Мышь умерла!");

        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        this.enabled = false;

        Collider2D enemyCollider = GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        Destroy(gameObject, 1.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}