using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlash : SwordEffect
{
    public float damage = 30f;
    public float force = 5f;

    [HideInInspector]
    public Condition condition = null;

    public override string type => "Simple Slash";
    public override string skillName => "Simple Slash";
    // public override float cooldown => MeleeController.skillComboCooldown * 0.95f;

    private AudioSource hitAudioSource;

    protected new void OnDestroy()
    {
        base.OnDestroy();
        Destroy(hitAudioSource);
    }

    public override void Attack(PlayerMovementScriptWarrior controls, AttackIndicator indicator, List<SkinnedMeshRenderer> playerMesh)
    {
        if (hitAudioSource == null)
        {
            hitAudioSource = controls.gameObject.AddComponent<AudioSource>();
            hitAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", hitAudioSource, ResourceManager.Audio.AudioSources.Range.Short);
        }

        StartCoroutine(PerformAttack(comboTrailTimings[comboPhase].delayToFireSpell, controls, indicator));
    }

    IEnumerator PerformAttack(float attackDelay, PlayerMovementScriptWarrior controls, AttackIndicator indicator)
    {
        yield return new WaitForSeconds(attackDelay);
        controls.sliding = true;

        PlaySwordSwingAudio();

        int col = 0;

        foreach (Transform visibleTarget in indicator.visibleTargets)
        {
            if (visibleTarget.name != controls.name)
            {
                HealthEventSystem.current.TakeDamage(visibleTarget.gameObject.GetInstanceID(), damage, attributes.elementType);
                if (condition != null)
                    if (Random.value <= 0.2f) HealthEventSystem.current.SetCondition(visibleTarget.gameObject.GetInstanceID(), condition);
                HealthEventSystem.current.ApplyForce(visibleTarget.gameObject.GetInstanceID(), controls.transform.forward, force);
                CameraShake.current.ShakeCamera(0.1f, 0.15f);

                if ((visibleTarget.gameObject.layer.Equals(BasicLayerMasks.EnemiesLayer) || visibleTarget.gameObject.layer.Equals(BasicLayerMasks.DamageablesLayer)))
                    col = 1;
                else if (col == 0)
                    col = 2;
            }
        }

        switch (col)
        {
            case 1: // Hit damagable object
                hitAudioSource.clip = ResourceManager.Audio.Sword.HitFlesh;
                hitAudioSource.Play();
                break;
            case 2: // Hit terrain
                hitAudioSource.clip = ResourceManager.Audio.Sword.HitObject;
                hitAudioSource.Play();
                break;
        }

        yield return new WaitForSeconds(0.1f);
        controls.sliding = false;
    }

    public override GameObject GetSource()
    {
        return null;
    }

    public override string GetDamageText()
    {
        return damage + " " + ElementTypes.Name(attributes.elementType) + " damage";
    }

    public override string GetDescription()
    {
        return "Slashes dealing " + ElementTypes.Name(attributes.elementType) + " damage and applying " + ElementTypes.Condition(attributes.elementType) + " condition";
    }
}
