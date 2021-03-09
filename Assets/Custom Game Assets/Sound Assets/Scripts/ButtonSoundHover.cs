using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class ButtonSoundHover : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ResourceManager.UI.Sounds.ButtonHoverEnter;
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        audioSource.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Sound Effects")[0];
    }
}
