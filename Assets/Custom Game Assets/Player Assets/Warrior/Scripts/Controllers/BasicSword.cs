using System.Collections.Generic;
using UnityEngine;

public abstract class BasicSword : Skill
{
    public abstract int comboPhaseMax { get; }

    private int _comboPhase;
    public int comboPhase {
        get
        {
            return _comboPhase;
        }
        set
        {
            if (value >= comboTrailTimings.Length)
                _comboPhase = comboTrailTimings.Length - 1;
            else
                _comboPhase = value;
        }
    }

    public float[] swingCooldowns => new float[]
    {
        0f,
        0f,
        0f
    };

    public ComboStage[] comboTrailTimings => new ComboStage[]
    {
        new ComboStage(0, 0.2f, 0.1f, 0.25f),
        new ComboStage(1, 0.05f, 0.2f, 0.12f),
        new ComboStage(2, 0.1f, 0.2f, 0.15f)
    };

    public abstract void Attack(PlayerMovementScriptWarrior controls, AttackIndicator indicator, SkinnedMeshRenderer playerMesh);
    public abstract ParticleSystem GetSource();

    public void StartSwingCooldown()
    {
        //UIEventSystem.current.SkillCast(uniqueOverlayToWeaponAdapterId, 0f);
    }
}
