using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    public Collider doorCollider;
    public bool opening = false;
    public bool closing = false;
    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed, Space.World);
            if (transform.rotation.z <= 0)
            {
                opening = false;
                doorCollider.enabled = false;
            }
        }
        
        if (closing)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (transform.rotation.z >= 0.7)
            {
                closing = false;
            }
        }

    }
    public void Open()
    {
        opening = true;
    }

    public void Close()
    {
        closing = true;
        doorCollider.enabled = true;
    }
}
