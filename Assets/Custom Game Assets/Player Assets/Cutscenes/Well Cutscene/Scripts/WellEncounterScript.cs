using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellEncounterScript : MonoBehaviour
{
    public GameObject hordeToDespawn;
    public GameObject hordeToSpawn;

    private PlayerMovementScript playerMovementScript;
    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        hordeToSpawn.SetActive(false);

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

        hordeToDespawn.SetActive(false);
        hordeToSpawn.SetActive(true);

        foreach (Transform transform_child in hordeToSpawn.transform)
        {
            transform_child.gameObject.GetComponent<EnemyAi_V2>().target = playerMovementScript.gameObject.transform;
            transform_child.gameObject.GetComponent<Animator>().SetBool("Chase", true);
        }
    }
}
