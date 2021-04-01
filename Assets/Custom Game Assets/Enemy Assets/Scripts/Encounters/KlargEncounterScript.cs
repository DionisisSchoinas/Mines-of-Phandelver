using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlargEncounterScript : MonoBehaviour
{
    private Collider col;

    public int dialogIndex;

    public Camera cutsceneCamera;
    public GameObject cutsceneBossMonster;

    public HordeLogic hordeToKill;
    public GameObject bridgeToCollapse;
    public Collider blockEntrance;

    private PlayerMovementScript playerMovementScript;
    private Camera mainCamera;
    private bool encounterRunning;

    private void Awake()
    {
        mainCamera = Camera.main;
        col = gameObject.GetComponent<Collider>();

        cutsceneCamera.enabled = false;
        cutsceneBossMonster.SetActive(true);
        hordeToKill.gameObject.SetActive(false);

        encounterRunning = false;
        blockEntrance.enabled = false;
    }

    private void Start()
    {
        UIEventSystem.current.onFinishedDialog += FinishedDialog;
        CharacterLoadScript.current.onCharacterSelected += SetCharacter;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onFinishedDialog -= FinishedDialog;
        CharacterLoadScript.current.onCharacterSelected -= SetCharacter;
    }

    private void SetCharacter(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        this.playerMovementScript = playerMovementScript;
    }

    private void Update()
    {
        if (encounterRunning && hordeToKill.transform.gameObject.activeInHierarchy && hordeToKill.enemies.Count == 0)
        {
            EndEncounter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;
        StartCoroutine(PlayDialogCutscene());
    }
    private IEnumerator PlayDialogCutscene()
    {
        yield return new WaitForSeconds(0.1f);

        playerMovementScript.PlayerLock(true);

        cutsceneBossMonster.SetActive(true);
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

        UIEventSystem.current.ShowDialog(dialogIndex);
    }

    private void FinishedDialog(int index)
    {
        if (dialogIndex == index)
        {
            StartCoroutine(StartCombat());
        }
    }

    private IEnumerator StartCombat()
    {
        yield return new WaitForSeconds(1f);

        blockEntrance.enabled = true;
        CollapseBridge();

        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;

        cutsceneBossMonster.gameObject.SetActive(false);
        playerMovementScript.PlayerLock(false);

        hordeToKill.gameObject.SetActive(true);

        foreach (Transform transform_child in hordeToKill.transform)
        {
            transform_child.gameObject.GetComponent<EnemyAi_V2>().target = playerMovementScript.gameObject.transform;
            transform_child.gameObject.GetComponent<Animator>().SetBool("Chase", true);
        }

        encounterRunning = true;
    }

    private void CollapseBridge()
    {
        GameObject copy = Instantiate(bridgeToCollapse, bridgeToCollapse.transform.position, bridgeToCollapse.transform.rotation);
        Transform[] parts = copy.GetComponentsInChildren<Transform>();
        for(int i=2; i<parts.Length; i++)
        {
            Rigidbody rb = parts[i].gameObject.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.useGravity = true;
            rb.AddExplosionForce(Random.Range(10f, 20f), parts[0].position + Random.onUnitSphere * 2f, 15f, 5f, ForceMode.VelocityChange);
        }

        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
        audioSource.gameObject.transform.position = gameObject.transform.position;
        audioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource, ResourceManager.Audio.AudioSources.Range.Mid);
        audioSource.clip = ResourceManager.Audio.BridgeBreak;
        audioSource.Play();

        Destroy(copy, 5f);
        Destroy(bridgeToCollapse);
    }

    private void EndEncounter()
    {
        encounterRunning = false;
        UIEventSystem.current.GameEnded();
    }
}
