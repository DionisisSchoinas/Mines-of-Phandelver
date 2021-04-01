using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellEcnounterCutscene : MonoBehaviour
{
    public Camera cameraCutscene;

    private PlayerMovementScript playerMovementScript;
    private Camera cameraMain;
    private Collider col;

    // Start is called before the first frame update
    private void Start()
    {
        cameraMain = Camera.main;
        cameraCutscene.enabled = false;

        col = gameObject.GetComponent<Collider>();

        CharacterLoadScript.current.onCharacterSelected += SetCharacter;
    }

    private void OnDestroy()
    {
        CharacterLoadScript.current.onCharacterSelected -= SetCharacter;
    }

    private void SetCharacter(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        this.playerMovementScript = playerMovementScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;

        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        playerMovementScript.PlayerLock(true);
        cameraMain.enabled = false;
        cameraCutscene.enabled = true;

        yield return new WaitForSeconds(5f);

        cameraMain.enabled = true;
        cameraCutscene.enabled = false;
        playerMovementScript.PlayerLock(false);
    }
}
