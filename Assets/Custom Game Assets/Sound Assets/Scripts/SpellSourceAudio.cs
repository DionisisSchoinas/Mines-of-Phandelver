using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSourceAudio : MonoBehaviour
{
    public enum Type
    {
        Fire,
        Ice,
        Earth,
        Lightning,
        Energy
    }

    public Type elementType;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource, ResourceManager.Audio.AudioSources.Range.Short);
        audioSource.loop = true;
        switch (elementType)
        {
            case Type.Fire:
                audioSource.clip = ResourceManager.Audio.SpellSources.Fire;
                break;
            case Type.Ice:
                audioSource.clip = ResourceManager.Audio.SpellSources.Ice;
                break;
            case Type.Earth:
                audioSource.clip = ResourceManager.Audio.SpellSources.Earth;
                break;
            case Type.Lightning:
                audioSource.clip = ResourceManager.Audio.SpellSources.Lightning;
                break;
            default:
                audioSource.clip = ResourceManager.Audio.SpellSources.Energy;
                break;
        }
        audioSource.Play();
    }
}
