using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEncounter : MonoBehaviour
{
    public GameObject horde;
    public ProjectileScript arrow;
    public GameObject player;
    public GameObject cutscenePlayer;
    public Camera cutsceneCamera;
    public PlayerMovementScript PlayerMovementScript;
    public Animator animator;
    public GameObject questMarker;
    public GameObject interactPopup;

    private Camera mainCamera;
    private Collider col;
    private DialogBoxManager dialogBoxManager;

    private void Start()
    {
        mainCamera = Camera.main;

        cutsceneCamera.enabled = false;
        arrow.gameObject.SetActive(false);
        horde.SetActive(false);
        interactPopup.SetActive(false);
        cutscenePlayer.SetActive(false);

        col = gameObject.GetComponent<Collider>();
        dialogBoxManager = FindObjectOfType<DialogBoxManager>();
    }

    private void Update()
    {
        if (interactPopup.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            mainCamera.enabled = false;
            cutsceneCamera.enabled = true;
            player.SetActive(false);
            cutscenePlayer.SetActive(true);
            StartCoroutine(PlayCutscene());
            
            interactPopup.SetActive(false);
            questMarker.SetActive(false);

            col.enabled = false;
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
        PlayerMovementScript.canMove = false;
        
        yield return new WaitForSeconds(3f);
        arrow.gameObject.SetActive(true);
        arrow.AddForce();
        horde.SetActive(true);
        yield return new WaitForSeconds(3f);

        player.SetActive(true);
        cutscenePlayer.SetActive(false);

        mainCamera.enabled = true;
        cutsceneCamera.enabled = false;
        animator.SetLayerWeight(2, 0f);

        PlayerMovementScript.canMove = true;
    }
}
