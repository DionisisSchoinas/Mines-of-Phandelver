using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum Size
    {
        Small,
        Big
    }

    public Size explosionSize;
    public SpellSourceAudio.Type elementType;

    public float damage = 35f;
    public float radius = 9f;
    [HideInInspector]
    public int damageType;
    [HideInInspector]
    public Condition condition;

    private string casterName;

    private void Awake()
    {
        SpawnAudio();
    }

    public void SpawnAudio()
    {
        AudioSource audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource1, ResourceManager.Audio.AudioSources.Range.Mid);

        switch (explosionSize)
        {
            case Size.Big:
                switch (elementType)
                {
                    case SpellSourceAudio.Type.Earth:
                        audioSource1.clip = ResourceManager.Audio.Spells.Earth.BigExplosion;
                        break;
                    case SpellSourceAudio.Type.Ice:
                        audioSource1.clip = ResourceManager.Audio.Spells.Ice.BigExplosion;
                        break;
                    case SpellSourceAudio.Type.Lightning:
                        audioSource1.clip = ResourceManager.Audio.Spells.Lightning.BigExplosion;
                        break;
                    default:
                        audioSource1.clip = ResourceManager.Audio.Spells.Fire.BigExplosion;
                        break;
                }
                break;
            default:
                switch (elementType)
                {
                    case SpellSourceAudio.Type.Earth:
                        audioSource1.clip = ResourceManager.Audio.Spells.Earth.SmallExplosion;
                        break;
                    case SpellSourceAudio.Type.Ice:
                        audioSource1.clip = ResourceManager.Audio.Spells.Ice.SmallExplosion;
                        break;
                    case SpellSourceAudio.Type.Lightning:
                        audioSource1.clip = ResourceManager.Audio.Spells.Lightning.SmallExplosion;
                        break;
                    case SpellSourceAudio.Type.Energy:
                        //audioSource1.clip = ResourceManager.Audio.Spells.Energy.BigExplosion;
                        break;
                    default:
                        audioSource1.clip = ResourceManager.Audio.Spells.Fire.SmallExplosion;
                        break;
                }
                break;
        }

        audioSource1.Play();
    }

    protected void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, BasicLayerMasks.DamageableEntities);
        GameObject[] hitObjects = OverlapDetection.NoObstaclesLine(colliders, transform.position, BasicLayerMasks.IgnoreOnDamageRaycasts);
        foreach (GameObject gm in hitObjects)
        {
            Damage(gm);
        }
    }

    public void SetName(string casterName)
    {
        this.casterName = casterName;
    }

    private void Damage(GameObject gm)
    {
        if (gm == null || gm.name == casterName)  return;

        HealthEventSystem.current.TakeDamage(gm.name, damage, damageType);
        if (condition != null && Random.value <= 0.5f) HealthEventSystem.current.SetCondition(gm.name, condition);
    }
}
