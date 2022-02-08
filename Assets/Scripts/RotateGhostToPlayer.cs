using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGhostToPlayer : MonoBehaviour
{
    public SpriteRenderer sprite;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            sprite.transform.rotation = Quaternion.identity;
        }
        else
        {
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
