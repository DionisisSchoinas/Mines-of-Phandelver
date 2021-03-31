﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorEncounterScript : MonoBehaviour
{
    public HordeLogic hordeToKill;
    public OpenDoorScript script;
    public Camera cutsceneCamera;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        cutsceneCamera.enabled = false;

       // StartCoroutine(PlayCutscene(null));
    }

    public IEnumerator PlayCutscene(PlayerMovementScript movementScript)
    {
        yield return new WaitForSeconds(2f);

        if (movementScript != null)
            movementScript.PlayerLock(true);

        cutsceneCamera.enabled = true;
        mainCamera.enabled = false;

        yield return new WaitForSeconds(2f);

        script.Open();

        yield return new WaitForSeconds(4f);

        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;

        if (movementScript != null)
            movementScript.PlayerLock(false);
    }
}
