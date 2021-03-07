using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyMeleeController : MonoBehaviour
{
    AttackIndicator indicator;
    public bool canAttack;//check if the cooldown has passed
    public float attackDelay = 0.5f;
    public float swingSoundDelay = 0.5f;

    private List<AudioClip> swingSounds;
    private AudioSource swingAudioSource;
    private Coroutine swingSoundCoroutine;

    private AudioSource hitAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GetComponent<AttackIndicator>() as AttackIndicator;

        swingAudioSource = gameObject.AddComponent<AudioSource>();
        swingAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", swingAudioSource, ResourceManager.Audio.AudioSources.Range.Short);

        hitAudioSource = gameObject.AddComponent<AudioSource>();
        hitAudioSource.volume = 0.5f;
        hitAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", hitAudioSource, ResourceManager.Audio.AudioSources.Range.Short);

        swingSounds = new List<AudioClip>();
        swingSounds.Add(ResourceManager.Audio.Sword.Swing1);
        swingSounds.Add(ResourceManager.Audio.Sword.Swing2);

        canAttack = false;
    }

    void FixedUpdate()
    {
        if (canAttack)
        {
            StartCoroutine(PerformAttack());
            canAttack = false;
        }
    }

    IEnumerator PerformAttack()
    {
        PlaySwordSwingAudio();

        yield return new WaitForSeconds(attackDelay);

        int col = 0;

        foreach (Transform visibleTarget in indicator.visibleTargets)
        {
            if ((visibleTarget.gameObject.layer.Equals(BasicLayerMasks.Enemies) || visibleTarget.gameObject.layer.Equals(BasicLayerMasks.DamageablesLayer)) && col == 0)
                col = 1;
            else if (col == 0)
                col = 2;

            HealthEventSystem.current.TakeDamage(visibleTarget.name, 15f, DamageTypesManager.Physical);
        }

        switch (col)
        {
            case 1: // Hit damagable object
                hitAudioSource.clip = ResourceManager.Audio.Sword.HitFlesh;
                hitAudioSource.Play();
                break;
            case 2: // Hit terrain
                hitAudioSource.clip = ResourceManager.Audio.Sword.HitObject;
                hitAudioSource.Play();
                break;
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void PlaySwordSwingAudio()
    {
        swingAudioSource.Stop();
        int randomSwing = Random.Range(0, swingSounds.Count - 1);
        swingAudioSource.clip = swingSounds[randomSwing];

        if (swingSoundCoroutine != null)
            StopCoroutine(swingSoundCoroutine);
        swingSoundCoroutine = StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(swingSoundDelay);
        swingAudioSource.Play();
    }
}
