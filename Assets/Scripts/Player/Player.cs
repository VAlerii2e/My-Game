//using System;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//[SelectionBase]
//public class Player : MonoBehaviour
//{
//    private Animator animator;
//    public static Player Instance { get; private set; }
//    [SerializeField] private float movingSpeed = 10f;
//    private Rigidbody2D rb;

//    public Transform attackPoint;
//    public LayerMask enemyLayers;
//    public float attackRange = 0.5f;
//    public int attackDamage = 100;

//    public float health;
//    public float maxHealth;

//    private void Awake()
//    {
//        Instance = this;
//        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//    }

//    private void Start()
//    {
//        health = maxHealth;
//    }

//    private void Update()
//    {
//        Vector2 inputVector = GameInput.Instance.GetMovementVector();
//        HandleRunningState(inputVector);

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            PlayerVisual pv = new PlayerVisual();
//            pv.Attack();
//        }
//    }

//    private void FixedUpdate()
//    {
//        HandleMovement();
//    }

//    public void TakeDamage(float damage)
//    {
//        health -= damage;
//        if (health <= 0)
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        Console.WriteLine("You dead");
//    }

//    private void HandleMovement()
//    {
//        Vector2 inputVector = GameInput.Instance.GetMovementVector();
//        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
//    }

//    //private void HandleRunningState(Vector2 inputVector)
//    //{
//    //    bool isRunning = inputVector.sqrMagnitude > 0.01f;
//    //    animator.SetBool("IsRunning", isRunning);
//    //}

//    public Vector3 GetPlayerScreenPosition()
//    {
//        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
//        return playerScreenPosition;
//    }

//    //private void Attack()
//    //{
//    //    animator.SetTrigger("Attack");
//    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
//    //    foreach (Collider2D enemy in hitEnemies)
//    //    {
//    //        enemy.GetComponent<EyeEnemy>().TakeDamage(attackDamage);
//    //    }
//    //}

//    private void OnDrawGizmosSelected()
//    {
//        if (attackPoint == null)
//        {
//            return;
//        }
//        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//    }
//}