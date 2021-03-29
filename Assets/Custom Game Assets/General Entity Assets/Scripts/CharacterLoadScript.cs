using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoadScript : MonoBehaviour
{
    public static CharacterLoadScript current;

    private void Awake()
    {
        current = this;

        SelectedCharacterScript selectedCharacter = FindObjectOfType<SelectedCharacterScript>();
        if (selectedCharacter != null)
        {
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");

            string character_name = "";

            switch (selectedCharacter.character)
            {
                case SelectedCharacterScript.Character.Fighter:
                    character_name = "Player (Warrior)";
                    break;
                default:
                    character_name = "Player (Wizard)";
                    break;
            }

            foreach (GameObject gm in characters)
            {
                if (gm.name.Equals(character_name))
                {
                    gm.SetActive(true);
                }
                else
                {
                    gm.SetActive(false);
                }
            }

            StartCoroutine(SetCharacter(selectedCharacter.character));
        }
    }

    public event Action<SelectedCharacterScript.Character> onCharacterSelected;
    public void CharacterSelected(SelectedCharacterScript.Character character)
    {
        if (onCharacterSelected != null)
        {
            onCharacterSelected(character);
        }
    }

    private IEnumerator SetCharacter(SelectedCharacterScript.Character character)
    {
        yield return new WaitForSeconds(2f);
        CharacterSelected(character);

    }
}
