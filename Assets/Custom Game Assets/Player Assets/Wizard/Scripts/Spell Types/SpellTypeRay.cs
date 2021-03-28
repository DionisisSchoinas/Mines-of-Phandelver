using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTypeRay : Spell
{
    public float damage = 10f;
    public int damageTicksPerSecond = 8;
    [HideInInspector]
    public Condition condition;

    private GameObject[] collisions;
    private Vector3 boxSize;
    private GameObject tmpRay;
    private SpellIndicatorController indicatorController;

    public override string type => "Ray";
    public override string skillName => "Ray";
    public override bool channel => true;
    public override float cooldown { get => 10f; }
    public override float duration { get => 0f; }
    public override float instaCastDelay => 0f;
    public override bool instaCast => false;
    public override float manaCost => 2f;


    public new void Awake()
    {
        base.Awake();
        boxSize = (new Vector3(3f, 5f, 18f)) / 2f;
        InvokeRepeating(nameof(Damage), 0f, 1f / damageTicksPerSecond);
        SpawnAudio();
    }

    public void SpawnAudio()
    {
        AudioSource audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource1, ResourceManager.Audio.AudioSources.Range.Short);
        audioSource1.loop = true;

        AudioSource audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource2, ResourceManager.Audio.AudioSources.Range.Short);
        audioSource2.loop = true;

        switch (elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                audioSource1.clip = ResourceManager.Audio.Spells.Earth.Ray;
                audioSource2.clip = ResourceManager.Audio.SpellSources.Earth;
                break;
            case ElementTypes.Type.Cold_Ice:
                audioSource1.clip = ResourceManager.Audio.Spells.Ice.Ray;
                audioSource2.clip = ResourceManager.Audio.SpellSources.Ice;
                break;
            case ElementTypes.Type.Lightning:
                audioSource1.clip = ResourceManager.Audio.Spells.Lightning.Ray;
                audioSource2.clip = ResourceManager.Audio.SpellSources.Lightning;
                break;
            default:
                audioSource1.clip = ResourceManager.Audio.Spells.Fire.Ray;
                audioSource2.clip = ResourceManager.Audio.SpellSources.Fire;
                break;
        }

        audioSource1.Play();
        audioSource2.Play();
    }

    private new void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + Vector3.down + transform.forward * 9f, boxSize, transform.rotation, BasicLayerMasks.DamageableEntities);
        collisions = OverlapDetection.NoObstaclesLine(colliders, transform.position, BasicLayerMasks.IgnoreOnDamageRaycasts);
    }

    public new void StartCooldown()
    {
        // If it was already firing a ray
        if (isChanneling)
        {
            base.StartCooldown();
        }
    }

    public override void CastSpell(Transform firePoint, bool holding)
    {
        if (holding && !cancelled)
        {
            if (tmpRay == null)
            {
                tmpRay = Instantiate(gameObject, firePoint);
                indicatorController = tmpRay.AddComponent<SpellIndicatorController>();
                indicatorController.SelectLocation(firePoint, 3f, 18f, SpellIndicatorController.SquareIndicator);
                tmpRay.SetActive(true);
                isChanneling = true;
            }
        }
        else
        {
            if (cancelled)
                cancelled = false;

            if (indicatorController != null)
                indicatorController.DestroyIndicator();
            Destroy(tmpRay.gameObject);
            isChanneling = false;
        }
    }

    public override void CancelCast()
    {
        cancelled = true;
    }

    private void Damage()
    {
        if (collisions == null) return;

        foreach (GameObject gm in collisions)
        {
            if (gm != null)
            {
                HealthEventSystem.current.TakeDamage(gm.name, damage, elementType);
                if (condition != null)
                    if (Random.value <= 0.25f / damageTicksPerSecond) HealthEventSystem.current.SetCondition(gm.name, condition);
            }
        }

        ManaEventSystem.current.UseMana(manaCost);
    }

    public override Sprite GetIcon()
    {
        switch (elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                return ResourceManager.UI.SkillIcons.Ray.Earth;
            case ElementTypes.Type.Cold_Ice:
                return ResourceManager.UI.SkillIcons.Ray.Ice;
            case ElementTypes.Type.Lightning:
                return ResourceManager.UI.SkillIcons.Ray.Lightning;
            default:
                return ResourceManager.UI.SkillIcons.Ray.Fire;
        }
    }

    public override string GetDamageText()
    {
        return damage + " " + ElementTypes.Name(elementType) + " damage " + damageTicksPerSecond + " times per second";
    }

    public override string GetDescription()
    {
        return "Fires a ray dealing " + ElementTypes.Name(elementType) + " damage and applying " + ElementTypes.Condition(elementType) + " condition";
    }

    //------------------ Irrelevant ------------------
    public override ParticleSystem GetSource()
    {
        throw new System.NotImplementedException();
    }
    public override void WakeUp()
    {
    }
}
