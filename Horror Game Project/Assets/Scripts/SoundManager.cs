using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlaySoundEffectClip(AudioClip audioClip, Transform spawnTransform, float volume, float time)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;
        if (time <= 0)
            Destroy(audioSource.gameObject, clipLength);
        else
            Destroy(audioSource.gameObject, time);


    }

    private IEnumerator ForWalking()
    {
        yield return new WaitForSeconds(1f);
    }
}