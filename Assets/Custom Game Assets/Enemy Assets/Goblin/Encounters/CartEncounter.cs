using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEncounter : MonoBehaviour
{
    public HordeLogic hordeToKill;
    public ProjectileScript arrow;
    public GameObject player;
    public GameObject cutscenePlayer;
    public Camera cutsceneCamera;
    public PlayerMovementScript playerMovementScript;
    public Animator animator;
    public GameObject questMarker;
    public GameObject interactPopup;
    public OpenDoorEncounterScript gateAfterFinishing;

    private Camera mainCamera;
    private Collider col;
    private DialogBoxManager dialogBoxManager;

    private bool doorlocked;

    private void Start()
    {
        mainCamera = Camera.main;

        cutsceneCamera.enabled = false;
        arrow.gameObject.SetActive(false);
        hordeToKill.gameObject.SetActive(false);
        interactPopup.SetActive(false);
        cutscenePlayer.SetActive(false);

        col = gameObject.GetComponent<Collider>();
        dialogBoxManager = FindObjectOfType<DialogBoxManager>();

        doorlocked = true;
    }

    private void Update()
    {
        if (interactPopup.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PlayCutscene());
            
            interactPopup.SetActive(false);
            questMarker.SetActive(false);

            col.enabled = false;
        }

        if (hordeToKill.enemies.Count == 0 && hordeToKill.transform.gameObject.activeInHierarchy && doorlocked)
        {
            doorlocked = false;
            StartCoroutine(gateAfterFinishing.PlayCutscene(playerMovementScript));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactPopup.SetActive(true);
        questMarker.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        interactPopup.SetActive(false);
        questMarker.SetActive(true);
    }

    IEnumerator PlayCutscene() 
    {
        yield return new WaitForSeconds(0.2f);

        playerMovementScript.PlayerLock(true);
        playerMovementScript.gameObject.SetActive(false);
        cutscenePlayer.SetActive(true);
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

        yield return new WaitForSeconds(3f);
        arrow.gameObject.SetActive(true);
        arrow.AddForce();
        hordeToKill.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        cutscenePlayer.SetActive(false);

        mainCamera.enabled = true;
        cutsceneCamera.enabled = false;
        animator.SetLayerWeight(2, 0f);

        playerMovementScript.gameObject.SetActive(true);
        playerMovementScript.PlayerLock(false);
    }
}
