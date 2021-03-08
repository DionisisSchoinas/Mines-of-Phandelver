using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthEventSystem : MonoBehaviour
{
    public static HealthEventSystem current;

    private void Awake()
    {
        current = this;
    }

    // Deals damage
    public event Action<string, float, int> onTakeDamage;
    public void TakeDamage(string name, float damage, int damageType)
    {
        if (onTakeDamage != null)
        {
            onTakeDamage(name, damage, damageType);
        }
    }
    // Triggers on death
    public event Action<string> onDeath;
    public void Die(string name)
    {
        if (onDeath != null)
        {
            onDeath(name);
        }
    }

    // Sets the invunarablility state
    public event Action<string, bool> onChangeInvunerability;
    public void SetInvunerable(string name, bool state)
    {
        if (onChangeInvunerability != null)
        {
            onChangeInvunerability(name, state);
        }
    }
    // Applies a condition
    public event Action<string, Condition> onConditionHit;
    public void SetCondition(string name, Condition condition)
    {
        if (onConditionHit != null)
        {
            onConditionHit(name, condition);
        }
    }
    // Applies force
    public event Action<string, Vector3, float> onForceApply;
    public void ApplyForce(string name, Vector3 direction, float magnitude)
    {
        if (onForceApply != null)
        {
            onForceApply(name, direction.normalized, magnitude);
        }
    }
    // Applies resistance
    public event Action<string, List<SkinnedMeshRenderer>, Material, int, float> onResistanceApply;
    public void ApplyResistance(string name, List<SkinnedMeshRenderer> mesh, Material newMaterial, int resistance, float duration)
    {
        if (onResistanceApply != null)
        {
            onResistanceApply(name, mesh, newMaterial, resistance, duration);
        }
    }
    // Sends out current resistances
    public event Action<string, List<int>> onResistanceUpdate;
    public void UpdateResistance(string name, List<int> resistances)
    {
        if (onResistanceUpdate != null)
        {
            onResistanceUpdate(name, resistances);
        }
    }
}
