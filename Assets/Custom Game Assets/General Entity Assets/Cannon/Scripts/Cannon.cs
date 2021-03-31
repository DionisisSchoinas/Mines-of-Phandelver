using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float fireSoundDelay = 0.1f;
    public float fireParticlesDelay = 0.1f;

    public GameObject incomingProjectile;
    public GameObject explosionParticles;
    private ParticleSystem[] explosion;

    private Animator animator;
    private ParticleSystem blazeParticles;

    private void Awake()
    {
        explosion = explosionParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem part in explosion)
        {
            part.gameObject.transform.parent = null;
            part.Stop();
        }
    }

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        blazeParticles = particleSystems[0];
    }

    private void OnDestroy()
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
            Destroy(audioSource);
    }

    public void Fire(Vector3 fireAtPoint)
    {
        animator.SetTrigger("Fire");
        SpawnAudio();
        SpawnParticles();
        StartCoroutine(SpawnIncoming(fireAtPoint));
    }

    public void Explode()
    {
        foreach (ParticleSystem part in explosion)
        {
            part.Play();
            Destroy(part.gameObject, 3f);
        }

        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
        audioSource.gameObject.transform.position = gameObject.transform.position;
        audioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource, ResourceManager.Audio.AudioSources.Range.Mid);
        audioSource.clip = ResourceManager.Audio.Spells.Fire.BigExplosion;
        audioSource.Play();

        Destroy(audioSource.gameObject, 3f);
        Destroy(gameObject, 0.2f);
    }

    private IEnumerator SpawnIncoming(Vector3 position)
    {
        yield return new WaitForSeconds(0.5f);
        SpawnIncomingProjectile(position);
    }

    private void SpawnIncomingProjectile(Vector3 position)
    {
        Destroy(Instantiate(incomingProjectile, position, Quaternion.identity), 10f);
    }

    private void SpawnAudio()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource, ResourceManager.Audio.AudioSources.Range.Mid);
        audioSource.clip = ResourceManager.Audio.CannonFire;
        audioSource.PlayDelayed(fireSoundDelay);
        Destroy(audioSource, 2f);
    }

    private void SpawnParticles()
    {
        StartCoroutine(ParticleDelay());
    }

    private IEnumerator ParticleDelay()
    {
        yield return new WaitForSeconds(fireParticlesDelay);
        blazeParticles.Play(true);
    }
}
