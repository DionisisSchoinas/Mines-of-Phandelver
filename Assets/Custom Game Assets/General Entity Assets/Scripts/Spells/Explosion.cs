using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum Size
    {
        Small,
        Big
    }

    public Size explosionSize;
    public ElementTypes.Type elementType;

    public float damage = 35f;
    public float radius = 9f;
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

        switch (explosionSize)
        {
            case Size.Big:
                audioSource1 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource1, ResourceManager.Audio.AudioSources.Range.Mid);
                switch (elementType)
                {
                    case ElementTypes.Type.Physical_Earth:
                        audioSource1.clip = ResourceManager.Audio.Spells.Earth.BigExplosion;
                        break;
                    case ElementTypes.Type.Cold_Ice:
                        audioSource1.clip = ResourceManager.Audio.Spells.Ice.BigExplosion;
                        break;
                    case ElementTypes.Type.Lightning:
                        audioSource1.clip = ResourceManager.Audio.Spells.Lightning.BigExplosion;
                        break;
                    default:
                        audioSource1.clip = ResourceManager.Audio.Spells.Fire.BigExplosion;
                        break;
                }
                break;
            default:
                audioSource1 = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", audioSource1, ResourceManager.Audio.AudioSources.Range.Short);
                switch (elementType)
                {
                    case ElementTypes.Type.Physical_Earth:
                        audioSource1.clip = ResourceManager.Audio.Spells.Earth.SmallExplosion;
                        break;
                    case ElementTypes.Type.Cold_Ice:
                        audioSource1.clip = ResourceManager.Audio.Spells.Ice.SmallExplosion;
                        break;
                    case ElementTypes.Type.Lightning:
                        audioSource1.clip = ResourceManager.Audio.Spells.Lightning.SmallExplosion;
                        break;
                    case ElementTypes.Type.Energy:
                        AudioClip[] clips = ResourceManager.Audio.Spells.Energy.Impacts;
                        audioSource1.clip = clips[Random.Range(0, clips.Length)];
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
        //GameObject[] hitObjects = OverlapDetection.NoObstaclesLine(colliders, transform.position, BasicLayerMasks.IgnoreOnDamageRaycasts);
        foreach (Collider col in colliders)
        {
            Damage(col);
        }
    }

    public void SetName(string casterName)
    {
        this.casterName = casterName;
    }

    private void Damage(Collider col)
    {
        if (col == null || col.gameObject.name == casterName)  
            return;

        HealthEventSystem.current.TakeDamage(col.gameObject.GetInstanceID(), damage, elementType);
        if (condition != null && Random.value <= 0.5f) HealthEventSystem.current.SetCondition(col.gameObject.GetInstanceID(), condition);
    }
}
