using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBurst : SwordEffect
{
    public float damage = 20f;
    public float sphereRadius = 15f;
    public float force = 10f;

    [HideInInspector]
    public Condition condition = null;

    private SpellIndicatorController indicatorController;
    private ParticleSystem particles;

    public override string type => "Sphere Burst";
    public override string skillName => "Sphere Burst";
    public override float cooldown => 10f;
    public override int comboPhaseMax => 1;
    public override bool instaCast => true;
    public override float manaCost => 30f;

    private new void Awake()
    {
        base.Awake();
        // Unparent Wave particles
        particles = GetComponentInChildren<ParticleSystem>();
        particles.transform.parent = null;
        particles.transform.localScale = Vector3.one;

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
        indicatorController.SelectLocation(controls.transform, sphereRadius * 2f, sphereRadius * 2f, SpellIndicatorController.CircleIndicator);
        indicatorController.DestroyIndicator(0.5f);

        if (instaCasting)
            yield return null;
        else
            yield return new WaitForSeconds(attackDelay);

        PlaySwordSwingAudio();

        // Spawns copy of particle system
        ParticleSystem parts = Instantiate(particles, controls.transform.position + controls.transform.forward, controls.transform.rotation);
        parts.Play();
        Destroy(parts.gameObject, 4f);
        CameraShake.current.ShakeCamera(0.3f, 1f);

        // Find targets
        GameObject[] targets = FindTargets(controls.transform);

        foreach (GameObject visibleTarget in targets)
        {
            if (visibleTarget.name != controls.name)
            {
                HealthEventSystem.current.TakeDamage(visibleTarget.gameObject.GetInstanceID(), damage, attributes.elementType);
                if (condition != null)
                    if (Random.value <= 0.5f) HealthEventSystem.current.SetCondition(visibleTarget.GetInstanceID(), condition);
                HealthEventSystem.current.ApplyForce(visibleTarget.GetInstanceID(), visibleTarget.transform.position - controls.transform.position, force);
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    private GameObject[] FindTargets(Transform sphereCenter)
    {
        Collider[] sphereCollisions = Physics.OverlapSphere(sphereCenter.position, sphereRadius, BasicLayerMasks.DamageableEntities);
        GameObject[] notBlocked = OverlapDetection.NoObstaclesLine(sphereCollisions, sphereCenter.position, BasicLayerMasks.IgnoreOnDamageRaycasts);

        return notBlocked;
    }

    public override Sprite GetIcon()
    {
        switch (attributes.elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                return ResourceManager.UI.SkillIcons.Explosion.Earth;
            case ElementTypes.Type.Cold_Ice:
                return ResourceManager.UI.SkillIcons.Explosion.Ice;
            case ElementTypes.Type.Lightning:
                return ResourceManager.UI.SkillIcons.Explosion.Lightning;
            default:
                return ResourceManager.UI.SkillIcons.Explosion.Fire;
        }
    }

    public override string GetDamageText()
    {
        return damage + " " + ElementTypes.Name(attributes.elementType) + " damage";
    }

    public override string GetDescription()
    {
        return "Fires projectiles on sphere dealing " + ElementTypes.Name(attributes.elementType) + " damage and applying " + ElementTypes.Condition(attributes.elementType) + " condition";
    }
}
