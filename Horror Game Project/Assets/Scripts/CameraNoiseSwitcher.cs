using UnityEngine;
using Unity.Cinemachine;

public class CameraNoiseSwitcher : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public NoiseSettings newNoiseProfile;
    [SerializeField] private float TriggerTimer = 300f; // Time after which to switch noise profile

    private CinemachineBasicMultiChannelPerlin perlin;

    void Start()
    {
        if (virtualCamera != null && newNoiseProfile != null)
        {
            // Get the noise component attached to the same GameObject
            perlin = virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogWarning("No CinemachineBasicMultiChannelPerlin component found on the virtual camera.");
        }
    }

    void Update()
    {
        if (perlin != null && Time.time > TriggerTimer)
        {
            perlin.NoiseProfile = newNoiseProfile;
        }
    }
}
