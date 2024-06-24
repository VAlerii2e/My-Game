//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerVisual : MonoBehaviour
//{
//    private Animator animator;
//    private SpriteRenderer spriteRenderer;
//    private const string IS_RUNNING = "IsRunning";
//    private const string IS_DEAD = "IsDead";

//    public Image healthBar;

//    private void Awake()
//    {
//        animator = GetComponent<Animator>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }

//    private void Update()
//    {
//        UpdateHealthBar();
//        AdjustPlayerFacingDirection();
//    }

//    private void UpdateHealthBar()
//    {
//        if (healthBar != null && Player.Instance != null)
//        {
//            healthBar.fillAmount = Mathf.Clamp(Player.Instance.health / Player.Instance.maxHealth, 0, 1);
//        }
//    }

//    private void AdjustPlayerFacingDirection()
//    {
//        if (Player.Instance != null)
//        {
//            Vector3 mousePos = GameInput.Instance.GetMousePosition();
//            Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();
//            spriteRenderer.flipX = mousePos.x < playerPosition.x;
//        }
//    }

//    public void Attack()
//    {
//        animator.SetTrigger("Attack");
//        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
//        foreach (Collider2D enemy in hitEnemies)
//        {
//            enemy.GetComponent<EyeEnemy>().TakeDamage(attackDamage);
//        }
//    }

//    private void HandleRunningState(Vector2 inputVector)
//    {
//        bool isRunning = inputVector.sqrMagnitude > 0.01f;
//        animator.SetBool("IsRunning", isRunning);
//    }
//}