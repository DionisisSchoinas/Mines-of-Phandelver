using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public ElementTypes.Type damageType = ElementTypes.Type.Physical_Earth;
    public float speed = 8f;
    public float damage = 15f;
    public float despawnAfter = 30f;

    public bool stickToTarget = true;

    private Collider col;
    private Rigidbody rb;

    [SerializeField]
    Transform centerOfMass;

    public bool stuck;

    private AudioSource flyAudioSource;

    private ParticleSystem particles;

    private void Awake()
    {
         
        rb = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();

        rb.AddForce(transform.forward * speed, ForceMode.Force);
        rb.centerOfMass = centerOfMass.position;

        stuck = false;

        //PlayFlySound();
    }

    public void SetCaster(Collider caster)
    {
        Physics.IgnoreCollision(col, caster);
    }

    public void FireSimple(Transform firePoint, Collider caster)
    {
        ProjectileScript scr = Instantiate(gameObject, firePoint.position + firePoint.forward * 0.5f, firePoint.rotation).GetComponent<ProjectileScript>();
        scr.SetCaster(caster);
        Destroy(scr.gameObject, 5f);
    }

    void FixedUpdate()
    {
        if (!stuck)
        {
            //transform.RotateAround(transform.position, transform.right, Time.deltaTime * 180f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //flyAudioSource.Stop();
        PlayHitSound();

        stuck = true;
        // Stick to target
        int layer = collision.gameObject.layer;
        if (stickToTarget && (layer.Equals(BasicLayerMasks.EnemiesLayer) || layer.Equals(BasicLayerMasks.DamageablesLayer)) )
        {
            rb.transform.parent = collision.transform;
        }

        if (stickToTarget)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            // Disable colider
            col.enabled = false;
            // Disable particles
            particles.Clear();
            // Move a bit forward
            transform.position += transform.forward;
        }

        // Damage
        HealthEventSystem.current.TakeDamage(collision.gameObject.name, damage, damageType);

        Destroy(gameObject, despawnAfter);
    }

    private void PlayFlySound()
    {
        flyAudioSource = gameObject.AddComponent<AudioSource>();
        flyAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", flyAudioSource, ResourceManager.Audio.AudioSources.Range.Short);
        flyAudioSource.loop = true;
        flyAudioSource.clip = ResourceManager.Audio.Arrow.Fly;
        flyAudioSource.Play();
    }

    private void PlayHitSound()
    {
        GameObject gm = new GameObject();
        gm.transform.position = gameObject.transform.position;
        Destroy(gm, 2f);
        AudioSource hitAudioSource = gm.AddComponent<AudioSource>();
        hitAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", hitAudioSource, ResourceManager.Audio.AudioSources.Range.Short);
        hitAudioSource.volume = 0.7f;
        hitAudioSource.clip = ResourceManager.Audio.Arrow.Hit;
        hitAudioSource.Play();
    }
}