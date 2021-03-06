using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillListButton : ButtonContainer, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerUp;

    public new void Awake()
    {
        base.Awake();

        pointerUp = true;

        UIEventSystem.current.onHighlightButtonInSkillList += Highlight;
        UIEventSystem.current.onUnhighlightButtonsInSkillList += UnHighlight;
        UIEventSystem.current.onSkillListUp += SkillListUp;
    }

    public new void OnDestroy()
    {
        base.OnDestroy();
        UIEventSystem.current.onHighlightButtonInSkillList -= Highlight;
        UIEventSystem.current.onUnhighlightButtonsInSkillList -= UnHighlight;
        UIEventSystem.current.onSkillListUp -= SkillListUp;
    }

    private void Highlight(int indexInAdapter)
    {
        if (buttonData.skillIndexInAdapter == indexInAdapter)
        {
            buttonSelection.color = OverlayControls.selectedButtonColor;
        }
    }
    private void UnHighlight()
    {
        buttonSelection.color = OverlayControls.unselectedButtonColor;
    }

    private SkillListButton ReInstantiate()
    {
        SkillListButton btn = Instantiate(gameObject, parent).GetComponent<SkillListButton>();

        btn.buttonData = buttonData;
        btn.overlayControls = overlayControls;
        btn.parent = parent;
        btn.cooldownPercentage = cooldownPercentage;
        btn.skillListUp = skillListUp;

        btn.CheckCooldown();

        btn.transform.SetSiblingIndex(buttonData.skillIndexInColumn);
        return btn;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUp = false;

        // If skill list not up
        if (!skillListUp)
            return;

        audioSource.Play();

        // Set new one to position
        ReInstantiate();
        // Get offset of mouse from position of transform
        clickPositionOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        // Move to canvas to allow drag around
        transform.parent = canvas;


        rect.position = rect.position + Vector3.up * 3f + Vector3.left * 3f;

        // Notify event
        UIEventSystem.current.DraggingButton(this, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pointerUp = true;

        // If skill list not up
        if (!skillListUp)
            return;

        GameObject gm = new GameObject();
        AudioSource a = gm.AddComponent<AudioSource>();
        a.clip = ResourceManager.UI.Sounds.ButtonPick;
        a.outputAudioMixerGroup = ResourceManager.Audio.AudioMixers.MainMixer.FindMatchingGroups("Sound Effects")[0];
        a.Play();
        Destroy(gm, 2f);

        // Notify event
        UIEventSystem.current.DraggingButton(this, false);
        // Destroy drag around button
        Destroy(gameObject);
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
