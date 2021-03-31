using System;
using UnityEngine;

public class UIEventSystem : MonoBehaviour
{
    public static UIEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<bool> onHover;
    public void SetHover(bool hovering)
    {
        if (onHover != null)
        {
            onHover(hovering);
        }
    }

    public event Action<Skill> onShowSkillToolTip;
    public void ShowSkillToolTip(Skill skill)
    {
        if (onShowSkillToolTip != null)
        {
            onShowSkillToolTip(skill);
        }
    }

    public event Action onHideToolTip;

    public void HideToolTip()
    {
        if (onHideToolTip != null)
        {
            onHideToolTip();
        }
    }

    public event Action<ButtonContainer, bool> onDraggingButton;
    public void DraggingButton(ButtonContainer buttonContainer, bool dragging)
    {
        if (onDraggingButton != null)
        {
            onDraggingButton(buttonContainer, dragging);
        }
    }

    public event Action<bool> onSkillListUp;
    public void SetSkillListUp(bool up)
    {
        if (onSkillListUp != null)
        {
            onSkillListUp(up);
        }
    }

    public event Action<int> onHighlightButtonInSkillList;
    public void SetHighlight(int indexInAdapter)
    {
        if (onHighlightButtonInSkillList != null)
        {
            onHighlightButtonInSkillList(indexInAdapter);
        }
    }

    public event Action onUnhighlightButtonsInSkillList;
    public void UnHighlightSKillList()
    {
        if (onUnhighlightButtonsInSkillList != null)
        {
            onUnhighlightButtonsInSkillList();
        }
    }

    public event Action<int, float> onFreezeAllSkills;
    public void FreezeAllSkills(int uniqueAdapterIndex, float delay)
    {
        if (onFreezeAllSkills != null)
        {
            onFreezeAllSkills(uniqueAdapterIndex, delay);
        }
    }

    public event Action<int, float> onCancelSkill;
    public void CancelSkill(int uniqueAdapterIndex, float delay)
    {
        if (onCancelSkill != null)
        {
            onCancelSkill(uniqueAdapterIndex, delay);
        }
    }

    public event Action<int> onSkillPicked;
    public void SkillPicked(int skillIndexInAdapter)
    {
        if (onSkillPicked != null)
        {
            onSkillPicked(skillIndexInAdapter);
        }
    }

    public event Action<int, bool> onSkillPickedRegistered;
    public void SkillPickedRegister(int skillIndexInAdapter, bool startCooldownForThis)
    {
        if (onSkillPickedRegistered != null)
        {
            onSkillPickedRegistered(skillIndexInAdapter, startCooldownForThis);
        }
    }

    public event Action<int, float> onSkillCast;
    public void SkillCast(int uniqueId, float cooldown)
    {
        if (onSkillCast != null)
        {
            onSkillCast(uniqueId, cooldown);
        }
    }

    public event Action<float> onDodgeFinish;
    public void Dodged(float cooldown)
    {
        if (onDodgeFinish != null)
        {
            onDodgeFinish(cooldown);
        }
    }


    public event Action<Skill, float> onStartCooldown;
    public void StartCooldown(Skill skill, float delay)
    {
        if (onStartCooldown != null)
        {
            onStartCooldown(skill, delay);
        }
    }

    public event Action<Sprite, float> onApplyResistance;
    public void ApplyResistance(Sprite resistanceIcon, float duration)
    {
        if (onApplyResistance != null)
        {
            onApplyResistance(resistanceIcon, duration);
        }
    }

    public event Action onRemoveResistance;
    public void RemoveResistance()
    {
        if (onRemoveResistance != null)
        {
            onRemoveResistance();
        }
    }

    public event Action<int> onShowDialog;
    public void ShowDialog(int dialogindex)
    {
        if (onShowDialog != null)
        {
            onShowDialog(dialogindex);
        }
    }

    public event Action<int> onFinishedDialog;
    public void FinishedDialog(int dialogindex)
    {
        if (onFinishedDialog != null)
        {
            onFinishedDialog(dialogindex);
        }
    }
}
