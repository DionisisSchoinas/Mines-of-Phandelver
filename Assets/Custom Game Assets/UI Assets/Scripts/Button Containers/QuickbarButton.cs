using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickbarButton : ButtonContainer, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [HideInInspector]
    public bool swappable;

    private Vector2 lastPosition;

    private bool pointerUp;
    private bool lockedPlayer;

    public new void Awake()
    {
        base.Awake();

        pointerUp = true;
        swappable = true;
        lockedPlayer = false;

        UIEventSystem.current.onSkillPickedRegistered += SelectButton;
        UIEventSystem.current.onSkillListUp += SkillListUp;
        UIEventSystem.current.onPlayerLocked += LockedPlayer;
    }

    public new void OnDestroy()
    {
        base.OnDestroy();
        UIEventSystem.current.onSkillPickedRegistered -= SelectButton;
        UIEventSystem.current.onSkillListUp -= SkillListUp;
        UIEventSystem.current.onPlayerLocked -= LockedPlayer;
    }

    private void SelectButton(int skillIndexInAdapter, bool startCooldown)
    {
        if (buttonData.skillIndexInAdapter == skillIndexInAdapter)
        {
            buttonSelection.color = OverlayControls.selectedButtonColor;
        }
        else
        {
            buttonSelection.color = OverlayControls.unselectedButtonColor;
        }
    }

    private void LockedPlayer(bool lockedPlayer)
    {
        this.lockedPlayer = lockedPlayer;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!skillListUp && !lockedPlayer)
            overlayControls.SetSelectedQuickBar(buttonData.quickBarIndex);
    }
    
    private QuickbarButton ReInstantiate()
    {
        // Instatiate new button and get components
        GameObject btnGm = Instantiate(gameObject, parent);
        Button btn = btnGm.GetComponent<Button>();
        QuickbarButton btnScript = btnGm.GetComponent<QuickbarButton>();

        // Set new values in script
        btnScript.buttonData = new ButtonData();
        btnScript.buttonData.CopyData(btn, this);
        btnScript.skillListUp = skillListUp;
        btnScript.overlayControls = overlayControls;
        btnScript.parent = parent;
        btnScript.rect.position = lastPosition;

        // Set values back in OverlayControls
        overlayControls.quickbarButtons[buttonData.quickBarIndex] = btnScript.button;
        overlayControls.quickbarButtonContainers[buttonData.quickBarIndex] = btnScript;
        overlayControls.quickbarButtonTransforms[buttonData.quickBarIndex] = btnScript.rect;

        btnScript.transform.SetSiblingIndex(btnScript.buttonData.quickBarIndex);

        return btnScript;
    }

    public new void OnDrag(PointerEventData eventData)
    {
        if (skillListUp && swappable)
        {
            Drag(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUp = false;

        if (skillListUp && swappable)
        {
            //audioSource.Play();

            ReInstantiate();

            clickPositionOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
            transform.parent = canvas;
            lastPosition = rect.position;

            rect.position = rect.position + Vector3.up * 3f + Vector3.left * 3f;

            UIEventSystem.current.DraggingButton(this, true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUp = true;

        if (skillListUp && swappable)
        {
            GameObject gm = new GameObject();
            AudioSource a = gm.AddComponent<AudioSource>();
            a.clip = ResourceManager.UI.Sounds.ButtonPick;
            a.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Sound Effects")[0];
            a.Play();
            Destroy(gm, 2f);

            UIEventSystem.current.DraggingButton(this, false);

            Destroy(gameObject);
        }
    }

    private void SkillListUp(bool skillList)
    {
        if (!skillListUp && !pointerUp)
        {
            // Notify event
            UIEventSystem.current.DraggingButton(this, false);
            // Destroy drag around button
            Destroy(gameObject);
        }
    }
}
