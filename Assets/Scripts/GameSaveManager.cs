using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DataBase;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSaveManager : MonoBehaviour
{
    private Data data = new Data();

    public void Save (){
        Debug.Log("START SAVING GAME DATA");
        // save items
        foreach (NextGenItem item in LevelModel.ItemDataBase.getAllItems())
        {
            item.SaveItem();
        }
        foreach (HeroManiMenuCard item in LevelModel.Game.AllHeroCard)
        {
            item.SaveChar();
        }
        // save char
        data.savedPlayerInfo.IDcaracter = LevelModel.Player.charecter.Id;
        data.savedPlayerInfo.IDheroCard = LevelModel.Player.heroCard.Id;
        data.savedPlayerInfo.IDCharClass = LevelModel.Player.charecter.Class.Id;

        data.savedPlayerInfo.level = LevelModel.Player.Level;
        data.savedPlayerInfo.experience = LevelModel.Player.Experience.CurrentExperience;

        data.savedPlayerInfo.StatsPointEvalable = LevelModel.Player.Experience.StatsPointEvalable;
        data.savedPlayerInfo.ExperienceRequired = LevelModel.Player.Experience.ExperienceRequired;

        data.savedPlayerInfo.Vitality = LevelModel.Player.Stats.Vitality;
        data.savedPlayerInfo.Dexterity = LevelModel.Player.Stats.Dexterity;
        data.savedPlayerInfo.Intelligence = LevelModel.Player.Stats.Intelligence;
        data.savedPlayerInfo.Strength = LevelModel.Player.Stats.Strength;

        if (LevelModel.Player.Inventory.weapon)
        {
            data.savedPlayerInfo.IDCurrentWeapon = LevelModel.Player.Inventory.weapon.id;
        } else {
            data.savedPlayerInfo.IDCurrentWeapon = 0;
        }
        if (LevelModel.Player.Inventory.shild)
        {
            data.savedPlayerInfo.IDCurrentShild = LevelModel.Player.Inventory.shild.id;
        }
        else
        {
            data.savedPlayerInfo.IDCurrentWeapon = 0;
        }
        if (LevelModel.Player.Inventory.runa)
        {
            data.savedPlayerInfo.IDCurrentRuna = LevelModel.Player.Inventory.runa.id;
        }
        else
        {
            data.savedPlayerInfo.IDCurrentWeapon = 0;
        }

        data.savedPlayerInfo.SkilloneLevel = LevelModel.Player.charecter.Skill_One.level;
        data.savedPlayerInfo.SkilltwoLevel = LevelModel.Player.charecter.Skill_Two.level;
        data.savedPlayerInfo.SkillthreeLevel = LevelModel.Player.charecter.Skill_Three.level;

        data.savedPlayerInfo.WarCoins = LevelModel.Player.Inventory.WarCoins;
        data.savedPlayerInfo.TrophySkulls = LevelModel.Player.Inventory.TrophySkulls;
        data.savedPlayerInfo.Crystals = LevelModel.Player.Inventory.Crystals;
        // save more data

        data.savedPlayerInfo.CurretnFloor = LevelModel.Dungeon.curentFloor;
        data.savedPlayerInfo.MaxFloor = LevelModel.Player.Info.MaxFloor;
        data.savedPlayerInfo.TotalKills = LevelModel.Player.Info.totalKills;

        data.savedPlayerInfo.isFirstLounch = LevelModel.Game.isFirstLounch;

        PlayerPrefs.SetString("GAMEDATA",JsonUtility.ToJson(data));
        Debug.Log(JsonUtility.ToJson(data));
        Debug.Log("SAVING GAME DATA COMPLETE");
    }

    public void Load(){
        Debug.Log("START LOADING GAME DATA");
        if(PlayerPrefs.HasKey("GAMEDATA") == false){
            Debug.Log("NO DATA TO LOAD");
            return;
        }
        data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("GAMEDATA"));

        LevelModel.Game.isFirstLounch = data.savedPlayerInfo.isFirstLounch;

        // load items
        foreach (NextGenItem item in LevelModel.ItemDataBase.getAllItems())
        {
            item.LoadItem();
        }
        foreach (HeroManiMenuCard item in LevelModel.Game.AllHeroCard)
        {
            item.LoadChar();
        }
        // load char
        foreach (Charecter item in LevelModel.Game.AllChars)
        {
            if(data.savedPlayerInfo.IDcaracter == item.Id){
                LevelModel.Player.charecter = item;
            }
        }
   
        foreach (HeroManiMenuCard item in LevelModel.Game.AllHeroCard)
        {
            if (data.savedPlayerInfo.IDheroCard == item.Id)
            {
                LevelModel.Player.heroCard = item;
            }
        }
        foreach (CharecterClass item in LevelModel.Game.AllCharClasses)
        {
            if (data.savedPlayerInfo.IDCharClass == item.Id)
            {
                LevelModel.Player.charecter.Class = item;
            }
        }
     
        LevelModel.Player.Level = data.savedPlayerInfo.level;
        LevelModel.Player.Experience.CurrentExperience = data.savedPlayerInfo.experience;

        LevelModel.Player.Experience.StatsPointEvalable = data.savedPlayerInfo.StatsPointEvalable;
        LevelModel.Player.Experience.ExperienceRequired = data.savedPlayerInfo.ExperienceRequired;

        LevelModel.Player.Stats.Vitality = data.savedPlayerInfo.Vitality;
        LevelModel.Player.Stats.Dexterity = data.savedPlayerInfo.Dexterity;
        LevelModel.Player.Stats.Intelligence = data.savedPlayerInfo.Intelligence;
        LevelModel.Player.Stats.Strength = data.savedPlayerInfo.Strength;

     //   if (data.savedCharecter.IDCurrentWeapon != 0)
     //   {
     //       LevelModel.Player.Inventory.PutOnItem(LevelModel.ItemDataBase.GetItem((data.savedCharecter.IDCurrentWeapon)));
     //   }
     //   if (data.savedCharecter.IDCurrentShild != 0)
     //   {
     //       LevelModel.Player.Inventory.PutOnItem(LevelModel.ItemDataBase.GetItem((data.savedCharecter.IDCurrentWeapon)));
     //   }
     //   if (data.savedCharecter.IDCurrentRuna != 0)
     //   {
     //      LevelModel.Player.Inventory.PutOnItem(LevelModel.ItemDataBase.GetItem((data.savedCharecter.IDCurrentWeapon)));
     //  }

        LevelModel.Player.charecter.Skill_One.level = data.savedPlayerInfo.SkilloneLevel;
        LevelModel.Player.charecter.Skill_Two.level = data.savedPlayerInfo.SkilltwoLevel;
        LevelModel.Player.charecter.Skill_Three.level = data.savedPlayerInfo.SkillthreeLevel;

        LevelModel.Player.Inventory.WarCoins = data.savedPlayerInfo.WarCoins;
        LevelModel.Player.Inventory.TrophySkulls = data.savedPlayerInfo.TrophySkulls;
        LevelModel.Player.Inventory.Crystals = data.savedPlayerInfo.Crystals;

        // load more data
        LevelModel.Dungeon.curentFloor = data.savedPlayerInfo.CurretnFloor;
        LevelModel.Player.Info.MaxFloor = data.savedPlayerInfo.MaxFloor;
        LevelModel.Player.Info.totalKills = data.savedPlayerInfo.TotalKills;
        Debug.Log("LOADING GAME DATA COMPLETE");
    }

    #if UNITY_EDITOR
     [MenuItem("My Commands/Take Screenshots _c")]
    #endif

    public static void ClearAllSavedData(){
        Debug.Log("CLEAR ALL SAVE");
        PlayerPrefs.DeleteAll();
    }

    #if UNITY_EDITOR
    [MenuItem("My Commands/Take Screenshots _g")]
    #endif

    public static void GetNAMES()
    {
        string s = "";
        foreach (NextGenItem item in LevelModel.ItemDataBase.getAllItems())
        {
            s += item.name + "\n";
        }
        Debug.Log(s);
    }

    [System.Serializable]
    public class Data{
        public SavedPlayerInfo savedPlayerInfo = new SavedPlayerInfo();
    }

    [System.Serializable]
    public class SavedPlayerInfo
    {
        public int IDcaracter;
        public int IDheroCard;
        public int IDCharClass;

        public int level;
        public float experience;
        public int StatsPointEvalable;
        public float ExperienceRequired;
        // money
        public  int WarCoins;
        public  int TrophySkulls;
        public  int Crystals;
        // stats
        public  int Vitality;
        public  int Dexterity;
        public  int Intelligence;
        public  int Strength;
        public  int IDCurrentWeapon;
        public int IDCurrentShild;
        public int IDCurrentRuna;

        public int SkilloneLevel;
        public int SkilltwoLevel;
        public int SkillthreeLevel;

        public bool isFirstLounch;
        public int CurretnFloor;
        public int MaxFloor;
        public int TotalKills;

    }

}



