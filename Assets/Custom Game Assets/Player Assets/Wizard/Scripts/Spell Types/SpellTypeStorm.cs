using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTypeStorm : Spell
{
    public ElementTypes.Type elementType;

    public float damage = 5f;
    public int damageTicksPerSecond = 5;

    [HideInInspector]
    public Condition condition;

    private GameObject tmpStorm;
    protected SpellIndicatorController indicatorController;
    protected IndicatorResponse indicatorResponse;
    protected GameObject tmpIndicatorHolder;

    [HideInInspector]
    public GameObject[] collisions;

    public override string type => "Storm";
    public override string skillName => "Storm";
    public override bool channel => true;
    public override float cooldown { get => 30f; }
    public override float duration { get => 10f; }
    public override float instaCastDelay => 0f;
    public override bool instaCast => false;
    public override float manaCost => 50f;

    public new void Awake()
    {
        base.Awake();
        cancelled = false;
        InvokeRepeating(nameof(Damage), 1f, 1f / damageTicksPerSecond);
        SpawnAudio();
    }

    public void SpawnAudio()
    {
        AudioSource audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource1, ResourceManager.Audio.AudioSources.Range.Mid);
        audioSource1.loop = true;

        switch (elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                audioSource1.clip = ResourceManager.Audio.Spells.Earth.Storm;
                break;
            case ElementTypes.Type.Cold_Ice:
                audioSource1.clip = ResourceManager.Audio.Spells.Ice.Storm;
                break;
            case ElementTypes.Type.Lightning:
                audioSource1.clip = ResourceManager.Audio.Spells.Lightning.Storm;
                break;
            default:
                audioSource1.clip = ResourceManager.Audio.Spells.Fire.Storm;
                break;
        }

        audioSource1.Play();
    }

    private new void FixedUpdate()
    {
        Vector3 capsuleTop = transform.position + Vector3.up * 8f;
        Collider[] colliders = Physics.OverlapCapsule(capsuleTop, capsuleTop + Vector3.down * 60f, 14f, BasicLayerMasks.DamageableEntities);
        collisions = OverlapDetection.NoObstaclesVertical(colliders, capsuleTop, BasicLayerMasks.IgnoreOnDamageRaycasts);
    }

    public override void CastSpell(Transform firePoint, bool holding)
    {
        if (tmpStorm == null)
        {
            if (holding)
            {
                tmpIndicatorHolder = new GameObject();
                indicatorController = tmpIndicatorHolder.AddComponent<SpellIndicatorController>();
                indicatorController.SelectLocation(20f, 15f);
            }
            else
            {
                if (indicatorController != null)
                {
                    indicatorResponse = indicatorController.LockLocation();
                    if (!indicatorResponse.isNull && !cancelled)
                    {
                        ManaEventSystem.current.UseMana(manaCost);

                        tmpStorm = Instantiate(gameObject);
                        tmpStorm.transform.position = indicatorResponse.centerOfAoe + Vector3.up * 40f;
                        tmpStorm.SetActive(true);
                        tmpStorm.GetComponent<Spell>().TransferData(this);
                        Invoke(nameof(StopStorm), duration);
                    }
                    else
                    {
                        if (cancelled)
                            cancelled = false;

                        Clear();
                    }
                }
            }
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
            if (gm != null && gm.name != casterName)
            {
                HealthEventSystem.current.TakeDamage(gm.name, damage, elementType);
                if (condition != null)
                    if (Random.value <= 0.2f / damageTicksPerSecond) HealthEventSystem.current.SetCondition(gm.name, condition);
            }
        }
    }

    private void StopStorm()
    {
        Clear();
        Destroy(tmpStorm);
    }

    private void CancelSpell()
    {
        if (tmpStorm == null)
        {
            Clear();
        }
    }

    protected void Clear()
    {
        if (indicatorController != null)
            indicatorController.DestroyIndicator();
        Destroy(tmpIndicatorHolder.gameObject);
    }

    public override Sprite GetIcon()
    {
        switch (elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                return ResourceManager.UI.SkillIcons.Storm.Earth;
            case ElementTypes.Type.Cold_Ice:
                return ResourceManager.UI.SkillIcons.Storm.Ice;
            case ElementTypes.Type.Lightning:
                return ResourceManager.UI.SkillIcons.Storm.Lightning;
            default:
                return ResourceManager.UI.SkillIcons.Storm.Fire;
        }
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
