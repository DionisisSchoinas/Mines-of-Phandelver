using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float fireSoundDelay = 0.1f;
    public float fireParticlesDelay = 0.1f;

    public GameObject incomingProjectile;

    public bool shoot;
    public float shootEvery;
    public int consecutiveShots;
    public float cooldown;

    private Animator animator;
    private ParticleSystem blazeParticles;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        blazeParticles = particleSystems[0];

        StartCoroutine(Shoot());
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
        Debug.Log("boom");
    }

    private IEnumerator SpawnIncoming(Vector3 position)
    {
        yield return new WaitForSeconds(0.5f);
        SpawnIncomingProjectile(position);
    }

    private void SpawnIncomingProjectile(Vector3 position)
    {
        Destroy(Instantiate(incomingProjectile, position, Quaternion.identity), 2f);
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

    private IEnumerator Shoot()
    {
        while (true)
        {
            for (int i = 0; i < consecutiveShots; i++)
            {
                if (shoot)
                    Fire(gameObject.transform.position);
                yield return new WaitForSeconds(Mathf.Max(shootEvery, 0.1f));
            }
            yield return new WaitForSeconds(Mathf.Max(cooldown, 2f));
        }
    }
}
