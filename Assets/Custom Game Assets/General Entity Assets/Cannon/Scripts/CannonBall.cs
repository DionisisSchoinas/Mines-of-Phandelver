using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float ballMoveAfterSpawn;
    public float speed;
    public float damage;
    public ElementTypes.Type elementType;

    public List<ParticleSystem> particles;
    public Transform explosionRadius;
    public float radius;

    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();

        Invoke(nameof(Move), ballMoveAfterSpawn);
    }

    private void Move()
    {
        rb.AddForce(Vector3.down * speed, ForceMode.VelocityChange);
        explosionRadius.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void AbleRbGravity()
    {
        rb.useGravity = true;
    }

    private void OnTriggerEnter()
    {
        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
        audioSource.gameObject.transform.position = gameObject.transform.position;
        audioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource, ResourceManager.Audio.AudioSources.Range.Short);
        audioSource.clip = ResourceManager.Audio.Spells.Earth.SmallExplosion;
        audioSource.Play();

        foreach (ParticleSystem part in particles)
        {
            part.transform.position = gameObject.transform.position;
            part.Play();
        }

        Collider[] colliders = Physics.OverlapSphere(explosionRadius.position, radius);

        foreach (Collider col in colliders)
        {
            if (col != null)
            {
                HealthEventSystem.current.TakeDamage(col.gameObject.GetInstanceID(), damage, elementType);
            }
        }

        Destroy(audioSource.gameObject, 3f);
        Destroy(gameObject, 3f);

        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        transform.position += Vector3.down * 0.5f;
        col.enabled = false;

        Invoke(nameof(AbleRbGravity), 0.5f);
    }

    private void OnDrawGizmos()
    {
        if (explosionRadius == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(explosionRadius.position, radius);
    }
}
