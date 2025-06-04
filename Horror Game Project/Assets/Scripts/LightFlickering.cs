using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlickering : MonoBehaviour
{
    private Light lightToFlicker;
    [SerializeField] private StageManager stageManager;
    [SerializeField, Range(1f, 15f)] private float minIntensity = 15f;
    [SerializeField, Range(1f, 15f)] private float maxIntensity = 15f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 100f;

    public float flickerTimer = 0f;
    public float totalTime = 300f; //5 Mins
    public float scareTime = 300f; //Set this to time for jumpscare and level restart
    public float timer; //Game timer

    [SerializeField] private float flickerTimeEnd = 210f;

    private void Awake()
    {
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>();
        }

        timer = totalTime;

        ValidateIntensityBounds();
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            //Debug.Log(timer);
            //Debug.Log($"Timer: {timer:F2}, DeltaTime: {Time.deltaTime:F4}, Time.time: {Time.time:F2}");


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
            else if (timer <= flickerTimeEnd) //Continue reducing brightness
            {   
                flickerTimeEnd -= 15f;
               
                

                minIntensity = minIntensity - 1f;
                if (minIntensity < 0f) { minIntensity = 1f; }
                maxIntensity = maxIntensity - 1f;
                if (maxIntensity < 2f) { maxIntensity = 2f; }
                if (timeBetweenIntensity > 0.5f)
                {
                    timeBetweenIntensity -= 0.5f;
                }
            }
        }
        
        if (timer < scareTime)
        {
            if (stageManager != null)
            {
                StartCoroutine(stageManager.JumpScareAndRestart());
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