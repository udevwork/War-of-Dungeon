using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DataBase;


public class PlayerStatsPanel : UIPanelManager
{

    // STATS
    [SerializeField] private Text statsUpPoints;


    [SerializeField] private Text MaxHealth;
    [SerializeField] private Text Defense;
    [SerializeField] private Text Damage;
    [SerializeField] private Text SkillDamage;
    [SerializeField] private Text CriticalDamage;
    [SerializeField] private Text CriticalChanse;

    [SerializeField] private Text INT;
    [SerializeField] private Text DEX;
    [SerializeField] private Text STR;
    [SerializeField] private Text VIT;

    public Button addSTRbtn;
    public Button addDexbtn;
    public Button addIntbtn;
    public Button addDefbtn;

    // SKILLS
    [SerializeField] private Text Sone_desc;
    [SerializeField] private Text Stwo_desc;
    [SerializeField] private Text Sthree_desc;

    [SerializeField] private Text Sone_name;
    [SerializeField] private Text Stwo_name;
    [SerializeField] private Text Sthree_name;

    [SerializeField] private Text Sone;
    [SerializeField] private Text Stwo;
    [SerializeField] private Text Sthree;

    [SerializeField] private Image Sone_icon;
    [SerializeField] private Image Stwo_icon;
    [SerializeField] private Image Sthree_icon;


    public void Start()
    {
        SetStatsText();
        SetSkillsText();
        Sone_icon.sprite = LevelModel.Player.charecter.Skill_One.SpellIcon;
        Stwo_icon.sprite = LevelModel.Player.charecter.Skill_Two.SpellIcon;
        Sthree_icon.sprite = LevelModel.Player.charecter.Skill_Three.SpellIcon;
    }

    public void OnEnable()
    {
        SetStatsText();
        SetSkillsText();
        CheckForButtonsActive();
    }


    public void UpgradeStat(string val)
    {
        if (LevelModel.Player.Experience.StatsPointEvalable > 0)
        {
            switch (val)
            {
                case "Strength":
                    LevelModel.Player.Stats.Strength += 1;
                    break;
                case "Dexterity":
                    LevelModel.Player.Stats.Dexterity += 1;
                    break;
                case "Intelligence":
                    LevelModel.Player.Stats.Intelligence += 1;
                    break;
                case "Vitality":
                    LevelModel.Player.Stats.Vitality += 1;
                    break;

            }
            LevelModel.Player.Experience.StatsPointEvalable -= 1;
            CheckForButtonsActive();
            SetStatsText();
        }
    }

    public void UpgradeSkills(string val)
    {
        if (LevelModel.Player.Experience.SkillsPointEvalable > 0)
        {
            switch (val)
            {
                case "one":
                    LevelModel.Player.charecter.Skill_One.upgrade();
                    break;
                case "two":
                    LevelModel.Player.charecter.Skill_Two.upgrade();
                    break;
                case "three":
                    LevelModel.Player.charecter.Skill_Three.upgrade();
                    break;
            }
            LevelModel.Player.Experience.SkillsPointEvalable -= 1;
            CheckForButtonsActive();
            SetSkillsText();
        }
    }



    public override void Open()
    {
        base.Open();
        CheckForButtonsActive();
    }


    public void CheckForButtonsActive()
    {
        if (LevelModel.Player.Experience.StatsPointEvalable <= 0)
        {
            addSTRbtn.interactable = false;
            addDexbtn.interactable = false;
            addIntbtn.interactable = false;
            addDefbtn.interactable = false;
        }
        else if (LevelModel.Player.Experience.StatsPointEvalable > 0)
        {
            addSTRbtn.interactable = true;
            addDexbtn.interactable = true;
            addIntbtn.interactable = true;
            addDefbtn.interactable = true;
        }
    }


    public void SetStatsText()
    {
        statsUpPoints.text = "UpPoints: " + LevelModel.Player.Experience.StatsPointEvalable.ToString();

        MaxHealth.text = "Max Health: " + LevelModel.Player.MaxHealth.ToString();
        Defense.text = "Defense: " + LevelModel.Player.Defense.ToString();
        Damage.text = "Damage: " + LevelModel.Player.Damage.ToString();
        SkillDamage.text = "Skill Damage: " + LevelModel.Player.SkillDamage.ToString();
        CriticalDamage.text = "Crit Damage: " + LevelModel.Player.CriticalDamage.ToString() + "%";
        CriticalChanse.text = "Crit Chanse: " + LevelModel.Player.CriticalChanse.ToString()+ "%";

        INT.text = LevelModel.Player.Stats.Intelligence.ToString();
        DEX.text = LevelModel.Player.Stats.Dexterity.ToString();
        STR.text = LevelModel.Player.Stats.Strength.ToString();
        VIT.text = LevelModel.Player.Stats.Vitality.ToString();

    }

    public void SetSkillsText()
    {


        Sone_desc.text = LevelModel.Player.charecter.Skill_One.skillDescription;
        Stwo_desc.text = LevelModel.Player.charecter.Skill_Two.skillDescription;
        Sthree_desc.text = LevelModel.Player.charecter.Skill_Three.skillDescription;

        Sone_name.text = LevelModel.Player.charecter.Skill_One.skillName;
        Stwo_name.text = LevelModel.Player.charecter.Skill_Two.skillName;
        Sthree_name.text = LevelModel.Player.charecter.Skill_Three.skillName;

        Sone.text = LevelModel.Player.charecter.Skill_One.level.ToString();
        Stwo.text = LevelModel.Player.charecter.Skill_Two.level.ToString();
        Sthree.text = LevelModel.Player.charecter.Skill_Three.level.ToString();

    }




}
