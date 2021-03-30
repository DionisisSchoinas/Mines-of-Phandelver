using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorEncounterScript : MonoBehaviour
{
    public HordeLogic hordeToKill;
    public GameObject hordeToSpawn;
    public OpenDoorScript script;
    bool doorLock=true;
    public Camera mainCamera;
    public Camera cutsceneCamera;
    void Start()
    {
        hordeToSpawn.SetActive(false); 
        cutsceneCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hordeToKill.enemies.Count == 0 && hordeToKill.transform.gameObject.activeInHierarchy && doorLock) { 
            doorLock = false;
            StartCoroutine(PlayCutscene());
        }
    }
    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(2f);
        cutsceneCamera.enabled = true;
        mainCamera.enabled = false;
        script.Open();
        yield return new WaitForSeconds(3f);
        hordeToSpawn.SetActive(true);
        foreach(Transform transform_child in hordeToSpawn.transform)
        {
            transform_child.gameObject.GetComponent<Animator>().SetBool("Chase",true);
        }
        yield return new WaitForSeconds(3f);
        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;
    }
}
