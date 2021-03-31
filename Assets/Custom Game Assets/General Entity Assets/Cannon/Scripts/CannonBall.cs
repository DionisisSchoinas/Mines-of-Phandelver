using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;
    public float damage;
    public ElementTypes.Type elementType;

    public List<ParticleSystem> particles;
    public Transform explosionRadius;
    public float radius;

    private void Awake()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter()
    {
        Debug.Log("Boom");

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

        Destroy(gameObject, 0.1f);
    }

    private void OnDrawGizmos()
    {
        if (explosionRadius == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(explosionRadius.position, radius);
    }
}
