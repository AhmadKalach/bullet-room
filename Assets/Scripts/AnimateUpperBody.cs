using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateUpperBody : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite notShootingSprite;
    public Sprite shootingSprite;

    bool animatingShooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Animate(bool isShooting)
    {
        if (isShooting)
        {
            if (!animatingShooting)
            {
                spriteRenderer.sprite = shootingSprite;
                animatingShooting = true;
            }
        }
        else
        {
            if (animatingShooting)
            {
                spriteRenderer.sprite = notShootingSprite;
                animatingShooting = false;
            }
        }
    }
}
