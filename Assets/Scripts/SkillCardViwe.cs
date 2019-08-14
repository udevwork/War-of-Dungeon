// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardViwe : MonoBehaviour {

    public Text nameText,DescText,CostText,LevelText;
    public Image icon;

    public void SetContent (Skill skill) {
        nameText.text = skill.skillName;
        DescText.text = skill.skillDescription;
        if (CostText)
        {
            CostText.text = "LEARN FOR " + skill.Cost + " SKULLS";
            LevelText.text = "SKILL LEVEL: " + skill.level;
            icon.sprite = skill.SpellIcon;
        }
	}

}
