using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;
    public float oxygenDamageDelay = 2f;
    public float oxygenDamagePerSecond = 10f;
    public GameObject deathScreen;

    private bool takingOxygenDamage = false;
    private Oxygen oxygen;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.maxValue = maxHealth;

        oxygen = GetComponent<Oxygen>();
    }

    void Update()
    {
        if (oxygen != null && oxygen.currentOxygen <= 0)
        {
            if (!takingOxygenDamage)
                InvokeRepeating(nameof(DamageFromOxygen), oxygenDamageDelay, 1f);
            takingOxygenDamage = true;
        }
        else if (oxygen != null && oxygen.currentOxygen > 0 && takingOxygenDamage)
        {
            CancelInvoke(nameof(DamageFromOxygen));
            takingOxygenDamage = false;
        }

        if (currentHealth <= 0)
            Die();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    void DamageFromOxygen()
    {
        TakeDamage(oxygenDamagePerSecond);
        Debug.Log("Taking damage from no oxygen...");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerTorch torch = GetComponent<PlayerTorch>();
            if (torch != null && torch.HasTorchActive())
            {
                Debug.Log("Torch active — immune to enemy damage.");
                return;
            }

            EnemyDamage damage = collision.gameObject.GetComponent<EnemyDamage>();
            if (damage != null)
            {
                TakeDamage(damage.damageAmount);
                Debug.Log("Player damaged by enemy: " + damage.damageAmount);
            }
        }
    }

    void Die()
    {
        Debug.Log("Player has died. GAME OVER.");

        if (deathScreen != null)
            deathScreen.SetActive(true);

        Time.timeScale = 0f;
    }
}

