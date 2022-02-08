using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTextBehavior : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        if (gameManager.score > highscore)
        {
            highscoreText.text = "High: " + gameManager.score;
        }
        else
        {
            highscoreText.text = "High: " + highscore;
        }
    }
}
