using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEncounter : MonoBehaviour
{
    public int dialogIndex;

    public HordeLogic hordeToKill;
    public ProjectileScript arrow;

    public Camera cutsceneCamera;

    public GameObject questMarker;
    public GameObject interactPopup;
    public OpenDoorEncounterScript gateAfterFinishing;

    public List<PlayerIdentityScript> charactersForCutscene;
    private Animator animator;
    private GameObject cutscenePlayer;
    private PlayerMovementScript playerMovementScript;

    private Camera mainCamera;
    private Collider col;

    private bool doorlocked;

    private void Awake()
    {
        foreach (PlayerIdentityScript script in charactersForCutscene)
        {
            script.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;

        cutsceneCamera.enabled = false;
        arrow.gameObject.SetActive(false);
        hordeToKill.gameObject.SetActive(false);
        interactPopup.SetActive(false);

        col = gameObject.GetComponent<Collider>();

        doorlocked = true;

        UIEventSystem.current.onFinishedDialog += FinishedDialog;
        CharacterLoadScript.current.onCharacterSelected += SetCharacter;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onFinishedDialog -= FinishedDialog;
        CharacterLoadScript.current.onCharacterSelected -= SetCharacter;
    }

    private void Update()
    {
        if (interactPopup.activeInHierarchy && !playerMovementScript.playerLocked && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PlaySearchCutscene());
            
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

    private void SetCharacter(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        foreach (PlayerIdentityScript script in charactersForCutscene)
        {
            if (script.character == character)
            {
                cutscenePlayer = script.gameObject;
                animator = script.gameObject.GetComponent<Animator>();
                break;
            }
        }

        this.playerMovementScript = playerMovementScript;
    }

    private IEnumerator PlaySearchCutscene()
    {
        yield return new WaitForSeconds(0.2f);

        playerMovementScript.PlayerLock(true);
        playerMovementScript.gameObject.SetActive(false);
        cutscenePlayer.SetActive(true);
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

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
        yield return new WaitForSeconds(1f);

        arrow.gameObject.SetActive(true);
        arrow.AddForce();

        yield return new WaitForSeconds(0.5f);

        cutscenePlayer.GetComponent<Animator>().SetTrigger("LookAround");

        yield return new WaitForSeconds(1.5f);

        hordeToKill.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        foreach (Transform transform_child in hordeToKill.transform)
        {
            transform_child.gameObject.GetComponent<EnemyAi_V2>().target = playerMovementScript.gameObject.transform;
            transform_child.gameObject.GetComponent<Animator>().SetBool("Chase", true);
        }

        cutscenePlayer.SetActive(false);

        mainCamera.enabled = true;
        cutsceneCamera.enabled = false;
        animator.SetLayerWeight(2, 0f);

        playerMovementScript.gameObject.SetActive(true);
        playerMovementScript.PlayerLock(false);
    }
}
