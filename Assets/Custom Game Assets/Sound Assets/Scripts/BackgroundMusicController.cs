using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public enum Location
    {
        Cave,
        City,
        Combat,
        Forest
    }

    public static BackgroundMusicController current;

    private AudioSource audioSource;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        current.audioSource = player.AddComponent<AudioSource>();
        current.audioSource.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Music")[0];
        current.audioSource.loop = true;
        current.audioSource.playOnAwake = true;

        current.onLocationSet += LocationChanged;
    }

    private void OnDestroy()
    {
        current.onLocationSet -= LocationChanged;
    }

    public Action<Location> onLocationSet;
    public void SetLocation(Location location)
    {
        if (onLocationSet != null)
        {
            onLocationSet(location);
        }
    }

    private void LocationChanged(Location location)
    {
        switch (location)
        {
            case Location.Cave:
                current.audioSource.clip = ResourceManager.Audio.Ambient.CaveBackground;
                break;
            case Location.City:
                current.audioSource.clip = ResourceManager.Audio.Ambient.CityBackground;
                break;
            case Location.Combat:
                current.audioSource.clip = ResourceManager.Audio.Ambient.CombatBackground;
                break;
            case Location.Forest:
                current.audioSource.clip = ResourceManager.Audio.Ambient.ForestBackground;
                break;
        }
        current.audioSource.Play();
    }
}
