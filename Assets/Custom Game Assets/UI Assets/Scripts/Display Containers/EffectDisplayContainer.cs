using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplayContainer : MonoBehaviour
{
    private Image cooldownDisplay;
    private Image iconDisplay;
    private Coroutine coroutine;
    private float cooldownPercentage;
    private bool resistance;

    private void Awake()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        iconDisplay = images[0];
        cooldownDisplay = images[1];

        cooldownDisplay.fillAmount = 1f;
        cooldownPercentage = 1f;

        coroutine = null;
        resistance = false;

        UIEventSystem.current.onRemoveResistance += RemoveResistance;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onRemoveResistance -= RemoveResistance;
    }

    public void SetResistance(Sprite icon)
    {
        iconDisplay.sprite = icon;
        resistance = true;
    }

    public void StartCountdown(float duration)
    {
        coroutine = StartCoroutine(StartTimer(duration));
    }

    private IEnumerator StartTimer(float duration)
    {
        cooldownPercentage = 1f;

        float delayForEachStep = duration / 100f;
        while (cooldownPercentage > 0)
        {
            cooldownPercentage -= 0.01f;
            cooldownDisplay.fillAmount = cooldownPercentage;
            yield return new WaitForSeconds(delayForEachStep);
        }

        cooldownPercentage = 1f;
    }

    private void RemoveResistance()
    {
        if (resistance)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            Destroy(gameObject);
        }
    }
}
