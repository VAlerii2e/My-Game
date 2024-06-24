using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
public class Boss : MonoBehaviour, IDamageable
{
    public GameObject player;
    public float speed;
    private float distance;
    private bool isFlipped = false;

    public GameObject fireball;
    public Transform fireballPos;
    private float timer;

    public float meleeAttackRange = 5f;
    private Animator animator;
    private Transform playerTransform;
    private float meleeDamage = 10f;

    private bool isTakingDamage = false;    
    private float damageCooldown = 5f;
    private float meleeAttackCooldown = 1.5f; // Затримка між атаками ближнього бою
    private float lastMeleeAttackTime = 0f;

    [SerializeField] private float maxHealth = 1000;
    [SerializeField] private float health;

    private GameManagerScript gameManager; // Додаємо змінну для GameManager
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        playerTransform = player.transform;
        health = maxHealth;
        gameManager = FindObjectOfType<GameManagerScript>(); // Знаходимо GameManager в сцені
    }
    void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < 8 && !isTakingDamage)
            {
                if (distance > meleeAttackRange)
                {
                    MoveTowardsPlayer();
                }
                else
                {
                    MeleeAttack();
                }
            }
            else if(distance >= 8 && !isTakingDamage)
            { 
            
                timer += Time.deltaTime;

                if (timer > 2)
                {
                    timer = 0;
                    Shoot();
                }
            }

            FlipTowardsPlayer();
        }
    }
    void MoveTowardsPlayer()
    {
        animator.SetTrigger("Walk");
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
    void Shoot()
    {
        Instantiate(fireball, fireballPos.position, Quaternion.identity);
    }
    void MeleeAttack()
    {
        // Перевірка на затримку між атаками ближнього бою
        if (Time.time - lastMeleeAttackTime >= meleeAttackCooldown)
        {
            lastMeleeAttackTime = Time.time; // Оновлення часу останньої атаки

            if (distance <= meleeAttackRange)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
    void FlipTowardsPlayer()
    {
        if (playerTransform.position.x > transform.position.x && !isFlipped)
        {
            Flip();
        }
        else if (playerTransform.position.x < transform.position.x && isFlipped)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    public void TakeDamage(float damage)
    {
        if (!isTakingDamage)
        {
            health -= damage;
            animator.SetTrigger("TakeHit");
            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(DamageCooldown());
            }
        }
    }
    IEnumerator DamageCooldown()
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(damageCooldown);
        isTakingDamage = false;
    }
    void Die()
    {
        animator.SetBool("IsDead", true);
        if (gameManager != null)
        {
            gameManager.ShowVictoryMenu(); // Викликаємо ShowVictoryMenu перед знищенням об'єкта
        }
        else
        {
            Debug.LogWarning("GameManager not found.");
        }
        Destroy(gameObject, 2f);
    }
    // Цей метод буде викликаний подією анімації
    public void ApplyMeleeDamage()
    {   
        if (distance <= meleeAttackRange)
        {
            player.GetComponent<Player>().TakeDamage(meleeDamage);
        }
    }
}
