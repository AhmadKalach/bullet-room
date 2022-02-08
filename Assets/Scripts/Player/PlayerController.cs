using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public GameObject sprite;

    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource stompAudio;

    [Header("Weapons")]
    public Transform stompCheckTransform;
    public Vector2 stompCheckSize;
    public float stompDamage;
    public float stompKnockback;

    [Header("Ground Check")]
    public Transform groundCheckTransform;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;

    [Header("Jump")]
    public bool enableDoubleJump;
    public float jumpForce;
    public float gravityFallMultiplier;
    public bool overrideGravity;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Squash and Stretch")]
    public float squashTime;
    public Vector2 squashValues;
    public float stretchTime;
    public Vector2 stretchValues;

    [Header("Events")]
    public UnityEvent doubleJump;

    public bool isGrounded;
    Rigidbody2D rb;
    bool alreadySquashed;
    bool stretched;
    bool jump;
    Sequence currSequence;
    GameManager gameManager;
    Vector2 initialScale;
    bool canDoubleJump;
    bool lookingRight;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = sprite.transform.localScale;
        lookingRight = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        if (jump)
        {
            Jump();
        }
        IncreaseFallGravity();
    }

    private void Update()
    {
        if (!enableDoubleJump)
        {
            canDoubleJump = false;
        }

        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        StompEnemies();
        GroundCheck();
        FlipPlayerSprite();
        GetJumpInputAndHandleDoubleJump();
        SquashAndStretch();

        if (isGrounded)
        {
            overrideGravity = false;
        }

    }

    void SquashAndStretch()
    {
        if (!isGrounded)
        {
            if (!stretched)
            {
                Stretch();
                stretched = true;
                alreadySquashed = false;
            }
        }
        else
        {
            if (stretched && !alreadySquashed)
            {
                Squash();
                stretched = false;
                alreadySquashed = true;
            }
        }
    }

    void Stretch()
    {
        currSequence = DOTween.Sequence();
        currSequence.Append(sprite.transform.DOScaleX(stretchValues.x * initialScale.x, stretchTime));
        currSequence.Join(sprite.transform.DOScaleY(stretchValues.y * initialScale.y, stretchTime));
        currSequence.Play();
    }

    void Squash()
    {
        currSequence = DOTween.Sequence();
        currSequence.Append(sprite.transform.DOScaleX(squashValues.x * initialScale.x, squashTime / 2));
        currSequence.Join(sprite.transform.DOScaleY(squashValues.y * initialScale.y, squashTime / 2));
        currSequence.Append(sprite.transform.DOScaleX(initialScale.x, squashTime / 2));
        currSequence.Join(sprite.transform.DOScaleY(initialScale.y, squashTime / 2));
        currSequence.Play();
    }

    void GetJumpInputAndHandleDoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    void GroundCheck()
    {
        Collider2D coll = Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0, groundLayer);

        if (coll == null)
        {
            isGrounded = false;
        }
        else if (rb.velocity.y < 0.05f)
        {
            isGrounded = true;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            jumpAudio.Play();
            overrideGravity = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (!isGrounded && canDoubleJump)
        {
            jumpAudio.Play();
            overrideGravity = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            doubleJump.Invoke();
        }

        jump = false;
    }

    void IncreaseFallGravity()
    {
        if (!overrideGravity)
        {
            if ((rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z)) || rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityFallMultiplier - 1) * Time.deltaTime;
            }
        }
        else
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityFallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Movement()
    {
        float speed = Input.GetAxis("Horizontal") * moveSpeed;
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (isGrounded)
        {
            animator.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0);
        }

    }

    void FlipPlayerSprite()
    {
        if (rb.velocity.x > 0.01f && !lookingRight)
        {
            transform.rotation = Quaternion.identity;
            lookingRight = true;
        }
        else if (rb.velocity.x < -0.01f && lookingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            lookingRight = false;
        }
    }

    void StompEnemies()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(stompCheckTransform.position, stompCheckSize, 0);

        foreach (Collider2D coll in colls)
        {
            if (coll.gameObject.CompareTag("Enemy"))
            {
                    //Jump
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = true;
                    overrideGravity = false;
                    stompAudio.Play();
                    coll.gameObject.GetComponent<EnemyBaseBehavior>().GetStomped(stompDamage, stompKnockback);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(stompCheckTransform.position, stompCheckSize);
    }
}
