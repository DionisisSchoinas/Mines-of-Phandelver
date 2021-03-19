using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private PlayerMovementScriptWarrior controls;
    private CharacterController characterController;
    private AnimationScriptControllerWarrior animations;
    private Sword sword;

    public AttackIndicator indicator;
    public float attackDelay;
    [HideInInspector]
    public bool attacking = false;//check if already attacking
    public bool canAttack = true;//check if the cooldown has passed
    public float meleeCooldown = 0.2f;
    bool canHit;
    //combo counters 
    public float reset;
    public float resetTime;
   
    //combo spacers 
    [HideInInspector]
    public float comboCurrent;
    public float comboTime = 0.7f;
    //Combo queue 
    [HideInInspector]
    public List<int> comboQueue = new List<int>();
    //combo spam regulation
    bool comboLock;
    public float comboCooldown;
    //direction lock
    public bool isDuringAttack;

    public static float skillComboCooldown;
    private bool skillListUp;
    private float currentMana;

    private bool isOnCooldown;
    private bool hasEnoughMana;

    private float lastCooldownDisplayMessage;
    private float lastManaDisplayMessage;
    /*
    public float swingSoundDelay = 0.5f;
    private List<AudioClip> swingSounds;
    private Coroutine swingSoundCoroutine;
    private AudioSource swingAudioSource;
    private AudioSource hitAudioSource;
    */
    void Start()
    {
        canHit = true;

        indicator = GetComponent<AttackIndicator>();
        controls = GetComponent<PlayerMovementScriptWarrior>();
        animations = GetComponent<AnimationScriptControllerWarrior>();
        sword = GetComponent<Sword>();

        isDuringAttack = false;

        lastCooldownDisplayMessage = Time.time;
        lastManaDisplayMessage = Time.time;

        skillComboCooldown = comboCooldown;

        skillListUp = false;

        isOnCooldown = false;
        hasEnoughMana = true;
        /*
        swingAudioSource = gameObject.AddComponent<AudioSource>();
        swingAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", swingAudioSource, ResourceManager.Audio.AudioSources.Range.Short);
        hitAudioSource = gameObject.AddComponent<AudioSource>();
        hitAudioSource = ResourceManager.Audio.AudioSources.LoadAudioSource("Sound Effects", hitAudioSource, ResourceManager.Audio.AudioSources.Range.Short);

        swingSounds = new List<AudioClip>();
        swingSounds.Add(ResourceManager.Audio.Sword.Swing1);
        swingSounds.Add(ResourceManager.Audio.Sword.Swing2);
        */
        UIEventSystem.current.onSkillListUp += SkillListUp;
        ManaEventSystem.current.onManaUpdated += ManaUpdate;

        // Requests update for mana values
        ManaEventSystem.current.UseMana(0);
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onSkillListUp -= SkillListUp;
        ManaEventSystem.current.onManaUpdated -= ManaUpdate;
    }

    private void ManaUpdate(float mana)
    {
        currentMana = mana;
    }
    private void SkillListUp(bool up)
    {
        skillListUp = up;
    }

    void FixedUpdate()
    {
        // Cooldown display message
        if (sword.GetSelectedEffect().onCooldown && controls.mousePressed_1)
        {
            if (Time.time - lastCooldownDisplayMessage >= 1f)
            {
                Debug.Log("On Cooldown");
                lastCooldownDisplayMessage = Time.time;
            }
            isOnCooldown = true;
        }
        else if (isOnCooldown && !sword.GetSelectedEffect().onCooldown)
        {
            isOnCooldown = false;
        }

        // Mana display message
        if (currentMana < sword.GetSelectedEffect().manaCost && controls.mousePressed_1)
        {
            if (Time.time - lastManaDisplayMessage >= 1f)
            {
                Debug.Log("Not enough mana");
                lastManaDisplayMessage = Time.time;
            }
            hasEnoughMana = false;
        }
        else if (!hasEnoughMana && currentMana >= sword.GetSelectedEffect().manaCost)
        {
            hasEnoughMana = true;
        }

        if (controls.mousePressed_1 && !skillListUp && !isOnCooldown && hasEnoughMana)
        {
            if (canHit && !comboLock)
            {
                if (comboQueue.Count < 3)
                {
                    animations.Attack();
                    comboQueue.Add(0);
                    reset = 0f;

                    lastCooldownDisplayMessage = Time.time;
                    lastManaDisplayMessage = Time.time;
                }
                canHit = false;
            }
        }
        else
        {
            canHit = true;
        }

        if (comboQueue.Count != 0 && !attacking && !isOnCooldown && hasEnoughMana)
        {
            attacking = true;
            isDuringAttack = true;
           
            StartCoroutine(PerformAttack(attackDelay));
            comboCurrent = 0f;
        }

        if (comboQueue.Count == 0 && isDuringAttack)
        {
            sword.GetSelectedEffect().StartCooldown();
            isDuringAttack = false;
            animations.ResetAttack();
        }
        else if (comboQueue.Count == 3)
        {
            StartCoroutine(ComboCooldown(comboCooldown));
        }

        if (attacking)
        {
            comboCurrent += Time.deltaTime;

            if (comboCurrent > comboTime)
            {
                comboQueue.RemoveAt(0);
                attacking = false;
            }
        }
    }

    public void Attack(int comboPhase)
    {
        sword.Attack(indicator, comboPhase);
    }

    public void IsSwinging(bool state)
    {
        sword.isSwinging = state;
    }

    IEnumerator PerformAttack(float attackDelay)
    {
        //PlaySwordSwingAudio();

        animations.Attack();

        yield return new WaitForSeconds(attackDelay);
        /*
           foreach (Transform visibleTarget in indicator.visibleTargets)
           {
               Debug.Log(visibleTarget.name);
               HealthEventSystem.current.TakeDamage(visibleTarget.name, 30, 0);
               HealthEventSystem.current.ApplyForce(visibleTarget.name, transform.forward, 5f);
           }
        */
        yield return new WaitForSeconds(0.1f);

        controls.sliding = false;
    }
 
    IEnumerator ComboCooldown(float comboCooldown)
    {
        comboLock = true;
        yield return new WaitForSeconds(comboCooldown);
        comboLock = false;
    }

}
