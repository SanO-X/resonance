using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator anim;

    // Параметры для "памяти"
    private float lastMoveX;
    private float lastMoveY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
   
    float moveX = Input.GetAxisRaw("Horizontal");
    float moveY = Input.GetAxisRaw("Vertical");

    // Отдаем текущее движение
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

    // Движение
    Vector2 moveInput = new Vector2(moveX, moveY).normalized;
    rb.linearVelocity = moveInput * speed;

    }


}
