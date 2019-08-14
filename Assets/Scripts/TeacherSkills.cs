// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;

public class TeacherSkills : MonoBehaviour {

    public SkillCardViwe One, Two, Three;

    public Button UBOne, UBTwo, UBThree;

	void Start () {
        One.SetContent(LevelModel.Player.charecter.Skill_One);
        Two.SetContent(LevelModel.Player.charecter.Skill_Two);
        Three.SetContent(LevelModel.Player.charecter.Skill_Three);
        ChackButtons();
	}

    public void Upgrade_one()
    {
        if (LevelModel.Player.charecter.Skill_One.Cost <= LevelModel.Player.Inventory.TrophySkulls)
        {
            LevelModel.Player.charecter.Skill_One.upgrade();
            One.SetContent(LevelModel.Player.charecter.Skill_One);
            NotificationController.show.BlackNotifi(LevelModel.Player.charecter.Skill_One.skillName + "UPGRADED", "LEVEL: "+LevelModel.Player.charecter.Skill_One.level);

        }
        else {
            NotificationController.show.BlackNotifi("Not enough Sculls!", "Not enough Sculls!");
        }
        ChackButtons();

    }
    public void Upgrade_two()
    {
        if (LevelModel.Player.charecter.Skill_Two.Cost <= LevelModel.Player.Inventory.TrophySkulls)
        {
        LevelModel.Player.charecter.Skill_Two.upgrade();
        Two.SetContent(LevelModel.Player.charecter.Skill_Two);
            NotificationController.show.BlackNotifi(LevelModel.Player.charecter.Skill_Two.skillName + "UPGRADED", "LEVEL: " + LevelModel.Player.charecter.Skill_Two.level);
        }
        else
        {
            NotificationController.show.BlackNotifi("Not enough Sculls!", "Not enough Sculls!");
        }
        ChackButtons();
    }
    public void Upgrade_three()
    {
        if (LevelModel.Player.charecter.Skill_Three.Cost <= LevelModel.Player.Inventory.TrophySkulls)
        {
        LevelModel.Player.charecter.Skill_Three.upgrade();
        Three.SetContent(LevelModel.Player.charecter.Skill_Three);
            NotificationController.show.BlackNotifi(LevelModel.Player.charecter.Skill_Three.skillName + "UPGRADED", "LEVEL: " + LevelModel.Player.charecter.Skill_Three.level);
        }
        else
        {
            NotificationController.show.BlackNotifi("Not enough Sculls!", "Not enough Sculls!");
        }
        ChackButtons();
    }

    public void ChackButtons()
    {
        if (UBOne)
        {
            if (LevelModel.Player.charecter.Skill_One.Cost >= LevelModel.Player.Inventory.TrophySkulls)
            {
                UBOne.interactable = false;
            }
            else
            {
                UBOne.interactable = true;
            }
            if (LevelModel.Player.charecter.Skill_Two.Cost >= LevelModel.Player.Inventory.TrophySkulls)
            {
                UBTwo.interactable = false;
            }
            else
            {
                UBTwo.interactable = true;
            }
            if (LevelModel.Player.charecter.Skill_Three.Cost >= LevelModel.Player.Inventory.TrophySkulls)
            {
                UBThree.interactable = false;
            }
            else
            {
                UBThree.interactable = true;
            }
        }
    }

}
