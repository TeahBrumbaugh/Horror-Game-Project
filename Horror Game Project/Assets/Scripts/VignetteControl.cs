using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlowFadeIn : MonoBehaviour
{
    public RawImage rawImage;
    public float fadeDuration = 300f; // 5 minutes = 300 seconds

    void Start()
    {
        StartCoroutine(FadeIn(rawImage, fadeDuration));
    }

    IEnumerator FadeIn(RawImage image, float duration)
    {
        Color startColor = image.color;
        startColor.a = 0f; // Start fully transparent
        Color endColor = image.color;
        endColor.a = 1f; // End fully opaque

        float time = 0f;

        while (time < duration)
        {
            image.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        image.color = endColor; // Ensure it ends fully opaque
    }
}
