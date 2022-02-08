using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float targetDistance;
    public float acceleration;
    public float maxSpeed;

    [Header("Move Animation")]
    public float movementCycleTime;
    public Vector2 moveStretchScale;
    public Vector2 moveSquashScale;

    [Header("Shooting")]
    public List<Transform> shootPoints;
    public GameObject bulletPrefab;
    public float bulletChargeTime;
    public float bulletSpeed;
    public AudioSource shotAudio;

    [Header("Shoot Animation")]
    public Vector2 shootWindupStretchScale;
    public float shotRecoveryTime;
    public Vector2 shootSquashScale;

    Rigidbody2D rb;
    bool shooting;
    Vector2 initialScale;
    Transform target;
    Sequence sequence;
    bool movementSquashing;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //2 state FSM, can be done with IF statements
        if (shooting)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, acceleration * Time.deltaTime);
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > targetDistance)
        {
            //animate movement
            if (movementSquashing)
            {
                sequence = DOTween.Sequence();
                sequence.Append(transform.DOScale(moveSquashScale * initialScale, movementCycleTime));
                sequence.AppendCallback(() => movementSquashing = false);
            }
            else
            {
                sequence = DOTween.Sequence();
                sequence.Append(transform.DOScale(moveStretchScale * initialScale, movementCycleTime));
                sequence.AppendCallback(() => movementSquashing = true);
            }

            Vector2 direction = ((Vector2)target.position - rb.position).normalized;

            rb.velocity += direction * acceleration * Time.deltaTime;
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            shooting = true;
            AnimateAndShoot();
        }
    }

    void AnimateAndShoot()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        //Animate windup
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(shootWindupStretchScale * initialScale, bulletChargeTime));

        yield return new WaitForSeconds(bulletChargeTime);

        foreach (Transform shootPoint in shootPoints) {
            GameObject currBullet = Instantiate(bulletPrefab);
            currBullet.transform.position = transform.position;
            currBullet.GetComponent<Rigidbody2D>().velocity = shootPoint.transform.right * bulletSpeed;
            currBullet.transform.right = shootPoint.transform.right;
            shotAudio.Play();
        }

        //Animate shot
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(shootSquashScale * initialScale, shotRecoveryTime / 2));
        sequence.Append(transform.DOScale(initialScale, shotRecoveryTime / 2));
        sequence.AppendCallback(() =>
        {
            movementSquashing = false;
            shooting = false;
        });
    }
}
