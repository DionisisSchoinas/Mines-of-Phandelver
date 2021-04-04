using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLogic : MonoBehaviour
{
    public int dialogIndex;

    public Camera interactionCamera;
    public HordeLogic hordeLogic;
    public GameObject questionMark;
    public GameObject interactPopup;

    private Animator animator;
    private Camera mainCamera;
    private Transform target;
    private Collider col;
    private PlayerMovementScript player;

    private void Start()
    {
        GameObject[] transforms = GameObject.FindGameObjectsWithTag("Player");

        if (transforms.Length != 0)
        {
            player = transforms[0].GetComponent<PlayerMovementScript>();
            target = transforms[0].transform;
        }
        questionMark.SetActive(false);
        interactPopup.SetActive(false);

        col = gameObject.GetComponent<Collider>();
        col.enabled = false;

        mainCamera = Camera.main;
        animator = gameObject.GetComponent<Animator>();

        UIEventSystem.current.onFinishedDialog += FinishedDialog;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onFinishedDialog -= FinishedDialog;
    }

    private void Update()
    {
        Debug.Log("Npc : " + (interactPopup.activeInHierarchy && !player.playerLocked && Input.GetKeyDown(KeyCode.E)));
        if (interactPopup.activeInHierarchy && !player.playerLocked && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PlayDialogCutscene());

            interactPopup.SetActive(false);
            questionMark.SetActive(false);

            col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactPopup.SetActive(true);
        questionMark.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (questionMark.activeSelf)
            questionMark.SetActive(false);

        if (interactPopup.activeSelf)
            interactPopup.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactPopup.SetActive(false);
        questionMark.SetActive(true);
    }

    public void EncounterEnded()
    {
        col.enabled = true;
        questionMark.SetActive(true);
    }

    private IEnumerator PlayDialogCutscene()
    {
        Debug.Log("Open dialog");

        yield return new WaitForSeconds(0.2f);

        animator.SetTrigger("Idle");
        player.PlayerLock(true);
        mainCamera.enabled = false;
        interactionCamera.enabled = true;

        UIEventSystem.current.ShowDialog(dialogIndex);
    }

    private void FinishedDialog(int index)
    {
        if (dialogIndex == index)
        {
            StartCoroutine(PlayCombatStartCutscene());
        }
    }

    private IEnumerator PlayCombatStartCutscene()
    {
        yield return new WaitForSeconds(0.2f);

        interactionCamera.enabled = false;
        mainCamera.enabled = true;
        player.PlayerLock(false);
    }

    public void RotateTowardsPlayer()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.eulerAngles = rotation.eulerAngles;
    }
}
