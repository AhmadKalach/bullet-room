using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingEnemyMovement : MonoBehaviour
{
    public LayerMask groundLayer;

    [Header("Ground Check")]
    public Transform groundCheckTransform;
    public float groundCheckRadius;

    [Header("Forward Check")]
    public Transform forwardCheckTransform;
    public float forwardCheckRadius;

    [Header("Movement")]
    public float speed;

    bool isGrounded;
    bool hasForward;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        ForwardCheck();

        if (isGrounded)
        {
            rb.velocity = transform.right * speed;
        }

        if (hasForward)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    void GroundCheck()
    {
        Collider2D coll = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);

        if (coll == null)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }

    void ForwardCheck()
    {
        Collider2D coll = Physics2D.OverlapCircle(forwardCheckTransform.position, forwardCheckRadius, groundLayer);

        if (coll == null)
        {
            hasForward = false;
        }
        else
        {
            hasForward = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(forwardCheckTransform.position, forwardCheckRadius);
    }
}
