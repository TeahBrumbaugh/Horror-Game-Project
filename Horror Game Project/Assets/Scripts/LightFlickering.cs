using UnityEngine;

[RequireComponent(typeof(Light))]

public class LightFlickering : MonoBehaviour
{
    private Light lightToFlicker;
    [SerializeField, Range(0f, 13f)] private float minIntensity = 12f;
    [SerializeField, Range(0f, 13f)] private float maxIntensity = 12f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 100f;

    private float flickerTimer = 0f;
    public float totalTime = 300f; //5 Mins
    private float timer; //Game timer

    private void Awake()
    {
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>();
        }

        timer = totalTime;

        ValidateIntensityBounds();
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            //Reset flicker values
            minIntensity = 12f;
            maxIntensity = 12f;
            timeBetweenIntensity = 0.1f;

            //small flicker at 4 mins
            if (timer <= 240f && timer > 239)
            {
                minIntensity = 0f;
                maxIntensity = 5f;
                timeBetweenIntensity = 0.1f;
            }
            else if (timer <= 230f)
            {
                minIntensity = 0.5f;
                maxIntensity = 1f;
                timeBetweenIntensity = 0.1f;
            }
        }

        flickerTimer += Time.deltaTime;
        if (flickerTimer >= timeBetweenIntensity)
        {
            lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
            flickerTimer = 0f;
        }
    }

    private void ValidateIntensityBounds()
    {
        if (minIntensity > maxIntensity)
        {
            Debug.LogWarning("MinIntensity > MaxIntensity");
        }
    }
}
