using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashObstaclesScript : MonoBehaviour
{
    private List<Rigidbody> rigidbodies;
    private Collider col;

    private void Awake()
    {
        rigidbodies = new List<Rigidbody>();
        col = gameObject.GetComponent<Collider>();

        Transform[] gameObjects = gameObject.GetComponentsInChildren<Transform>();

        for (int i=1; i<gameObjects.Length; i++)
        {
            rigidbodies.Add(gameObjects[i].GetComponent<Rigidbody>());
        }

        col.enabled = true;
        SetGates(false);
    }

    public void Smash(Vector3 forceOrigin)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(200f, forceOrigin, 10f, 10f, ForceMode.VelocityChange);
            rb.useGravity = true;
        }
        col.enabled = false;
    }

    private void SetGates(bool enabled)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.useGravity = enabled;
        }
    }
}
