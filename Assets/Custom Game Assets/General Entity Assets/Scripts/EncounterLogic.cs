using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterLogic : MonoBehaviour
{
    ObjectiveManager objectiveManager;
    // Start is called before the first frame update
    void Start()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        StartCoroutine(FirstObjective());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FirstObjective()
    {
        yield return new WaitForSeconds(2f);
        objectiveManager.SetUpQuest();
    }
}
