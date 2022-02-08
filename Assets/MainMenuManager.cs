using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainCam;
    public float startCameraY;

    Vector3 targetCamPos;

    // Start is called before the first frame update
    void Start()
    {
        targetCamPos = mainCam.transform.position;
        mainCam.transform.position = new Vector3(targetCamPos.x, startCameraY, targetCamPos.z);
    }

    public void MoveCamera(float transitionTime)
    {
        mainCam.transform.DOMoveY(targetCamPos.y, transitionTime);
    }
}
