using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SwordEffectAttributes
{
    public ElementTypes.Type elementType;
    public float swingSoundDelay;
    public SwingTrailRenderer[] trails;
    public Material swordMaterial;
}

public class SwordEffect : BasicSword
{
    public SwordEffectAttributes attributes;

    private List<SwingTrailRenderer> trails;
    private SwordEffect currentEffect;
    private Transform tipPoint, basePoint;
    private GameObject swordParticles;
    private Coroutine swingSoundCoroutine;

    protected bool instaCasting;
    private AudioSource swingAudioSource;
    protected AudioClip[] swingAudioClips;

    public override string type => "Sword Effect";
    public override string skillName => "Sword Effect";
    public override float cooldown => 0.7f;
    public override float duration => 0f;
    public override int comboPhaseMax => 3;
    public override float instaCastDelay => 0.4f;
    public override bool instaCast => false;
    public override float manaCost => 0f;

    public new void Awake()
    {
        base.Awake();
        trails = new List<SwingTrailRenderer>();
        instaCasting = false;

        swingAudioSource = gameObject.AddComponent<AudioSource>();
        swingAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", swingAudioSource, ResourceManager.Audio.AudioSources.Range.Short);

        swingAudioClips = ResourceManager.Audio.Sword.Swings;
    }

    protected void OnDestroy()
    {
        if (swordParticles != null)
            Destroy(swordParticles);
    }

    public SwordEffect InstantiateEffect(Transform tPoint, Transform bPoint, Transform parent)
    {
        currentEffect = Instantiate(gameObject, parent).GetComponent<SwordEffect>();
        currentEffect.SetPoints(tPoint, bPoint);
        return currentEffect;
    }

    public void SetPoints(Transform tPoint, Transform bPoint)
    {
        tipPoint = tPoint;
        basePoint = bPoint;
        foreach (SwingTrailRenderer t in attributes.trails)
        {
            trails.Add(Instantiate(t, transform));
            trails[trails.Count - 1].SetPoints(tipPoint, basePoint);
        }
    }

    public void StartSwingTrail()
    {
        foreach (SwingTrailRenderer t in trails)
        {
            t.StartLine();
        }
    }

    public void StopSwingTrail()
    {
        foreach (SwingTrailRenderer t in trails)
        {
            t.StopLine();
        }
    }

    // This function instanitates a copy of itself and calls the coroutine FROM THE COPY
    public SwordEffect InstaCast(PlayerMovementScriptWarrior controls, GameObject swordObject, List<SkinnedMeshRenderer> playerMesh, Renderer swordRenderer, Transform swordTopPoint, Transform swordBasePoint, Transform swordParent)
    {
        SwordEffect current = InstantiateEffect(swordTopPoint, swordBasePoint, swordParent);
        current.StartCast(controls, swordObject, playerMesh, swordRenderer);

        return current;
    }

    // Called in the copy of the object
    public void StartCast(PlayerMovementScriptWarrior controls, GameObject swordObject, List<SkinnedMeshRenderer> playerMesh, Renderer swordRenderer)
    {
        if (isActiveAndEnabled)
            StartCoroutine(DelayInstaCast(controls, swordObject, playerMesh, swordRenderer));
    }

    private IEnumerator DelayInstaCast(PlayerMovementScriptWarrior controls, GameObject swordObject, List<SkinnedMeshRenderer> playerMesh, Renderer swordRenderer)
    {
        instaCasting = true;

        yield return new WaitForSeconds(instaCastDelay);

        if (swordParticles != null)
            Destroy(swordParticles);

        if (GetSource() != null)
        {
            swordParticles = Instantiate(GetSource().gameObject, swordObject.transform);
        }

        swordRenderer.material = attributes.swordMaterial;

        if (instaCast)
        {
            Attack(controls, null, playerMesh);
            
            StartCooldown();
            ManaEventSystem.current.UseMana(manaCost);
            UIEventSystem.current.FreezeAllSkills(uniqueOverlayToWeaponAdapterId, OverlayControls.skillFreezeAfterCasting);
        }

        instaCasting = false;
    }

    public override void Attack(PlayerMovementScriptWarrior controls, AttackIndicator indicator, List<SkinnedMeshRenderer> playerMesh)
    {
    }

    public override GameObject GetSource()
    {
        return null;
    }

    protected void PlaySwordSwingAudio()
    {
        swingAudioSource.Stop();
        int randomSwing = UnityEngine.Random.Range(0, swingAudioClips.Length);
        swingAudioSource.clip = swingAudioClips[randomSwing];

        if (swingSoundCoroutine != null)
            StopCoroutine(swingSoundCoroutine);
        swingSoundCoroutine = StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(attributes.swingSoundDelay);
        swingAudioSource.Play();
    }

    public override Sprite GetIcon()
    {
        return ResourceManager.UI.SkillIcons.Default.Swing;
    }

    public override string GetDamageText()
    {
        return "Not sure yet";
    }

    public override Color GetTextColor()
    {
        return ElementTypes.Colors(attributes.elementType);
    }

    public override string GetDescription()
    {
        return "Sword effect description";
    }
}
