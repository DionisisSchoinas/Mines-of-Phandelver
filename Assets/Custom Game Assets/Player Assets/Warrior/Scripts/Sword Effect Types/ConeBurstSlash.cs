using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBurstSlash : SwordEffect
{
    public float damage = 50f;
    public float coneWidth = 35f;
    public float coneLength = 25f;
    public float force = 5f;

    [HideInInspector]
    public Condition condition = null;

    private SpellIndicatorController indicatorController;
    private ParticleSystem particles;
    private float attackAngle;

    public override string type => "Cone Burst";
    public override string skillName => "Cone Burst";
    public override float cooldown => 20f;
    public override float manaCost => 20f;

    private new void Awake()
    {
        base.Awake();
        // Unparent Wave particles
        particles = GetComponentInChildren<ParticleSystem>();
        particles.transform.parent = null;
        particles.transform.localScale = Vector3.one;

        // Calculate Cone angle
        Vector3 edge1 = Vector3.forward * coneLength + Vector3.right * coneWidth / 2f;
        Vector3 edge2 = Vector3.forward * coneLength - Vector3.right * coneWidth / 2f;
        attackAngle = Vector3.Angle(edge1, edge2);

        SpawnAudio();
    }

    public void SpawnAudio()
    {
        switch (attributes.elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                swingAudioClips = ResourceManager.Audio.Spells.Earth.Swings;
                break;
            case ElementTypes.Type.Cold_Ice:
                swingAudioClips = ResourceManager.Audio.Spells.Ice.Swings;
                break;
            case ElementTypes.Type.Lightning:
                swingAudioClips = ResourceManager.Audio.Spells.Lightning.Swings;
                break;
            default:
                swingAudioClips = ResourceManager.Audio.Spells.Fire.Swings;
                break;
        }
    }

    private new void OnDestroy()
    {
        base.OnDestroy();

        if (particles != null)
            Destroy(particles.gameObject);
    }

    public override void Attack(PlayerMovementScriptWarrior controls, AttackIndicator indicator, List<SkinnedMeshRenderer> playerMesh)
    {
        StartCoroutine(PerformAttack(comboTrailTimings[comboPhase].delayToFireSpell, controls));
    }

    IEnumerator PerformAttack(float attackDelay, PlayerMovementScriptWarrior controls)
    {
        // Spawns Indicator
        indicatorController = gameObject.AddComponent<SpellIndicatorController>();
        indicatorController.SelectLocation(controls.transform, coneWidth, coneLength, SpellIndicatorController.ConeIndicator);
        indicatorController.DestroyIndicator(0.5f);

        yield return new WaitForSeconds(attackDelay);
        controls.sliding = true;

        // Play audio
        PlaySwordSwingAudio();

        // Spawns copy of particle system
        ParticleSystem parts = Instantiate(particles, controls.transform.position + controls.transform.forward * 2f, controls.transform.rotation);
        parts.Play();
        Destroy(parts.gameObject, 4f);

        // Find targets
        Collider[] targets = FindTargets(controls.transform);

        foreach (Collider visibleTarget in targets)
        {
            if (visibleTarget.gameObject.name != controls.name)
            {
                HealthEventSystem.current.TakeDamage(visibleTarget.gameObject.GetInstanceID(), damage, attributes.elementType);
                if (condition != null)
                    if (Random.value <= 0.5f) HealthEventSystem.current.SetCondition(visibleTarget.gameObject.GetInstanceID(), condition);
                HealthEventSystem.current.ApplyForce(visibleTarget.gameObject.GetInstanceID(), controls.transform.forward, force);
                CameraShake.current.ShakeCamera(0.2f, 0.5f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        controls.sliding = false;
    }

    private Collider[] FindTargets(Transform startingConePosition)
    {
        Vector3 boxCenter = startingConePosition.position + startingConePosition.forward * coneLength / 2f;
        Vector3 boxSize = new Vector3(coneLength, 5f, coneWidth);

        Collider[] boxCollisions = Physics.OverlapBox(boxCenter, boxSize / 2f, startingConePosition.rotation, BasicLayerMasks.DamageableEntities);
        //GameObject[] notBlocked = OverlapDetection.NoObstaclesLine(boxCollisions, startingConePosition.position, BasicLayerMasks.IgnoreOnDamageRaycasts);
        List<Collider> targets = new List<Collider>();
        foreach (Collider target in boxCollisions)
        {
            Vector3 dirToTarget = (target.transform.position - startingConePosition.position);
            if (Vector3.Angle(startingConePosition.forward, dirToTarget) < attackAngle / 2)
            {
                targets.Add(target);
            }
        }
        return targets.ToArray();
    }

    public override Sprite GetIcon()
    {
        switch (attributes.elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                return ResourceManager.UI.SkillIcons.Cone.Earth;
            case ElementTypes.Type.Cold_Ice:
                return ResourceManager.UI.SkillIcons.Cone.Ice;
            case ElementTypes.Type.Lightning:
                return ResourceManager.UI.SkillIcons.Cone.Lightning;
            default:
                return ResourceManager.UI.SkillIcons.Cone.Fire;
        }
    }
    public override string GetDamageText()
    {
        return damage + " " + ElementTypes.Name(attributes.elementType) + " damage";
    }

    public override string GetDescription()
    {
        return "Fires projectiles on a cone dealing " + ElementTypes.Name(attributes.elementType) + " damage and applying " + ElementTypes.Condition(attributes.elementType) + " condition";
    }
}
