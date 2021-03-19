﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HealthController : EntityResource
{
    public float deathDelay = 30f;
    public List<ElementTypes.Type> resistances { get; private set; }
    public List<ElementTypes.Type> immunities { get; private set; }

    private Animator animator;
    private Collider col;

    [SerializeField]
    private bool _invulnerable;
    public bool invulnerable
    {
        get => _invulnerable;
        set
        {
            _invulnerable = value;
        }
    }

    [SerializeField]
    private bool _respawn;
    public bool respawn
    {
        get => _respawn;
        set => _respawn = value;
    }

    public new float currentValue
    {
        get => base.currentValue;
        set
        {
            if (value <= 0)
            {
                if (respawn)
                    value = maxValue;
                else if (currentValue > 0f)
                {
                    base.currentValue = 0f;

                    if (animator != null)
                        animator.SetTrigger("Die");

                    HealthEventSystem.current.Die(gameObject.name);
                    col.enabled = false;
                    Destroy(gameObject, deathDelay);
                    return;
                }
            }

            if (currentValue > 0f)
            {
                if (animator != null && value < currentValue)
                { 
                    animator.SetTrigger("Hit");
                    
                }
                  
            }

            base.currentValue = value;
        }
    }

    private ConditionsHandler conditionsHandler;
    private ResistanceHandler resistanceHandler;

    //temp
    Rigidbody rb;

    private new void Awake()
    {
        base.Awake();

        conditionsHandler = gameObject.AddComponent<ConditionsHandler>();
        resistanceHandler = gameObject.AddComponent<ResistanceHandler>();

        animator = gameObject.GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();

        if (immunities == null)
            immunities = new List<ElementTypes.Type>();
        if (resistances == null)
            resistances = new List<ElementTypes.Type>();
    }

    private new void Start()
    {
        base.Start();

        if (currentValue != maxValue)
        {
            if (resourceBar != null)
                resourceBar.SetMaxValue(maxValue);

            currentValue = maxValue;
        }

        HealthEventSystem.current.onTakeDamage += TakeDamage;
        HealthEventSystem.current.onChangeInvunerability += SetInvunerability;
        HealthEventSystem.current.onConditionHit += SetCondition;
        HealthEventSystem.current.onForceApply += ApplyForce;
        HealthEventSystem.current.onResistanceUpdate += UpdateResistances;
    }

    private void OnDestroy()
    {
        HealthEventSystem.current.onTakeDamage -= TakeDamage;
        HealthEventSystem.current.onChangeInvunerability -= SetInvunerability;
        HealthEventSystem.current.onConditionHit -= SetCondition;
        HealthEventSystem.current.onForceApply -= ApplyForce;
        HealthEventSystem.current.onResistanceUpdate -= UpdateResistances;
    }

    public void TakeDamage(string name, float damage, ElementTypes.Type damageType)
    {
        // If controller matches
        if (gameObject.name == name)
        {
            // If controller not invulnerable
            if (!invulnerable)
            {
                currentValue = currentValue - CheckDamageTypes(damage, damageType);
                CameraShake.current.ShakeCamera(0.05f, 0.2f);

            }
        }
    }

    private float CheckDamageTypes(float damage, ElementTypes.Type damageType)
    {
        if (immunities.Contains(damageType))
        {
            return 0f;
        }

        if (resistances.Contains(damageType))
        {
            return damage / 2f;
        }

        return damage;
    }

    public void SetValues(float maxValue, float regenPerSecond, ResourceBar resourceBar, Color barColor, bool respawn, bool invulnerable)
    {
        SetValues(maxValue, regenPerSecond, resourceBar, barColor);
        this.respawn = respawn;
        this.invulnerable = invulnerable;
    }

    private void UpdateResistances(string name, List<ElementTypes.Type> resistances)
    {
        if (gameObject.name == name)
        {
            this.resistances = resistances;
        }
    }

    private void UpdateImmunities(string name, List<ElementTypes.Type> immunities)
    {
        if (gameObject.name == name)
        {
            this.immunities = immunities;
        }
    }

    public void SetInvunerability(string name, bool state)
    {
        if (gameObject.name == name)
        {
            invulnerable = state;
        }
    }

    public void SetCondition(string name, Condition condition)
    {
        if (gameObject.name == name)
        {
            if (invulnerable)
                return;

            conditionsHandler.AddCondition(condition);
        }
    }

    public void ApplyForce(string name, Vector3 direction, float magnitude)
    {
        if (gameObject.name == name)
        {
            if (invulnerable)
                return;

            if (rb != null)
                rb.AddForce(direction.normalized * magnitude, ForceMode.Impulse);
        }
    }
    
    protected override IEnumerator Regen()
    {
        finsihedRegen = false;
        while (currentValue < maxValue)
        {
            currentValue = currentValue + regenPerSecond / 10f;
            yield return new WaitForSeconds(0.1f);
        }
        finsihedRegen = true;
        yield return null;
    }
}
