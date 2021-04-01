using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellEcnounterCutscene : MonoBehaviour
{
    public Camera cameraCutscene;
    public PlayerMovementScript player;

    private Camera cameraMain;
    private Collider col;

    // Start is called before the first frame update
    private void Start()
    {
        cameraMain = Camera.main;
        cameraCutscene.enabled = false;

        col = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;

        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        player.PlayerLock(true);
        cameraMain.enabled = false;
        cameraCutscene.enabled = true;

        yield return new WaitForSeconds(5f);

        cameraMain.enabled = true;
        cameraCutscene.enabled = false;
        player.PlayerLock(false);
    }
}
