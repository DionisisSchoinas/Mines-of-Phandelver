using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillToolTipScript : MonoBehaviour
{
    private Text skillName;
    private Text damage;
    private Text mana;
    private TextMeshProUGUI description;

    

    private void Awake()
    {
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        skillName = texts[0];
        damage = texts[1];
        mana = texts[2];
        description = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UIEventSystem.current.onShowSkillToolTip += SetSkill;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onShowSkillToolTip -= SetSkill;
    }

    private void SetSkill(Skill skill)
    {
        skillName.text = skill.skillName;
        damage.text = skill.GetDamageText();
        damage.color = skill.GetTextColor();
        mana.text = skill.manaCost + " mana";
    }
}
