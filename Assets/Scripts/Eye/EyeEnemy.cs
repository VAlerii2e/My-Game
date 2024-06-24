using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform[] moveSpots;
    public int randomSpot;
    public int maxHealthEnemy = 3;
    int currentHealth;
    public Animator animator;

    public float damage = 1;
    private void Start()
    {
        waitTime = startWaitTime;
        currentHealth = maxHealthEnemy;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0) 
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetBool("IsDead", true);

        this.enabled = false;  
        GetComponent<Collider2D>().enabled = false;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else {
                waitTime -= Time.deltaTime;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
