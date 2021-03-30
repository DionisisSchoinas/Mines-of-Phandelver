using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEncounter : MonoBehaviour
{
    public GameObject horde;
    public GameObject arrow;
    public GameObject player;
    public GameObject cutscenePlayer;
    bool firstTime = true;
    public Camera mainCamera;
    public Camera cutsceneCamera;
    public PlayerMovementScript PlayerMovementScript;
    public Animator animator;
    DialogBoxManager dialogBoxManager;
    private void Start()
    {
        cutsceneCamera.enabled = false;
        horde.SetActive(false);
        dialogBoxManager = FindObjectOfType<DialogBoxManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        {
          //  dialogBoxManager.StartDialog();
            mainCamera.enabled = false;
            cutsceneCamera.enabled = true;
            player.SetActive(false);
            cutscenePlayer.SetActive(true);
            StartCoroutine(PlayCutscene());
            firstTime = false;
           


        }
    }

    IEnumerator PlayCutscene() 
    {
        PlayerMovementScript.canMove = false;
        
       
        
        yield return new WaitForSeconds(3f);
        arrow.SetActive(true);
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
