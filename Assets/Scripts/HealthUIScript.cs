using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIScript : MonoBehaviour
{
    public List<GameObject> healthSpots;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < healthSpots.Count; i++)
        {
            healthSpots[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealthUI()
    {
        if (playerStats == null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }

        for (int i = 0; i < healthSpots.Count; i++)
        {
            healthSpots[i].SetActive(false);
        }

        for (int i = 0; i < playerStats.currentHealth; i++)
        {
            healthSpots[i].SetActive(true);
        }
    }
}
    