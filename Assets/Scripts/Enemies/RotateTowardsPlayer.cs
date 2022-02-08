using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    Transform target;
    bool lookingRight;

    // Start is called before the first frame update
    void Start()
    {
        lookingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                //look right
                if (!lookingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    lookingRight = true;
                }
            }
            else
            {
                //look left
                if (lookingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    lookingRight = false;
                }
            }
        }
    }
}
