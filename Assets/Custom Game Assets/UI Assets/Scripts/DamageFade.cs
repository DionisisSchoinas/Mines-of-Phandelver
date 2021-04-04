using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private HealthController playerHealth;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Start()
    {
        CharacterLoadScript.current.onCharacterSelected += CharacterSelected;
        UIEventSystem.current.onDamageFadeGet += GetDamageFade;
    }

    private void OnDestroy()
    {
        CharacterLoadScript.current.onCharacterSelected -= CharacterSelected;
        UIEventSystem.current.onDamageFadeGet -= GetDamageFade;
    }

    private void CharacterSelected(SelectedCharacterScript.Character character, PlayerMovementScript playerMovementScript)
    {
        playerHealth = playerMovementScript.gameObject.GetComponent<HealthController>();
    }

    private void GetDamageFade()
    {
        float alpha = 1 - Mathf.InverseLerp(0f, playerHealth.maxValue, playerHealth.currentValue);

        if (alpha < 0.05)
            alpha = 0f;

        canvasGroup.alpha = alpha;
    }
}
