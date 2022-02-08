using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public SpriteRenderer weaponSprite;
    public bool mouseTarget;
    public GameObject objectTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos;
        if (mouseTarget)
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            targetPos = objectTarget.transform.position;
        }

        Vector2 direction = new Vector2(
                targetPos.x - transform.position.x,
                targetPos.y - transform.position.y
            );

        transform.right = direction;

        if (targetPos.x >= transform.position.x)
        {
            weaponSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (targetPos.x <= transform.position.x)
        {
            weaponSprite.transform.localRotation = Quaternion.Euler(0, 180, 180);
        }
    }
}
