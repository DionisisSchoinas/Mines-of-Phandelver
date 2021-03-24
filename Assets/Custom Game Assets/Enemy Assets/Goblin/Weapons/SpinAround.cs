using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAround : MonoBehaviour
{
    
    ProjectileScript projectileScript;
    // Start is called before the first frame update
    void Start()
    {
        projectileScript = FindObjectOfType<ProjectileScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!projectileScript.stuck)transform.RotateAround(transform.position, transform.right, Time.deltaTime * 720f);
    }
}
