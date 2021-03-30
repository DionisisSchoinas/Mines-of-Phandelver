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
    public event Action<int, float, ElementTypes.Type> onTakeDamage;
    public void TakeDamage(int id, float damage, ElementTypes.Type damageType)
    {
        if (onTakeDamage != null)
        {
            onTakeDamage(id, damage, damageType);
        }
    }
    // Triggers on death
    public event Action<int> onDeath;
    public void Die(int id)
    {
        if (onDeath != null)
        {
            onDeath(id);
        }
    }

    // Sets the invunarablility state
    public event Action<int, bool> onChangeInvunerability;
    public void SetInvunerable(int id, bool state)
    {
        if (onChangeInvunerability != null)
        {
            onChangeInvunerability(id, state);
        }
    }
    // Applies a condition
    public event Action<int, Condition> onConditionHit;
    public void SetCondition(int id, Condition condition)
    {
        if (onConditionHit != null)
        {
            onConditionHit(id, condition);
        }
    }
    // Applies force
    public event Action<int, Vector3, float> onForceApply;
    public void ApplyForce(int id, Vector3 direction, float magnitude)
    {
        if (onForceApply != null)
        {
            onForceApply(id, direction.normalized, magnitude);
        }
    }
    // Applies resistance
    public event Action<int, List<SkinnedMeshRenderer>, Material, ElementTypes.Type, float> onResistanceApply;
    public void ApplyResistance(int id, List<SkinnedMeshRenderer> mesh, Material newMaterial, ElementTypes.Type resistance, float duration)
    {
        if (onResistanceApply != null)
        {
            onResistanceApply(id, mesh, newMaterial, resistance, duration);
        }
    }
    // Sends out current resistances
    public event Action<int, List<ElementTypes.Type>> onResistanceUpdate;
    public void UpdateResistance(int id, List<ElementTypes.Type> resistances)
    {
        if (onResistanceUpdate != null)
        {
            onResistanceUpdate(id, resistances);
        }
    }
}
