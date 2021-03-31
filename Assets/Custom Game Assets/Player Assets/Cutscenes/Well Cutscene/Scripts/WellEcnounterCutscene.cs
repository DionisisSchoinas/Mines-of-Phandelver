using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellEcnounterCutscene : MonoBehaviour
{
    bool firstTime=true;
    public Camera cameraMain;
    public Camera cameraCutscene;
    public GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        cameraCutscene.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        {
            StartCoroutine(Cutscene());
            firstTime = false;
        }
    }
    IEnumerator Cutscene()
    {
        player.SetActive(false);
        cameraMain.enabled = false;
        cameraCutscene.enabled = true;
        yield return new WaitForSeconds(5f);
        cameraMain.enabled = true;
        cameraCutscene.enabled = false;
        player.SetActive(true);
    }
}
