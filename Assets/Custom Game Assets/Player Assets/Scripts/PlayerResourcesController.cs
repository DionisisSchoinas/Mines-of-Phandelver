using UnityEngine;

public class PlayerResourcesController : MonoBehaviour
{
    public ResourceBar healthBar;
    public Color healthBarColor;
    public float maxHealth;
    public float healthRegenPerSecond;
    public bool respawn;
    public bool invulnerable;
    [Range(0f,1f)]
    public float staggerPercentage;

    public ResourceBar manaBar;
    public Color manaBarColor;
    public float maxMana;
    public float manaRegenPerSecond;

    private HealthController healthController;
    private ManaController manaController;

    private void Awake()
    {
        if (healthBar != null)
        {
            healthController = gameObject.AddComponent<HealthController>();
            healthController.SetValues(maxHealth, healthRegenPerSecond, healthBar, healthBarColor, respawn, invulnerable, staggerPercentage);
        }

        if (manaBar != null)
        {
            manaController = gameObject.AddComponent<ManaController>();
            manaController.SetValues(maxMana, manaRegenPerSecond, manaBar, manaBarColor);
        }
    }

    private void Start()
    {
        HealthEventSystem.current.onDeath += PlayerDeath;
    }

    private void OnDestroy()
    {
        HealthEventSystem.current.onDeath -= PlayerDeath;
    }

    private void PlayerDeath(int id)
    {
        if (gameObject.GetInstanceID() == id)
            UIEventSystem.current.PlayerDied();
    }
}
