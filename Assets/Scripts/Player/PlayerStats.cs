using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public float invincibilityTime;

    [Header("Animations")]
    public SpriteRenderer bodySpriteRenderer;
    public SpriteRenderer mouthSpriteRenderer;
    public float invertTime;


    [Header("Events")]
    public UnityEvent hurt;
    public UnityEvent healed;
    public UnityEvent playerDeadEvent;

    Material bodyMaterial;
    Material mouthMaterial;
    float lastInvincibility;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bodyMaterial = bodySpriteRenderer.GetComponent<SpriteRenderer>().material;
        mouthMaterial = mouthSpriteRenderer.GetComponent<SpriteRenderer>().material;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void GetDamaged(int damage)
    {
        if (Time.time > lastInvincibility + invincibilityTime)
        {
            lastInvincibility = Time.time;
            currentHealth -= damage;
            hurt.Invoke();
            gameManager.PlayerScreenShake();

            if (currentHealth <= 0)
            {
                Die();
            }
            StartCoroutine(InvertColors());
        }
    }

    public void GetHealed(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healed.Invoke();
    }

    public void Die()
    {
        playerDeadEvent.Invoke();
        Destroy(this.gameObject);
    }

    IEnumerator InvertColors()
    {
        bodyMaterial.SetInt("InvertColors", 1);
        mouthMaterial.SetInt("InvertColors", 1);

        yield return new WaitForSeconds(invertTime);

        bodyMaterial.SetInt("InvertColors", 0);
        mouthMaterial.SetInt("InvertColors", 0);
    }
}
