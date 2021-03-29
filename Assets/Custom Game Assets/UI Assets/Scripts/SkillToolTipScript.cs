using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillToolTipScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform movableBlock;
    private RectTransform informationBlock;
    private Vector2 offset;

    private Text skillName;
    private Text damage;
    private Text mana;
    private TextMeshProUGUI description;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        movableBlock = gameObject.GetComponentsInChildren<RectTransform>()[1];
        informationBlock = gameObject.GetComponentsInChildren<RectTransform>()[2];

        offset = 2f * informationBlock.anchoredPosition;

        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        skillName = texts[0];
        damage = texts[1];
        mana = texts[2];
        description = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UIEventSystem.current.onDraggingButton += Dragging;
        UIEventSystem.current.onShowSkillToolTip += SetSkill;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onDraggingButton -= Dragging;
        UIEventSystem.current.onShowSkillToolTip -= SetSkill;
    }

    private void FixedUpdate()
    {
        if (canvasGroup.alpha == 1f)
        {
            // Mouse position
            Vector2 mousePos = Input.mousePosition;
            // Card corners
            Vector3[] corners = new Vector3[4];
            informationBlock.GetWorldCorners(corners);
            // The size of the box
            Vector2 size = new Vector2(corners[2].x - corners[1].x, corners[2].y - corners[3].y);

            if (mousePos.x + size.x > Screen.width)
                mousePos.x = Screen.width - size.x;
            else if (mousePos.x < -offset.x)
                mousePos.x = -offset.x;

            if (mousePos.y + size.y > Screen.height)
                mousePos.y = Screen.height - size.y;
            else if (mousePos.y < -offset.y)
                mousePos.y = -offset.y;

            movableBlock.position = mousePos;
        }
    }

    private void Dragging(ButtonContainer btn, bool dragging)
    {
        if (dragging)
            canvasGroup.alpha = 0f;
        else
            canvasGroup.alpha = 1f;
    }

    private void SetSkill(Skill skill)
    {
        skillName.text = skill.skillName;
        damage.text = skill.GetDamageText();
        damage.color = skill.GetTextColor();
        mana.text = skill.manaCost + " mana";
        description.text = skill.GetDescription();
    }

    private void OnDrawGizmos()
    {
        if (informationBlock == null)
            return;

        Gizmos.color = Color.red;

        Vector3[] corners = new Vector3[4];
        informationBlock.GetWorldCorners(corners);
        Gizmos.DrawLine(corners[0], corners[1]);
        Gizmos.DrawLine(corners[1], corners[2]);
        Gizmos.DrawLine(corners[2], corners[3]);
        Gizmos.DrawLine(corners[3], corners[0]);
    }
}
