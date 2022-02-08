using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class EnemyDeathAnimation : MonoBehaviour
{
    public Light2D spotLight;
    public float fadeDuration;
    public GameObject particlePrefab;
    public float particleSpeed;
    public int particleCount;

    float startIntensity;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startIntensity = spotLight.intensity;

        for (int i = 0; i < particleCount; i++)
        {
            GameObject particle = Instantiate(particlePrefab, transform);
            particle.transform.position = transform.position;
            Vector2 direction = Random.insideUnitCircle.normalized;
            particle.GetComponent<Rigidbody2D>().velocity = direction * particleSpeed;
        }
    }

    private void Update()
    {
        spotLight.intensity = Mathf.Lerp(startIntensity, 0, (Time.time - startTime) / fadeDuration);

        if (Time.time > startTime + 20)
        {
            Destroy(this.gameObject);
        }
    }
}
