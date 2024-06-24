using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy2 : MonoBehaviour, IDamageable
{
    public float speed; // швидкість руху
    public Transform[] moveSpots; // точки доя яких буде переміщуватисб наш супротвник
    public int randomSpot; // випадкова точка
    private float waitTime;
    public float startWaitTime;
    public float maxHealthEnemy = 3;
    public float currentHealth;
    public Animator animator;
    public float damage;
    private bool isAttacking = false;
    private bool isTakingHit = false;
    private bool isDead = false;
    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length); // обираєсо випадкоаву точку
        currentHealth = maxHealthEnemy;
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        if (!isAttacking && !isTakingHit)
        {
            Move();
        }
    }
    private void Move()
    {
        animator.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime); // перееміщення до точок

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            other.gameObject.GetComponent<Player>().health -= damage;
            isAttacking = false;
        }
    }
    public void TakeDamage(float amount) 
    {
        currentHealth -= amount;
        isTakingHit = true;
        animator.SetTrigger("TakingHit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
