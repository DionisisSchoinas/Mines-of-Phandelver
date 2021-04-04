using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterGateEncounter : MonoBehaviour
{
    public int dialogIndex;

    public Camera cutsceneCamera;
    public GameObject cutsceneBossMonster;

    public OpenDoorScript areaEntranceGate;
    public SmashObstaclesScript gatesToSmash;
    public HordeLogic hordeToKill;
    public HealthController bossMonster;
    public Cannon cannon;
    public GameObject cannonFireCenter;
    public float cannonRadius = 5f;

    public float minCannonIntervals;
    public float maxCannonIntervals;
    private int fightStage;

    private PlayerMovementScript playerMovementScript;
    private Camera mainCamera;
    private Collider col;
    private Coroutine cannonCoroutine;

    private bool encounterRunning;

    private void Awake()
    {
        mainCamera = Camera.main;

        col = gameObject.GetComponent<Collider>();

        cutsceneCamera.enabled = false;
        cutsceneBossMonster.SetActive(true);
        hordeToKill.gameObject.SetActive(false);
        cannon.gameObject.SetActive(false);

        fightStage = 0;
        encounterRunning = false;

        minCannonIntervals = Mathf.Max(2f, minCannonIntervals);
        maxCannonIntervals = Mathf.Max(minCannonIntervals + 0.5f, maxCannonIntervals);
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

    private void OnTriggerEnter(Collider other)
    {
        TriggerEncounter();
        col.enabled = false;
    }

    private void Update()
    {
        if (encounterRunning && hordeToKill.transform.gameObject.activeInHierarchy) 
        {
            if (bossMonster.currentValue <= bossMonster.maxValue * 0.25)
                fightStage = 3;
            else if (bossMonster.currentValue <= bossMonster.maxValue * 0.50)
                fightStage = 2;
            else if (bossMonster.currentValue <= bossMonster.maxValue * 0.75)
                fightStage = 2;
            else
                fightStage = 1;

            if (hordeToKill.enemies.Count == 0)
            {
                EndEncounter();
            } 
        }
    }

    private void TriggerEncounter()
    {
        encounterRunning = true;

        StartCoroutine(PlayDialogCutscene());
    }

    private void EndEncounter()
    {
        encounterRunning = false;

        areaEntranceGate.Open();

        if (cannonCoroutine != null)
            StopCoroutine(cannonCoroutine);
        StartCoroutine(FireCannon(5, true));
    }

    private IEnumerator FireCannon(int shots, bool explodeAfter)
    {
        for (int i=0; i<shots; i++)
        {
            Vector2 randompoint = Random.insideUnitCircle * cannonRadius;
            Vector3 firePoint = new Vector3(randompoint.x, 0, randompoint.y);

            cannon.Fire(cannonFireCenter.transform.position + firePoint);

            yield return new WaitForSeconds(0.3f);
        }

        if (explodeAfter)
        {
            cannon.Explode();
            gatesToSmash.Smash(cannon.gameObject.transform.position);
        }
    }

    private IEnumerator PlayDialogCutscene()
    {
        yield return new WaitForSeconds(0.1f);

        areaEntranceGate.Close();

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

        cutsceneCamera.enabled = false;

        hordeToKill.gameObject.SetActive(true);
        cannon.gameObject.SetActive(true);

        foreach (Transform transform_child in hordeToKill.transform)
        {
            transform_child.gameObject.GetComponent<EnemyAi_V2>().target = playerMovementScript.gameObject.transform;
            transform_child.gameObject.GetComponent<Animator>().SetBool("Chase", true);
        }

        cutsceneBossMonster.gameObject.SetActive(false);

        mainCamera.enabled = true;

        playerMovementScript.PlayerLock(false);

        cannonCoroutine = StartCoroutine(CannonTimer());
    }

    private IEnumerator CannonTimer()
    {
        while (encounterRunning)
        {
            yield return new WaitForSeconds(Random.Range(minCannonIntervals, maxCannonIntervals));
            StartCoroutine(FireCannon(fightStage, false));
        }
    }

    private void OnDrawGizmos()
    {
        if (cannonFireCenter != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(cannonFireCenter.transform.position, cannonRadius);
        }
    }
}
