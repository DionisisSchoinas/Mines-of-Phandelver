using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private Transform firepoint;
    [SerializeField]
    private ProjectileScript projectile;
    public bool canAttack;
    public float attackDelay = 0.5f;
    public float shootArrowSoundDelay = 0.5f;

    private Collider col;

    private AudioSource shootAudioSource;
    private Coroutine shootCoroutine;


    private void Awake()
    {
        col = gameObject.GetComponent<Collider>();

        shootAudioSource = gameObject.AddComponent<AudioSource>();
        shootAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", shootAudioSource, ResourceManager.Audio.AudioSources.Range.Short);
        shootAudioSource.clip = ResourceManager.Audio.Bow.ShootArrow;

        canAttack = false;
    }

    // Update is called once per frame
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
        PlayBowFireAudio();
        yield return new WaitForSeconds(attackDelay);
        projectile.FireSimple(firepoint, col);
        yield return new WaitForSeconds(0.1f);
    }

    private void PlayBowFireAudio()
    {
        shootAudioSource.Stop();
        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);
        shootCoroutine = StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(shootArrowSoundDelay);
        shootAudioSource.Play();
    }
}
