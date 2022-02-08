using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightColorChanger : MonoBehaviour
{
    public bool isRed;
    public Light2D light2D;

    ColorManager colorManager;

    // Start is called before the first frame update
    void Start()
    {
        colorManager = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>();
    }

    // Update is called once per frame
    void Update()
    {
            //if (isRed)
            //{
            //    light2D.color = colorManager.redColorReplacement;
            //}
            //else
            //{
            //    light2D.color = colorManager.whiteColorReplacement;
            //}
    }
}
