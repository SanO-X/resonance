using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum WorldState { Harmony, Aggression, Void }

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    [Header("Текущее состояние мира")]
    public WorldState currentState = WorldState.Harmony;

    [Header("Ссылка на твой Global Volume")]
    public Volume globalVolume; 

    [Header("Настройки таймингов и триггеров")]
    public int attackCountForAggression = 5; // Нужно больше ударов для ярости, чтобы не скакало сразу
    public float timeForVoid = 12f;          // В Пустоту уходим позже (через 12 сек затишья), даем игроку перевести дух
    public float aggressionCooldown = 4f;   // Время, за которое ярость остывает

    [Header("Плавность перехода состояний")]
    public float transitionSpeed = 1.2f;    // Сделал переход мягче и медленнее

    private float attackTimer = 0f;
    private int recentAttacks = 0;
    private float noAttackTimer = 0f; 

    private EnemyAI[] enemies;
    private ColorAdjustments colorAdjustments;

    private PlayerMovement playerMovement;
    private PLayerCombat playerCombat; 

    private Color targetColor = Color.white;
    private float targetSaturation = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (globalVolume == null) globalVolume = FindFirstObjectByType<Volume>();

        if (globalVolume != null && globalVolume.profile != null)
        {
            if (globalVolume.profile.TryGet(out colorAdjustments))
            {
                colorAdjustments.colorFilter.overrideState = true;
                colorAdjustments.saturation.overrideState = true;
            }
        }
        CachePlayerScripts();
    }

    void Update()
    {
        // Плавная интерполяция цвета и насыщенности для мягкого визуала
        if (colorAdjustments != null)
        {
            colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, targetColor, Time.deltaTime * transitionSpeed);
            colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, targetSaturation, Time.deltaTime * transitionSpeed);
        }

        if (playerMovement == null || playerCombat == null) CachePlayerScripts();

        // Логика таймера Пустоты
        noAttackTimer += Time.deltaTime;
        if (noAttackTimer >= timeForVoid && currentState != WorldState.Void)
        {
            ChangeState(WorldState.Void);
        }

        // Плавное остывание счетчика атак
        attackTimer += Time.deltaTime;
        if (attackTimer >= aggressionCooldown)
        {
            if (recentAttacks > 0) recentAttacks--;
            attackTimer = 0f;
            CheckStateBalance();
        }
    }

    public void RegisterPlayerAttack()
    {
        recentAttacks++;
        noAttackTimer = 0f; // Сбрасываем таймер Пустоты при любой атаке

        if (recentAttacks >= attackCountForAggression && currentState != WorldState.Aggression)
        {
            ChangeState(WorldState.Aggression);
        }
        else if (currentState == WorldState.Void)
        {
            ChangeState(WorldState.Harmony);
        }
    }

    private void CheckStateBalance()
    {
        if (recentAttacks < attackCountForAggression && noAttackTimer < timeForVoid && currentState != WorldState.Harmony)
        {
            ChangeState(WorldState.Harmony);
        }
    }

    private void CachePlayerScripts()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerMovement = playerObj.GetComponent<PlayerMovement>();
            playerCombat = playerObj.GetComponent<PLayerCombat>(); 
        }
    }

    private void ChangeState(WorldState newState)
    {
        currentState = newState;
        enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);

        switch (currentState)
        {
            case WorldState.Harmony:
                targetColor = Color.white; // Обычный чистый цвет
                targetSaturation = 0f;     // Стандартная насыщенность
                if (playerMovement != null) playerMovement.speed = 4f; 
                if (playerCombat != null) playerCombat.attackCooldown = 1.5f; 
                foreach (var enemy in enemies) { if (enemy != null) { enemy.speed = 2.5f; enemy.damage = 10; } }
                break;

            case WorldState.Aggression:
                // Мягкий багровый тон с повышенной прозрачностью (меньше ядовитого красного)
                targetColor = new Color(0.75f, 0.45f, 0.5f); 
                targetSaturation = 15f;    // Слегка приподнятая насыщенность вместо сильного пересвета
                if (playerMovement != null) playerMovement.speed = 5f; 
                if (playerCombat != null) playerCombat.attackCooldown = 0.4f; 
                foreach (var enemy in enemies) { if (enemy != null) { enemy.speed = 4.5f; enemy.damage = 20; } } 
                break;

            case WorldState.Void:
                // Плавный уход в серость
                targetColor = new Color(0.6f, 0.6f, 0.6f); 
                targetSaturation = -80f;   // Не полный мертвый ноль, а мягкое обесцвечивание
                if (playerMovement != null) playerMovement.speed = 3.5f;
                if (playerCombat != null) playerCombat.attackCooldown = 1.8f;
                foreach (var enemy in enemies) { if (enemy != null) { enemy.speed = 1.2f; enemy.damage = 3; } } 
                break;
        }
    }
}