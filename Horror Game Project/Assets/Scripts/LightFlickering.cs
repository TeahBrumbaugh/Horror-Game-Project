using UnityEngine;

[RequireComponent(typeof(Light))]

public class LightFlickering : MonoBehaviour
{
    private Light lightToFlicker;
    [SerializeField, Range(0f, 13f)] private float minIntensity = 0.5f;
    [SerializeField, Range(0f, 13f)] private float maxIntensity = 12f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 0.1f;

    private float currentTimer = 0f;

    private void Awake()
    {
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>();
        }

        ValidateIntensityBounds();
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= timeBetweenIntensity)
        {
            lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
            currentTimer = 0f;
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
