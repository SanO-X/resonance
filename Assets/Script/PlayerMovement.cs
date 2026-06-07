using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки движения")]
    public float speed = 5f;

    private Rigidbody2D rb;
    public Animator anim;

    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Отдаем текущее движение в аниматор (твои параметры с маленькой буквы)
        if (anim != null)
        {
            anim.SetFloat("horizontal", moveX);
            anim.SetFloat("vertical", moveY);

            // Запоминаем направление для Idle
            if (moveX != 0 || moveY != 0) 
            {
                anim.SetFloat("lastHorizontal", moveX);
                anim.SetFloat("lastVertical", moveY);
            }

            // Скорость для переходов
            float speedAmount = new Vector2(moveX, moveY).magnitude;
            anim.SetFloat("speed", speedAmount);
        }

        // Сохраняем ввод для физики
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Переносим физическое движение сюда, чтобы не было скольжения и багов с колайдерами
        if (rb != null)
        {
            rb.linearVelocity = moveInput * speed;
        }
    }
}