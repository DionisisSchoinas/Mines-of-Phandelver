using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeDisplay : MonoBehaviour
{
    private Image buttonicon;
    private Image buttonImageCooldown;

    private void Awake()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();

        buttonicon = images[0];
        buttonImageCooldown = images[1];
        buttonImageCooldown.fillAmount = 0;
    }

    private void Start()
    {
        CharacterLoadScript.current.onCharacterSelected += SetIcon;
        UIEventSystem.current.onDodgeFinish += Cooldown;
    }

    private void OnDestroy()
    {
        CharacterLoadScript.current.onCharacterSelected -= SetIcon;
        UIEventSystem.current.onDodgeFinish -= Cooldown;
    }

    private void SetIcon(SelectedCharacterScript.Character character)
    {
        switch (character)
        {
            case SelectedCharacterScript.Character.Fighter:
                buttonicon.sprite = ResourceManager.UI.SkillIcons.Dodge.Roll;
                break;
            default:
                buttonicon.sprite = ResourceManager.UI.SkillIcons.Dodge.Dash;
                break;
        }
    }

    private void Cooldown(float cooldown)
    {
        StartCoroutine(StartCooldown(cooldown));
    }

    private IEnumerator StartCooldown(float cooldown)
    {
        float i = 0f;
        float delayForEachStep = cooldown / 50f;
        while (i < 1)
        {
            i += 0.02f;
            buttonImageCooldown.fillAmount += 0.02f;
            yield return new WaitForSeconds(delayForEachStep);
        }
        buttonImageCooldown.fillAmount = 0;
    }
}
