using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milosCamerascript : MonoBehaviour
{
    public Transform playerPosition;
    public Vector3 offset;
    public float smoothSpeed = 2f;



    bool isWalled;

    void FixedUpdate()
    {



        Vector3 targerPosition = playerPosition.position + offset; ;


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targerPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        transform.LookAt(playerPosition);

    }
}
