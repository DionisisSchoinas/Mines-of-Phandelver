using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoadScript : MonoBehaviour
{
    public static CharacterLoadScript current;

    private GameObject characterGm;

    private void Awake()
    {
        current = this;

        SelectedCharacterScript selectedCharacter = FindObjectOfType<SelectedCharacterScript>();
        if (selectedCharacter != null)  // On correct load of game
        {
            PlayerIdentityScript[] characters = FindObjectsOfType<PlayerIdentityScript>();

            foreach (PlayerIdentityScript script in characters)
            {
                if (script.thisIsTheCharacter && script.character == selectedCharacter.character)
                {
                    script.gameObject.SetActive(true);
                    characterGm = script.gameObject;
                }
                else
                {
                    script.gameObject.SetActive(false);
                }
            }

            StartCoroutine(SetCharacter(selectedCharacter.character, characterGm));
        }
        else // On singel scene load
        {
            PlayerIdentityScript[] characters = FindObjectsOfType<PlayerIdentityScript>();

            foreach (PlayerIdentityScript character in characters)
            {
                if (character.thisIsTheCharacter)
                {
                    StartCoroutine(SetCharacter(character.character, character.gameObject));
                    break;
                }
            }
        }
    }

    public event Action<SelectedCharacterScript.Character, PlayerMovementScript> onCharacterSelected;
    public void CharacterSelected(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        if (onCharacterSelected != null)
        {
            onCharacterSelected(character, playerMovementScript);
        }
    }

    private IEnumerator SetCharacter(SelectedCharacterScript.Character character, GameObject player)
    {
        PlayerMovementScript playerMovementScript = player.GetComponent<PlayerMovementScript>();
        playerMovementScript.PlayerLock(true);
        yield return new WaitForSeconds(2f);
        CharacterSelected(character, playerMovementScript);
        playerMovementScript.PlayerLock(false);
    }
}
