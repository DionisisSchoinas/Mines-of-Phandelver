using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private void Start()
    {
        UIEventSystem.current.onStartCooldown += StartCooldown;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onStartCooldown -= StartCooldown;
    }

    public void StartCooldown(Skill skill, float delay)
    {
        StartCoroutine(Cooldown(skill, delay));
    }

    private IEnumerator Cooldown(Skill skill, float delay)
    {
        skill.onCooldown = true;
        skill.cooldownPercentage = 0f;

        float step = 0f;
        if (delay <= 1f)
        {
            step = 0.04f;
        }
        else
        {
            step = 0.01f;
        }

        float delayForEachStep = delay * step;
        while (skill.cooldownPercentage < 1)
        {
            skill.cooldownPercentage += step;
            yield return new WaitForSeconds(delayForEachStep);
        }

        skill.cooldownPercentage = 0f;
        skill.onCooldown = false;

        yield return null;
    }
}
