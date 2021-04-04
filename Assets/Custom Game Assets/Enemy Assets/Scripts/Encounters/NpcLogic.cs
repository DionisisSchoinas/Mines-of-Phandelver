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
    private Collider col;
    private PlayerMovementScript player;
    private Transform target;

    private void Start()
    {
        questionMark.SetActive(false);
        interactPopup.SetActive(false);

        col = gameObject.GetComponent<Collider>();
        col.enabled = false;

        mainCamera = Camera.main;
        animator = gameObject.GetComponent<Animator>();

        UIEventSystem.current.onFinishedDialog += FinishedDialog;
        CharacterLoadScript.current.onCharacterSelected += CharacterSelected;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onFinishedDialog -= FinishedDialog;
        CharacterLoadScript.current.onCharacterSelected -= CharacterSelected;
    }

    private void Update()
    {
        if (player == null)
            return;

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
        yield return new WaitForSeconds(0.2f);

        animator.SetTrigger("Idle");
        player.PlayerLock(true);
        mainCamera.enabled = false;
        RotateTowardsNpc();
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
        if (target == null)
            return;

        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.eulerAngles = rotation.eulerAngles;
    }

    public void RotateTowardsNpc()
    {
        Vector3 lookPos = transform.position - player.gameObject.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        player.gameObject.transform.eulerAngles = rotation.eulerAngles;
    }

    private void CharacterSelected(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        player = playerMovementScript;
        target = playerMovementScript.gameObject.transform;
    }
}
