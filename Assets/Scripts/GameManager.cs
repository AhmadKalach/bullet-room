using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Death Animation")]
    public float startY;
    public float deathAnimationTime;

    [Header("ScreenShake")]
    public float shakeDuration;
    public float enemyDeathShakeStrength;
    public float playerHitShakeStrength;

    public int score;
    public RectTransform deathGUI;
    public Vector2 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        deathGUI.gameObject.SetActive(false);
        initialPos = deathGUI.position;

    }

    public void EnemyScreenShake()
    {
        Debug.Log("Shake enemy");
        Camera.main.DOShakePosition(shakeDuration, enemyDeathShakeStrength);
    }

    public void PlayerScreenShake()
    {
        Debug.Log("Shake player");
        Camera.main.DOShakePosition(shakeDuration, playerHitShakeStrength);
    }

    public void ReloadSceneAfterWait(float time)
    {
        StartCoroutine(ReloadScene(time));
    }

    public void WaitThenSlideInEndScreen(float waitTime)
    {
        StartCoroutine(WaitThenMoveDeathUI(waitTime));
    }

    public void UpdateHighscore()
    {
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    IEnumerator WaitThenMoveDeathUI(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        deathGUI.gameObject.SetActive(true);
        deathGUI.anchoredPosition = new Vector2(0, startY);
        deathGUI.DOAnchorPosY(0, deathAnimationTime);
    }

    IEnumerator ReloadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainScene(float waitUntilLoad)
    {
        StartCoroutine(LoadMainScene(waitUntilLoad));
    }

    IEnumerator LoadMainScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("SampleScene");
    }
}
