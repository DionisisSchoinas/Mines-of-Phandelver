using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class OverlayControls : MonoBehaviour
{
    public GameObject buttonQuickbar;
    public CanvasGroup spellListDisplay;
    public GameObject columnContentHolder;
    public GameObject effectsDisplay;
    public Color buttonColorSelected;
    public Color buttonColorUnselected;
    public CanvasGroup escapeMenu;
    public CanvasGroup objectiveMenu;
    public CanvasGroup skillToolTip;
    public CanvasGroup settingsMenu;
    public CanvasGroup dialogBox;
    public CanvasGroup gameEndScreen;
    public CanvasGroup loadingScreen;
   
    // Quickbar data
    [HideInInspector]
    public Button[] quickbarButtons;
    [HideInInspector]
    public RectTransform[] quickbarButtonTransforms;
    [HideInInspector]
    public QuickbarButton[] quickbarButtonContainers;
    [HideInInspector]
    public OverlayToWeaponAdapter overlayToWeaponAdapter;

    private SkillListFill skillList;
    private int selectedQuickbarIndex;
    private bool skillListUp;
    private bool escapeMenuUp;
    private bool objectiveMenuUp;
    private bool playerLocked;

    private EscapeMenuController escapeMenuScript;

    public static float skillFreezeAfterPicking = 0.1f;
    public static float skillFreezeAfterCasting = 0.2f;
    public static Color selectedButtonColor;
    public static Color unselectedButtonColor;

    private float mana;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ResourceManager.UI.Sounds.ButtonHoverEnter;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Sound Effects")[0];

        escapeMenuScript = escapeMenu.gameObject.GetComponent<EscapeMenuController>();

        SetCanvasState(true, loadingScreen);
    }

    private void Start()
    {
        overlayToWeaponAdapter = FindObjectOfType<OverlayToWeaponAdapter>();

        selectedButtonColor = buttonColorSelected;
        unselectedButtonColor = buttonColorUnselected;

        skillList = gameObject.AddComponent<SkillListFill>();
        skillList.weaponAdapter = overlayToWeaponAdapter;
        skillList.overlayControls = this;
        skillList.columnContentHolder = columnContentHolder;

        skillList.FillList();

        SetCanvasState(false, spellListDisplay);
        SetCanvasState(false, escapeMenu);
        SetCanvasState(false, objectiveMenu);
        spellListDisplay.gameObject.AddComponent<ElementHover>();

        quickbarButtons = buttonQuickbar.GetComponentsInChildren<Button>();

        for (int i = 0; i < quickbarButtons.Length; i++)
        {
            if (quickbarButtons[i] == null)
            {
                Debug.LogError("Button with index = " + i + " on the quickbar was null");
                break;
            }
        }

        quickbarButtonContainers = new QuickbarButton[quickbarButtons.Length];
        quickbarButtonTransforms = new RectTransform[quickbarButtons.Length];
        for (int i=0; i<quickbarButtons.Length; i++)
        {
            Image buttonIcon = quickbarButtons[i].GetComponentsInChildren<Image>()[1];
            // Put container script on the quickbar buttons
            quickbarButtonContainers[i] = quickbarButtons[i].gameObject.AddComponent<QuickbarButton>();
            Skill skill;
            if (i == 0) // Set to 1st quickbar button the default skill
            {
                skill = overlayToWeaponAdapter.GetSkillFromIndex(-1);
                quickbarButtonContainers[i].swappable = false;
                // Save values on the buttons script
                quickbarButtonContainers[i].buttonData = new ButtonData(quickbarButtons[i], skill, i, -1, buttonIcon);
            }
            else
            {
                skill = overlayToWeaponAdapter.GetSkillFromIndex(i-1);
                // Save values on the buttons script
                quickbarButtonContainers[i].buttonData = new ButtonData(quickbarButtons[i], skill, i, i-1, buttonIcon);
            }
            quickbarButtonContainers[i].overlayControls = this;
            quickbarButtonContainers[i].parent = buttonQuickbar.transform;
            quickbarButtonContainers[i].SetSelectionColor(unselectedButtonColor);

            // Transforms
            quickbarButtonTransforms[i] = quickbarButtons[i].GetComponent<RectTransform>();
        }

        // Hightlight the quickbar skills in the skill list
        HighlightQuickbarInList();

        skillListUp = false;
        escapeMenuUp = false;
        objectiveMenuUp = false;
        playerLocked = false;

        SetCanvasState(false, skillToolTip);

        UIEventSystem.current.onDraggingButton += DraggingButton;
        UIEventSystem.current.onApplyResistance += ApplyResistance;
        ManaEventSystem.current.onManaUpdated += SetCurrentMana;

        UIEventSystem.current.onShowSkillToolTip += ShowSkillToolTip;
        UIEventSystem.current.onHideToolTip += HideToolTip;
        UIEventSystem.current.onPlayerLocked += PlayerLock;

        StartCoroutine(SetFirstSelection());
    }

    private IEnumerator SetFirstSelection()
    {
        yield return new WaitForEndOfFrame();
        SetSelectedQuickBar(0);
        // Requests update for mana values
        ManaEventSystem.current.UseMana(0);
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onDraggingButton -= DraggingButton;
        UIEventSystem.current.onApplyResistance -= ApplyResistance;
        ManaEventSystem.current.onManaUpdated -= SetCurrentMana;
        UIEventSystem.current.onShowSkillToolTip -= ShowSkillToolTip;
        UIEventSystem.current.onHideToolTip -= HideToolTip;
        UIEventSystem.current.onPlayerLocked -= PlayerLock;
    }

    private void Update()
    {
        if ((!skillListUp && playerLocked) || gameEndScreen.alpha == 1f || loadingScreen.alpha == 1f)
            return;

        // Quick bar inptus
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetSelectedQuickBar(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetSelectedQuickBar(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetSelectedQuickBar(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SetSelectedQuickBar(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            SetSelectedQuickBar(4);
        }

        // Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (skillListUp)
            {
                ChangeSkillListState();
            }
            else if (settingsMenu.alpha == 1f)
            {
                SetCanvasState(false, settingsMenu);
            }
            else if (escapeMenuScript.mode == -1)
            {
                EscapeMenu();
            }
        }

        // Spell List
        if (!escapeMenuUp && Input.GetKeyDown(KeyCode.K))
        {
            ChangeSkillListState();
        }
    }

    private void PlayerLock(bool lockPlayer)
    {
        playerLocked = lockPlayer;
    }

    private void ApplyResistance(Sprite resistanceIcon, float duration)
    {
        EffectDisplayContainer resistanceEffect = Instantiate(ResourceManager.UI.EffectDisplay, effectsDisplay.transform).AddComponent<EffectDisplayContainer>();
        resistanceEffect.SetResistance(resistanceIcon);
        resistanceEffect.StartCountdown(duration);
    }

    private int HoveringQuickbarButtons()
    {
        for (int i = 0; i < quickbarButtonTransforms.Length; i++)
        {
            if (quickbarButtonContainers[i].swappable)
            {
                Vector2 localMousePosition = quickbarButtonTransforms[i].InverseTransformPoint(Input.mousePosition);
                if (quickbarButtonTransforms[i].rect.Contains(localMousePosition))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private void HighlightQuickbarInList()
    {
        List<int> indexInAdapter = quickbarButtonContainers.Select(cont => cont.buttonData.skillIndexInAdapter).ToList();

        skillList.HightlightQuickbarSkills(indexInAdapter);
    }

    private void ChangeSkillListState()
    {
        skillListUp = !skillListUp;
        UIEventSystem.current.SetHover(skillListUp);
        UIEventSystem.current.SetSkillListUp(skillListUp);
        SetCanvasState(skillListUp, spellListDisplay);

        if (!skillListUp)
        {
            skillToolTip.alpha = 0f;
            SetSelectedQuickBar(selectedQuickbarIndex);
        }
    }

    public static void SetCanvasState(bool show, CanvasGroup canvasGroup)
    {
        if (show)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public static void SetCanvasState(float alpha, CanvasGroup canvasGroup)
    {
        if (alpha > 0f)
        {
            canvasGroup.alpha = Mathf.Min(alpha, 1f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void DraggingButton(ButtonContainer buttonContainer, bool dragging)
    {
        int hoveringOverButton = HoveringQuickbarButtons();

        if (dragging || hoveringOverButton == -1)
            return;

        // Skill list button
        if (buttonContainer.buttonData.quickBarIndex == -1)
        {
            BindSkillToQuickbar(buttonContainer, hoveringOverButton);
        }
        // Quickbar button
        else
        {
            // If hovering a quickbar button and it's not the same button
            if (hoveringOverButton != buttonContainer.buttonData.quickBarIndex)
            {
                // Set the hovering button to the button we are dragging around
                BindSkillToQuickbar(quickbarButtonContainers[hoveringOverButton], buttonContainer.buttonData.quickBarIndex);
                // Set the button we are dragging around to the hovering button
                BindSkillToQuickbar(buttonContainer, hoveringOverButton);
            }
        }
    }

    // Binds the container data to the button with this index -> selectedQuickbar
    private void BindSkillToQuickbar(ButtonContainer container, int selectedQuickbar)
    {
        quickbarButtonContainers[selectedQuickbar].buttonData.NewData(container);
        HighlightQuickbarInList();
    }

    public void SetSelectedQuickBar(int selectedQuickbar)
    {
        if (!skillListUp)
        {
            if (quickbarButtonContainers[selectedQuickbar].buttonData.skill.manaCost <= mana)
            {
                audioSource.Play();

                selectedQuickbarIndex = selectedQuickbar;
                // Update Adapter
                UIEventSystem.current.SkillPicked(quickbarButtonContainers[selectedQuickbar].buttonData.skillIndexInAdapter);
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }
    }

    private void EscapeMenu()
    {
        escapeMenuUp = !escapeMenuUp;
        SetCanvasState(escapeMenuUp, escapeMenu);
        PauseGame(escapeMenuUp);
    }

    public void ObjectiveMenu()
    {
        objectiveMenuUp = !objectiveMenuUp;
        SetCanvasState(objectiveMenuUp, objectiveMenu);
        PauseGame(objectiveMenuUp);
    }

    private void PauseGame(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void SetCurrentMana(float mana)
    {
        this.mana = mana;
    }

    private void ShowSkillToolTip(Skill skill)
    {
        skillToolTip.alpha = 1f;
    }

    private void HideToolTip()
    {
        skillToolTip.alpha = 0f;
    }
}
