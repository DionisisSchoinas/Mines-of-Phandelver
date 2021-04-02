using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public float smoothSpeed = 2f;

    private Transform playerPosition;

    private void Start()
    {
        playerPosition = FindObjectOfType<PlayerMovementScript>().gameObject.transform;
    }

    void FixedUpdate()
    {
        if (playerPosition == null)
            return;

        Vector3 targetPosition = playerPosition.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(playerPosition);
    }
}
