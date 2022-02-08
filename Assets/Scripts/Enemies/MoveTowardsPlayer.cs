using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed = 5f;
    public float targetDistance;

    Transform target;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (Vector2.Distance(transform.position, target.transform.position) > targetDistance)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;

            rb.velocity += direction * acceleration * Time.deltaTime;
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, acceleration * Time.deltaTime);
        }
    }
}
