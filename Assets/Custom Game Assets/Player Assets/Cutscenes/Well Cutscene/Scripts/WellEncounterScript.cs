using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellEncounterScript : MonoBehaviour
{
    bool firstTime = true;
    public GameObject hordeToDespawn;
    public GameObject hordeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        { 
            hordeToDespawn.SetActive(false);
            hordeToSpawn.SetActive(true);
            firstTime = false;
        }
    }
}
