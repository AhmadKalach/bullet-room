using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public float damage;
    public float knockBackForce;
    public float volume = 2f;
    public GameObject audioPrefab;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (rb.velocity.x > 0)
            {
                collision.gameObject.GetComponent<EnemyBaseBehavior>().GetDamaged(damage, true, knockBackForce);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyBaseBehavior>().GetDamaged(damage, false, knockBackForce);
            }
            Instantiate(audioPrefab);
            Destroy(this.gameObject);
        }
    }
}
