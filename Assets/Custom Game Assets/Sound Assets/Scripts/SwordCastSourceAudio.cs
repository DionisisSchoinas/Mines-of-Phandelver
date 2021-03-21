using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCastSourceAudio : MonoBehaviour
{
    public ElementTypes.Type elementType;

    private AudioSource audioSource;
    private AudioClip[] swingAudioClips;

    private void Start()
    {
        SpawnAudio();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource, ResourceManager.Audio.AudioSources.Range.Short);

        audioSource.clip = swingAudioClips[Random.Range(0, swingAudioClips.Length)];

        audioSource.Play();
    }

    public void SpawnAudio()
    {
        switch (elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                swingAudioClips = ResourceManager.Audio.Spells.Earth.Swings;
                break;
            case ElementTypes.Type.Cold_Ice:
                swingAudioClips = ResourceManager.Audio.Spells.Ice.Swings;
                break;
            case ElementTypes.Type.Lightning:
                swingAudioClips = ResourceManager.Audio.Spells.Lightning.Swings;
                break;
            default:
                swingAudioClips = ResourceManager.Audio.Spells.Fire.Swings;
                break;
        }
    }
}
