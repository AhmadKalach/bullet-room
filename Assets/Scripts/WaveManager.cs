using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Spawning")]
    public List<Transform> spawnPoints;
    public GameObject warningSignPrefab;
    public float warningTime = 1f;

    [Header("Processing")]
    public char seperator;
    public GameObject eyePrefab;
    public char eyeChar;
    public GameObject batPrefab;
    public char batChar;
    public GameObject bigBatPrefab;
    public char bigBatChar;
    public GameObject ghostPrefab;
    public char ghostChar;
    public List<string> waves;
    public List<GameObject> enemies;

    [Header("Infinite")]
    public int startWaveValue;
    public int valueIncreasePerWave;
    public float spawnDelay;
    public int batValue;
    public int bigBatValue;
    public int eyeValue;
    public int ghostValue;
    public float batProbability;
    public float bigBatProbability;
    public float eyeProbability;
    public float ghostProbability;

    [Header("Events")]
    public UnityEvent wonGame;

    int currWaveIndex;
    float minEndTime;
    bool waveSpawned;
    int waveValue;

    // Start is called before the first frame update
    void Start()
    {
        currWaveIndex = 0;
        waveValue = startWaveValue;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count < 1 && Time.time > minEndTime)
        {
            if (currWaveIndex < waves.Count)
            {
                SpawnNextWave();
            }
            else
            {
                SpawnInfinitely();
            }
        }
    }

    public void SpawnInfinitely()
    {
        if (!waveSpawned)
        {
            waveSpawned = true;
            enemies.Clear();
            int currWaveValue = 0;
            float currSpawnDelay = spawnDelay;

            while (currWaveValue < waveValue)
            {
                if (currWaveValue > waveValue / 2)
                {
                    currSpawnDelay = spawnDelay * 2;
                }
                        
                float rand = Random.Range(0f, 1f);

                if (rand < batProbability)
                {
                    StartCoroutine(Spawn(batPrefab, currSpawnDelay));
                    currWaveValue += batValue;
                }
                else if (rand < bigBatProbability)
                {
                    StartCoroutine(Spawn(bigBatPrefab, currSpawnDelay));
                    currWaveValue += bigBatValue;
                }
                else if (rand < eyeProbability)
                {
                    StartCoroutine(Spawn(eyePrefab, currSpawnDelay));
                    currWaveValue += eyeValue;
                }
                else if (rand < ghostProbability)
                {
                    StartCoroutine(Spawn(ghostPrefab, currSpawnDelay));
                    currWaveValue += ghostValue;
                }
            }

            minEndTime = Time.time + currSpawnDelay * 1.5f;
        }
        else
        {
            if (enemies.Count < 1 && Time.time > minEndTime)
            {
                waveSpawned = false;
                waveValue += valueIncreasePerWave;
            }
        }
    }



    public void SpawnNextWave()
    {
        enemies.Clear();
        string currWave = waves[currWaveIndex];
        string[] seperatedString = currWave.Split(seperator);
        currWaveIndex++;
        float waitTime = 0;

        foreach (string toParse in seperatedString)
        {
            char currChar = toParse.ToCharArray()[0];
            if (currChar.Equals(eyeChar))
            {
                StartCoroutine(Spawn(eyePrefab, waitTime));
            }
            else if (currChar.Equals(batChar))
            {
                StartCoroutine(Spawn(batPrefab, waitTime));
            }
            else if (currChar.Equals(bigBatChar))
            {
                StartCoroutine(Spawn(bigBatPrefab, waitTime));
            }
            else if (currChar.Equals(ghostChar))
            {
                StartCoroutine(Spawn(ghostPrefab, waitTime));
            }
            else
            {
                waitTime += float.Parse(toParse);
            }
        }
        minEndTime = waitTime + Time.time + (warningTime * 1.5f);
    }

    IEnumerator Spawn(GameObject prefab, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Transform targetPos = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject currWarning = Instantiate(warningSignPrefab, targetPos.position, Quaternion.identity);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(warningTime);
        sequence.AppendCallback(() =>
        {
            Destroy(currWarning.gameObject);
            GameObject enemy = Instantiate(prefab, targetPos.position, Quaternion.identity);
            enemies.Add(enemy);
        });
    }
}

