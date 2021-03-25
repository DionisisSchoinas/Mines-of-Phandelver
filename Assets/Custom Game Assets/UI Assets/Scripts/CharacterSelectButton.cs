using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButton : ButtonSoundHover
{
    public SelectedCharacterScript.Character character;
    public Light[] lights;

    private CharacterSelectMenu menu;

    private void Start()
    {
        SetLights(false);
        menu = gameObject.GetComponentInParent<CharacterSelectMenu>();
        menu.AddButton(this);

        gameObject.GetComponent<Button>().onClick.AddListener(PickCharacter);
    }

    private void PickCharacter()
    {
        menu.PickCharacter(character);
    }

    public void SetLights(bool state)
    {
        foreach (Light light in lights)
            light.enabled = state;
    }
}
