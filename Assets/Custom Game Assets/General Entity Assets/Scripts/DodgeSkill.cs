using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeSkill : Skill
{
    public AudioClip dodgeSoundEffect;
    public string dodgeName;
    public float dodgeCooldown = 0.2f;
    public float dodgeDuration = 0.2f;

    public override string type => "Dodge";
    public override string skillName => dodgeName;
    public override float cooldown => dodgeCooldown;
    public override float duration => dodgeDuration;
    public override float instaCastDelay => 0f;
    public override bool instaCast => false;
    public override float manaCost => 0f;

    private AudioSource dodgeAudioSource;

    public new void Awake()
    {
        base.Awake();

        dodgeAudioSource = gameObject.GetComponent<AudioSource>();
        if (dodgeAudioSource == null)
            dodgeAudioSource = gameObject.AddComponent<AudioSource>();

        dodgeAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", dodgeAudioSource, ResourceManager.Audio.AudioSources.Range.Short);

        if (dodgeSoundEffect != null)
            dodgeAudioSource.clip = dodgeSoundEffect;
    }

    public new void StartCooldown()
    {
        StartCooldownWithoutEvent(cooldown);
    }

    public void PlayAudio()
    {
        dodgeAudioSource.Stop();
        dodgeAudioSource.Play();
    }

    public override Sprite GetIcon()
    {
        return ResourceManager.UI.SkillIcons.Dodge.Roll;
    }
}
