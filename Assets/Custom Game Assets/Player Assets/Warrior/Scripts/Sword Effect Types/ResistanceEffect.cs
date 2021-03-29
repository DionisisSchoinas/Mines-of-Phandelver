using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceEffect : SwordEffect
{
    [HideInInspector]
    public Material resistanceAppearance;

    public override string type => "Resistance";
    public override string skillName => "No Resistance";
    public override float cooldown => 20f;
    public override float duration => 10f;
    public override int comboPhaseMax => 1;
    public override bool instaCast => true;
    public override float manaCost => 20f;

    private new void Awake()
    {
        base.Awake();

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

    public override void Attack(PlayerMovementScriptWarrior controls, AttackIndicator indicator, List<SkinnedMeshRenderer> playerMesh)
    {
        StartCoroutine(PerformAttack(comboTrailTimings[comboPhase].delayToFireSpell, controls, playerMesh));
    }

    IEnumerator PerformAttack(float attackDelay, PlayerMovementScriptWarrior controls, List<SkinnedMeshRenderer> playerMesh)
    {
        if (instaCasting)
            yield return null;
        else
            yield return new WaitForSeconds(attackDelay);

        PlaySwordSwingAudio();

        HealthEventSystem.current.ApplyResistance(controls.gameObject.name, playerMesh, resistanceAppearance, attributes.elementType, duration);
    }

    public override Sprite GetIcon()
    {
        switch (attributes.elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                return ResourceManager.UI.SkillIcons.Resistance.Earth;
            case ElementTypes.Type.Cold_Ice:
                return ResourceManager.UI.SkillIcons.Resistance.Ice;
            case ElementTypes.Type.Lightning:
                return ResourceManager.UI.SkillIcons.Resistance.Lightning;
            default:
                return ResourceManager.UI.SkillIcons.Resistance.Fire;
        }
    }

    public override string GetDamageText()
    {
        return "-50% " + ElementTypes.Name(attributes.elementType) + " damage";
    }

    public override string GetDescription()
    {
        return "Gives resistance to " + ElementTypes.Name(attributes.elementType) + " damage for " + duration + " seconds, which reduces the incoming damage by 50% ";
    }
}
