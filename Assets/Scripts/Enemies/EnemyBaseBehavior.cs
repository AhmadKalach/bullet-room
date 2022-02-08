using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBaseBehavior : MonoBehaviour
{
    public float maxHealth;
    public int score;
    public SpriteRenderer spriteRenderer;
    public float flashTime;
    public GameObject deathEffect;
    public GameObject deathSfxPrefab;
    public GameObject healthPrefab;
    public float healthDropProbability;

    float stompInvincibilityTime = 0.03f;
    Material material;
    float currentHealth;
    float lastStompTime;
    Rigidbody2D rb;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        material = spriteRenderer.material;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStomped(float damage, float knockbackForce)
    {
        if (Time.time > lastStompTime + stompInvincibilityTime)
        {
            lastStompTime = Time.time;
            GetDamaged(damage);
            rb.velocity = Vector2.down * knockbackForce;
        }
    }

    public void GetDamaged(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        StartCoroutine(Flash());
    }

    public void GetDamaged(float amount, bool pushRight, float knockbackForce)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }

        Vector2 knockbackVector = pushRight ? Vector2.right * knockbackForce : -Vector2.right * knockbackForce;

        rb.velocity += knockbackVector;

        StartCoroutine(Flash());
    }

    void Die()
    {

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(deathSfxPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        gameManager.EnemyScreenShake();

        float rand = Random.Range(0f, 1f);
        if (rand < healthDropProbability)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }

        gameManager.score += score;
        WaveManager waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        if (waveManager != null)
        {
            waveManager.enemies.Remove(this.gameObject);
        }
    }

    IEnumerator Flash()
    {
        material.SetInt("InvertColors", 1);
        yield return new WaitForSeconds(flashTime);
        material.SetInt("InvertColors", 0);
    }
}
