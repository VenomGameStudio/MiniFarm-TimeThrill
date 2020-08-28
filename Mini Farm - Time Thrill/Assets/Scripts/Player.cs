using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public float sunHeat;
    public HealthBar healthBar;
    public PlayerManager playerManager;
    public SpriteRenderer spriteRenderer;
    public Sprite deadBody;
    public Animator animator;

    public GameObject handItem;

    private void Start()
    {
        handItem.gameObject.SetActive(false);
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        currentHealth = 100;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0f || playerManager.noTimeLeft)
        {
            Time.timeScale = 0f;
            animator.SetBool("isDead", true);
        }
        sunHeat += Time.deltaTime;
        if (sunHeat >= 15f)
        {
            TakeDamage(10);
            sunHeat = 0.1f;
        }
        if (playerManager.healthFull == 1)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            playerManager.healthFull = 0; 
        }

    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
