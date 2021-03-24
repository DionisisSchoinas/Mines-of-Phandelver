using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVegetationScript : MonoBehaviour
{
    public List<GameObject> Vegetation;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > Random.Range(1f, 3f)) 
        {
            int index = Random.Range(0, Vegetation.Count);
            Destroy(Instantiate(Vegetation[index], transform), 10f);
            timer = 0;

        }
      
    }
    
}
