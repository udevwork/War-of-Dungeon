// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;
using System;

public class GameConsole : MonoBehaviour {

    string[] commands = { 
        "cheats",
        "clear",
        "help",
        "give",
        "god",
        "coins",
        "sculls",
        "crystal",
        "chest",
        "statspoints",
        "win",
        "lose",
        "opencharacters",
        "openweapons",
        "upgradeweapon",
        "upgradeshild",
        "upgraderuna",
        "addfloor",
        "removefloor",
        "village",
        "mainmenu",
        "dungeon",
    };

    public bool isCheatsEnable;
    public GameObject ConsoleWindow;
    public UnityEngine.UI.InputField consoleInput;
    public UnityEngine.UI.Text consoleText;

    public void CommandEnter(){
        string[] userCommand = consoleInput.text.Split(new char[] { ' ' });
      
        if (userCommand[0] == commands[0])
        {
            if(userCommand[1] == "on"){
                isCheatsEnable = true;
                SetConsoleTest("cheats on");
                return;
            }
            if (userCommand[1] == "off")
            {
                isCheatsEnable = false;
                SetConsoleTest("cheats off");
                return;
            }
            SetConsoleTest("unknown command");
        }

        if(isCheatsEnable == false){
            SetConsoleTest("don't touch anything here");
            return;
        }

        if (userCommand.Length > 2)
        {
            SetConsoleTest("unknown command");
            return;
        }
        if (consoleInput.text == commands[1])
        {
            ClearConsoleTest();
        }
        if (consoleInput.text == commands[2])
        {
            foreach (string item in commands)
            {
                SetConsoleTest(item);
            }
        }
        if (userCommand[0] == commands[3])
        {
            LevelModel.ItemDataBase.GetItem(Convert.ToInt32(userCommand[1]));
            SetConsoleTest("You get " + LevelModel.ItemDataBase.GetItem(Convert.ToInt32(userCommand[1])).itemName);
        }
        if (userCommand[0] == commands[4])
        {
            if(userCommand[1] == "on") {
                LevelModel.Player.MainHeroScript.isGod = true;
                SetConsoleTest("GODMODE on");
            }
            if (userCommand[1] == "off")
            {
                LevelModel.Player.MainHeroScript.isGod = false;
                SetConsoleTest("GODMODE off");
            }
        }
        if (userCommand[0] == commands[5])
        {
            LevelModel.Player.Inventory.WarCoins += Convert.ToInt32(userCommand[1]); 
            SetConsoleTest("You get " + userCommand[1] + " WarCoins");
        }
        if (userCommand[0] == commands[6])
        {
            LevelModel.Player.Inventory.TrophySkulls += Convert.ToInt32(userCommand[1]);
            SetConsoleTest("You get " + userCommand[1] + " TrophySkulls");
        }
        if (userCommand[0] == commands[7])
        {
            LevelModel.Player.Inventory.Crystals += Convert.ToInt32(userCommand[1]);
            SetConsoleTest("You get " + userCommand[1] + " Crystals");
        }
        if (userCommand[0] == commands[8])
        {
            if (userCommand[1] == "small")
            {
                RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Small, 10);
                SetConsoleTest("You get ChestType.Small");
            }
            if (userCommand[1] == "medium")
            {
                RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Medium, 10);
                SetConsoleTest("You get ChestType.Medium");
            }
            if (userCommand[1] == "large")
            {
                RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Large, 10);
                SetConsoleTest("You get ChestType.Large");
            }
            if (userCommand[1] == "epic")
            {
                RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Epic, 10);
                SetConsoleTest("You get ChestType.Epic");

            }
        }
        if (userCommand[0] == commands[9])
        {
            LevelModel.Player.Experience.StatsPointEvalable += Convert.ToInt32(userCommand[1]);
            SetConsoleTest("You get " + userCommand[1] + " StatsPoint");
        }
        if (consoleInput.text == commands[10])
        {
            LevelModel.Dungeon.RoomsCount = 0;
        }
        if (consoleInput.text == commands[11])
        {
            LevelModel.Player.MainHeroScript.GetDamage(float.MaxValue,null);
        }
        if (consoleInput.text == commands[12])
        {
            foreach (HeroManiMenuCard item in LevelModel.Game.AllHeroCard)
            {
                item.isCharecterUnlocked = true;
            }    
        }
        if (consoleInput.text == commands[13])
        {
            foreach (NextGenItem item in LevelModel.ItemDataBase.getAllItems())
            {
                item.AddSegment(1);
            } 
        }
        if (consoleInput.text == commands[14])
        {
            if(LevelModel.Player.Inventory.weapon){
                LevelModel.Player.Inventory.weapon.AddSegment(Convert.ToInt32(userCommand[1]));
                SetConsoleTest("add " + userCommand[1] + " fragmets");
            } else {
                SetConsoleTest("add weapon");
            }
        }
        if (consoleInput.text == commands[15])
        {
            if (LevelModel.Player.Inventory.shild)
            {
                LevelModel.Player.Inventory.shild.AddSegment(Convert.ToInt32(userCommand[1]));
                SetConsoleTest("add " + userCommand[1] + " fragmets");
            }
            else
            {
                SetConsoleTest("add shild");
            }
        }
        if (consoleInput.text == commands[16])
        {
            if (LevelModel.Player.Inventory.runa)
            {
                LevelModel.Player.Inventory.runa.AddSegment(Convert.ToInt32(userCommand[1]));
                SetConsoleTest("add " + userCommand[1] + " fragmets");
            }
            else
            {
                SetConsoleTest("add runa");
            }
        }
        if (userCommand[0] == commands[17])
        {
            LevelModel.Dungeon.curentFloor += (Convert.ToInt32(userCommand[1]));
        }
        if (userCommand[0] == commands[18])
        {
            LevelModel.Dungeon.curentFloor -= (Convert.ToInt32(userCommand[1]));
        }
        if (consoleInput.text == commands[19])
        {
            LevelController.instance.GoToVillage();
        }
        if (consoleInput.text == commands[20])
        {
            LevelController.instance.ExitToMainMenuButton();
        }
        if (consoleInput.text == commands[21])
        {
            LevelController.instance.GoToNextLevel();
        }
    }

    public void SetConsoleTest(string text){
        consoleText.text += "\n" + text;
    }

    public void ClearConsoleTest()
    {
        consoleText.text = "";
    }


}
