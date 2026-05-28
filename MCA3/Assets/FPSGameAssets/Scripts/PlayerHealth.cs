using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;

    public int startingHealth = 100;

    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static bool isAlive {get; private set;}


    void Start()
    {
        currentHealth = startingHealth;
        isAlive = true;

        UpdateHealthSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        UpdateHealthSlider();

        Debug.Log("Damage Taken: " + currentHealth);

        if (currentHealth <= 0 && isAlive)
        {
            //player dies
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Dies!");
        isAlive = false;
        var audioSource = GetComponent<AudioSource>();

        if(audioSource)
        {
            audioSource.Play();
        }
        transform.Rotate(-90, 0, 0, Space.Self);
    }

    void UpdateHealthSlider()
    {
        if (healthSlider)
        {
            healthSlider.value = currentHealth;
        }

    }

    public void TakeHealth(int health)
    {
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        UpdateHealthSlider();

        Debug.Log("Health Taken: " + currentHealth);
    }
}
