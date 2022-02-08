using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject winUI;

    public void EnableWinUI()
    {
        winUI.SetActive(true);
    }
}
