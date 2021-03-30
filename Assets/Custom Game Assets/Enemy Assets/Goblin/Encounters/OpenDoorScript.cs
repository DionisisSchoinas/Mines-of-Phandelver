using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    public bool opening = false;
    public bool closing = false;
    public float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed, Space.World);
            if (transform.rotation.z <= 0)
            {
                opening = false;
            }
        }
        else if (closing)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed, Space.World);
            Debug.Log(transform.rotation.z);
            if (transform.rotation.z >= 0.7)
            {
                closing = false;
            }
        }

    }
    public void Open()
    {
        opening = true;
        Debug.Log(1);
    }
    public void Close()
    {
        opening = false;
    }
}
