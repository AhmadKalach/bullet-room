using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownwardsShot : MonoBehaviour
{
    public GameObject bullet;
    public Transform origin;
    public float bulletSpeed;

    public void Shoot()
    {
        GameObject currBullet = Instantiate(bullet);
        currBullet.transform.position = origin.transform.position;
        currBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
    }
}
