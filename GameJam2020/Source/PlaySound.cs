using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] audioClips;
    public int audioIndex = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(audioClips[audioIndex], 1f);
    }
}
