using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ButtonSoundHover : MonoBehaviour, IPointerEnterHandler
{
    private Button button;
    private AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
            audioSource.Play();
    }

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ResourceManager.UI.Sounds.ButtonHoverEnter;
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        audioSource.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Sound Effects")[0];

        button = gameObject.GetComponent<Button>();
    }
}
