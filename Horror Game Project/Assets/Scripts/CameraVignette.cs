using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VignetteController : MonoBehaviour
{
    public Volume volumeSettings;
    private Vignette vignette;

    [SerializeField, Range(0f, 1f)] private float vigIntensity = 0;
    [SerializeField, Range(0f, 500f)] private float timeBetweenIntensity = 400f;

    void Awake()
    {
        volumeSettings = GetComponent<Volume>();

        if (volumeSettings == null)
        {
            Debug.LogError("No Volume component found on this GameObject.");
            return;
        }

        if (volumeSettings.profile == null)
        {
            Debug.LogError("No Volume Profile assigned to the Volume component.");
            return;
        }

        if (volumeSettings.profile.TryGet(out vignette))
        {
            vignette.active = true;
            vignette.intensity.Override(vigIntensity);
        }
        else
        {
            Debug.LogError("Vignette component not found in the volume profile.");
        }
    }

    public void SetVignetteIntensity(float intensity)
    {
        if (vignette != null)
        {
            vignette.intensity.Override(intensity);
        }
    }

    void Update()
    {
        LightFlickering.flickerTimer += Time.deltaTime;

        if (LightFlickering.flickerTimer >= timeBetweenIntensity)
        {
            vigIntensity = Mathf.Clamp01(vigIntensity + 0.1f);
            SetVignetteIntensity(vigIntensity);
        }

        if (LightFlickering.timer < 60)
        {
            timeBetweenIntensity = 0.5f;
        }
    }
}
