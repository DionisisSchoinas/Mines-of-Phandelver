using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonContainer : ElementHover, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    [HideInInspector]
    public ButtonData buttonData;
    [HideInInspector]
    public OverlayControls overlayControls;
    [HideInInspector]
    public Button button;
    [HideInInspector]
    public Transform parent;
    [HideInInspector]
    public float cooldownPercentage;

    protected RectTransform rect;
    protected Transform canvas;
    protected Image buttonSelection;
    protected Image buttonBackground;
    protected Image buttonImageCooldown;
    protected Image buttonOutOfMana;
    protected Vector2 clickPositionOffset;
    protected bool skillListUp;
    protected AudioSource audioSource;

    private bool draggingButton;

    public void Awake()
    {
        button = gameObject.GetComponent<Button>();

        if (overlayControls == null)
            overlayControls = FindObjectOfType<OverlayControls>();

        rect = GetComponent<RectTransform>();
        canvas = FindObjectOfType<OverlayControls>().transform;

        Image[] images = gameObject.GetComponentsInChildren<Image>();
        // Highlight border
        buttonSelection = images[0];
        // Button background
        buttonBackground = images[1];
        // Spinning cooldown background
        buttonImageCooldown = images[2];
        buttonImageCooldown.fillAmount = 0;
        // Gray ouf of mana background
        buttonOutOfMana = images[3];
        buttonOutOfMana.fillAmount = 0;

        skillListUp = false;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ResourceManager.UI.Sounds.ButtonHoverEnter;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Sound Effects")[0];

        UIEventSystem.current.onSkillPickedRegistered += SkillPicked;
        UIEventSystem.current.onSkillCast += SkillCast;
        UIEventSystem.current.onFreezeAllSkills += Freeze;
        UIEventSystem.current.onSkillListUp += BlockQuickbarSkillSelection;
        UIEventSystem.current.onCancelSkill += SkillCast;
        ManaEventSystem.current.onManaUpdated += ManaUpdate;
        UIEventSystem.current.onDraggingButton += Dragging;
    }


    public void OnDestroy()
    {
        UIEventSystem.current.onSkillPickedRegistered -= SkillPicked;
        UIEventSystem.current.onSkillCast -= SkillCast;
        UIEventSystem.current.onFreezeAllSkills -= Freeze;
        UIEventSystem.current.onSkillListUp -= BlockQuickbarSkillSelection;
        UIEventSystem.current.onCancelSkill -= SkillCast;
        ManaEventSystem.current.onManaUpdated -= ManaUpdate;
        UIEventSystem.current.onDraggingButton -= Dragging;
    }

    private void BlockQuickbarSkillSelection(bool block)
    {
        skillListUp = block;
    }

    public void SetSelectionColor(Color color)
    {
        buttonSelection.color = color;
    }

    //------------ Hover functions ------------
    public new void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (!skillListUp || draggingButton)
            return;

        UIEventSystem.current.ShowSkillToolTip(buttonData.skill);
    }

    public new void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (!skillListUp)
            return;

        UIEventSystem.current.HideToolTip();
    }

    //------------ Reset functions ------------
    public void CheckCooldown()
    {
        if (buttonData.skill.cooldownPercentage != 0)
        {
            StartCoroutine(StartCooldownDisplay(buttonData.skill.cooldown));
        }
    }


    //------------ Drag functions ------------
    public void OnDrag(PointerEventData eventData)
    {
        Drag(eventData);
    }

    protected void Drag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector3 oldPos = rect.position;
        rect.position = currentMousePosition - clickPositionOffset;
        if (!IsRectTransformInsideSreen())
        {
            rect.position = oldPos;
        }
    }

    private void Dragging(ButtonContainer btn, bool dragging)
    {
        draggingButton = dragging;
    }

    private bool IsRectTransformInsideSreen()
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);
        Rect screen = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (!screen.Contains(corner))
            {
                return false;
            }
        }
        return true;
    }


    //------------ Event functions ------------
    private void SkillPicked(int skillIndexInAdapter, bool startCooldown)
    {
        if (buttonData.skillIndexInAdapter == skillIndexInAdapter && !startCooldown)
            return;

        if (isActiveAndEnabled)
        {
            if (!buttonData.skill.onCooldown)
                buttonData.skill.StartCooldownWithoutEvent(OverlayControls.skillFreezeAfterPicking);

            StartCoroutine(StartCooldownDisplay(OverlayControls.skillFreezeAfterPicking));
        }
    }

    private void SkillCast(int uniqueAdapterId, float cooldown)
    {
        if (buttonData.skill.uniqueOverlayToWeaponAdapterId == uniqueAdapterId && isActiveAndEnabled)
        {
            if (!buttonData.skill.onCooldown)
                buttonData.skill.StartCooldownWithoutEvent(cooldown);

            StartCoroutine(StartCooldownDisplay(cooldown));
        }
    }

    private void Freeze(int uniqueAdapterId, float delay)
    {
        if (isActiveAndEnabled && buttonData.skill.uniqueOverlayToWeaponAdapterId != uniqueAdapterId)
        {
            if (!buttonData.skill.onCooldown)
                buttonData.skill.StartCooldownWithoutEvent(delay);

            StartCoroutine(StartCooldownDisplay(delay));
        }
    }

    private void ManaUpdate(float mana)
    {
        if (buttonData.skill.manaCost > mana)
        {
            buttonOutOfMana.fillAmount = 1f;
        }
        else if (buttonOutOfMana.fillAmount == 1f)
        {
            buttonOutOfMana.fillAmount = 0f;
        }
    }

    private IEnumerator StartCooldownDisplay(float cooldown)
    {
        float delayForEachStep = cooldown / 100f;

        while (buttonData.skill.cooldownPercentage < 1)
        {
            buttonImageCooldown.fillAmount = buttonData.skill.cooldownPercentage;
            yield return new WaitForSeconds(delayForEachStep /2f);
        }
        buttonImageCooldown.fillAmount = 0f;

        yield return null;
    }
}
