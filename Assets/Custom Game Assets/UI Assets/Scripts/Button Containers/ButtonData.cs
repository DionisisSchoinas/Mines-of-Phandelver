using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    public int quickBarIndex;
    public int skillIndexInAdapter;
    public int skillIndexInColumn;
    public int skillColumnIndex;
    public Image buttonImage;
    public Skill skill;
    public Button container;
    public ButtonContainer containerScript;

    public ButtonData()
    {
        this.quickBarIndex = -1;
        this.skillIndexInAdapter = -2;
        this.skillIndexInColumn = -1;
        this.buttonImage = null;
        this.skill = null;
        this.container = null;
        this.containerScript = null;
    }

    // Base for all constructors
    public ButtonData(Button container, Skill skill, int quickBarIndex, int skillIndexInAdapter, int skillColumnIndex)
    {
        this.container = container;
        this.containerScript = container.gameObject.GetComponent<ButtonContainer>();
        this.skill = skill;
        this.quickBarIndex = quickBarIndex;
        this.skillIndexInAdapter = skillIndexInAdapter;
        this.skillColumnIndex = skillColumnIndex;
        CheckForIcon();
    }

    public ButtonData(Button container, Skill skill, int quickBarIndex, int skillIndexInAdapter, int skillIndexInColumn, int skillColumnIndex) : this(container, skill, quickBarIndex, skillIndexInAdapter, skillColumnIndex)
    {
        this.skillIndexInColumn = skillIndexInColumn;
    }

    public ButtonData(Button container, Skill skill, int quickBarIndex, int skillIndexInAdapter, Image buttonImage) : this(container, skill, quickBarIndex, skillIndexInAdapter, -1)
    {
        this.buttonImage = buttonImage;
        this.buttonImage.sprite = skill.GetIcon();
    }

    public ButtonData(Button container, Skill skill, int quickBarIndex, int skillIndexInAdapter, int skillColumnIndex, Image buttonImage) : this(container, skill, quickBarIndex, skillIndexInAdapter, skillColumnIndex)
    {
        this.buttonImage = buttonImage;
        this.buttonImage.sprite = skill.GetIcon();
    }

    public ButtonData(Button container, Skill skill, int quickBarIndex, int skillIndexInAdapter, int skillIndexInColumn, int skillColumnIndex, Image buttonImage) : this(container, skill, quickBarIndex, skillIndexInAdapter, skillIndexInColumn, skillColumnIndex)
    {
        this.buttonImage = buttonImage;
        this.buttonImage.sprite = skill.GetIcon();
    }

    public void NewData(ButtonContainer container)
    {
        ButtonData data = container.buttonData;

        this.skillIndexInAdapter = data.skillIndexInAdapter;
        this.skillIndexInColumn = data.skillIndexInColumn;
        this.skillColumnIndex = data.skillColumnIndex;
        this.skill = data.skill;
        CheckForIcon();
        this.buttonImage.sprite = data.skill.GetIcon();

        this.containerScript.cooldownPercentage = container.cooldownPercentage;
        this.containerScript.CheckCooldown();
    }

    public void CopyData(Button newButton, ButtonContainer container)
    {
        ButtonData data = container.buttonData;

        this.container = newButton;
        this.containerScript = newButton.gameObject.GetComponent<ButtonContainer>();

        this.quickBarIndex = data.quickBarIndex;
        this.skillIndexInAdapter = data.skillIndexInAdapter;
        this.skillIndexInColumn = data.skillIndexInColumn;
        this.skillColumnIndex = data.skillColumnIndex;
        this.skill = data.skill;
        CheckForIcon();
        this.buttonImage.sprite = data.skill.GetIcon();

        this.containerScript.cooldownPercentage = container.cooldownPercentage;
        this.containerScript.CheckCooldown();
    }

    private void CheckForIcon()
    {
        if (buttonImage == null)
        {
            buttonImage = container.gameObject.GetComponentsInChildren<Image>()[1];
        }
    }

    public void PrintData()
    {
        Debug.Log(quickBarIndex + " " + skillIndexInAdapter + " " + skillIndexInColumn + " " + skillColumnIndex + " " + skill.skillName + " " + skill.skillName + " " + container.name);
    }
}