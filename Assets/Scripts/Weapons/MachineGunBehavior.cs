using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBehavior : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float timeBetweenBullets;
    public float rotationRandomness;

    [Header("Sounds")]
    public AudioSource audioSource;
    public float minPitch;
    public float maxPitch;

    float lastBulletTime;
    AnimateUpperBody upperBodyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        upperBodyAnimator = GetComponent<AnimateUpperBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (Time.time > lastBulletTime + timeBetweenBullets)
            {
                Shoot();
                lastBulletTime = Time.time;
            }
            upperBodyAnimator.Animate(true);
        }
        else
        {
            upperBodyAnimator.Animate(false);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletSpawnPoint.transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(0, 0, Random.Range(-(rotationRandomness / 2), rotationRandomness / 2));
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;

        if (!audioSource.isPlaying)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }
}
