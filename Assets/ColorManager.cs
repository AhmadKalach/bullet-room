using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color redColorReplacement;
    public Color whiteColorReplacement;
    public List<GameObject> objectsToRecolorRed;
    public List<GameObject> objectsToRecolorWhite;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in objectsToRecolorRed)
        {
            obj.GetComponent<SpriteRenderer>().color = redColorReplacement;
        }

        foreach (GameObject obj in objectsToRecolorWhite)
        {
            obj.GetComponent<SpriteRenderer>().color = whiteColorReplacement;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
