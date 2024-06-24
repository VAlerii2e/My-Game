using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Animator animator;
    public static Player Instance { get; private set; }
    [SerializeField] private float movingSpeed = 10f;
    private Rigidbody2D rb;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 100;

    public float health;
    public float maxHealth;

    private SpriteRenderer spriteRenderer;
    private const string IS_RUNNING = "IsRunning";
    private bool isDead;

    public Image healthBar;

    public GameManagerScript gameManager;

    private Vector3 initialAttackPointLocalPosition;


    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialAttackPointLocalPosition = attackPoint.localPosition;
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        HandleRunningState(inputVector);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        UpdateHealthBar();
        AdjustPlayerFacingDirection();
        UpdateAttackPointPosition();

        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameManager.GameOver();
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Console.WriteLine("You dead");
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
    }


    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

 
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null && Player.Instance != null)
        {
            healthBar.fillAmount = Mathf.Clamp(Player.Instance.health / Player.Instance.maxHealth, 0, 1);
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        if (Player.Instance != null)
        {
            Vector3 mousePos = GameInput.Instance.GetMousePosition();
            Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();
            spriteRenderer.flipX = mousePos.x < playerPosition.x;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    private void HandleRunningState(Vector2 inputVector)
    {
        bool isRunning = inputVector.sqrMagnitude > 0.01f;
        animator.SetBool("IsRunning", isRunning);
    }

    private void UpdateAttackPointPosition()
    {
        Vector3 attackPointLocalPosition = initialAttackPointLocalPosition;
        attackPointLocalPosition.x *= spriteRenderer.flipX ? -1 : 1;
        attackPoint.localPosition = attackPointLocalPosition;
    }

    public void RestoreHealth(float healthRestoreAmount) 
    {
        //health += healthRestoreAmount;
        health = Mathf.Clamp(health + healthRestoreAmount, 0, maxHealth); // Обмежуємо здоров'я
        UpdateHealthBar(); // Оновлюємо індикатор здоров'я
    }
}