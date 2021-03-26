using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    public float rotationSpeed;
    public float upDownSize;

    private Transform objectToMove;

    private void Awake()
    {
        objectToMove = gameObject.GetComponentsInChildren<Transform>()[1];
    }

    private void FixedUpdate()
    {
        objectToMove.RotateAround(transform.position, transform.up, Time.deltaTime * rotationSpeed);

        objectToMove.position += new Vector3(0f, Time.deltaTime * Mathf.Sin(Time.time) * upDownSize, 0f);
    }
}
