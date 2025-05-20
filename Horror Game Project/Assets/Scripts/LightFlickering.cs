using UnityEngine;

[RequireComponent(typeof(Light))]

public class LightFlickering : MonoBehaviour
{
    private Light lightToFlicker;
    [SerializeField, Range(0f, 15f)] private float minIntensity = 15f;
    [SerializeField, Range(0f, 15f)] private float maxIntensity = 15f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 100f;

    public static float flickerTimer = 0f;
    public static float totalTime = 300f; //5 Mins
    public static float timer; //Game timer

    private float flickerTimeEnd = 210f;

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

            
            if (timer <= 240f && timer > 239)//small flicker at 4 mins
            {
                minIntensity = 0f;
                maxIntensity = 5f;
                timeBetweenIntensity = 0.1f;
            }
            else if (timer <= 238f && timer > 235)//3:55 - 3:30 Light Flickering
            {
                minIntensity = 15f;
                maxIntensity = 15f;
                timeBetweenIntensity = 1.0f;
            }
            else if (timer <= 235f && timer > 210)//3:55 - 3:30 Light Flickering
            {
                minIntensity = 10f;
                maxIntensity = 15f;
                timeBetweenIntensity = 1.0f;
            }
            else if (timer <= flickerTimeEnd)
            {
                flickerTimeEnd -= 30f;

                minIntensity = minIntensity - 2f;
                if (minIntensity < 0f) { minIntensity = 0f; }
                maxIntensity = maxIntensity - 2f;
                if (maxIntensity < 0f) { maxIntensity = 1f; }
                timeBetweenIntensity -= 0.1f;
            }
        }

        flickerTimer += Time.deltaTime;
        if (flickerTimer >= timeBetweenIntensity)
        {
            lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
            flickerTimer = 0f;
        }
        Debug.Log(timer);
    }

    private void ValidateIntensityBounds()
    {
        if (minIntensity > maxIntensity)
        {
            Debug.LogWarning("MinIntensity > MaxIntensity");
        }
    }
}
