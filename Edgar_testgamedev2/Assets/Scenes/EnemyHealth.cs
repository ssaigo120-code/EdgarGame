using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        audioSource.PlayOneShot(hitSound);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (EnemyCounter.Instance != null)
            EnemyCounter.Instance.AddKill();

        Destroy(gameObject) ;
    }
}
