// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DataBase;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class hucksterBehavior : MonoBehaviour {

    public GameObject windowToOpen;

    public ShopItemCard wep_1,wep_2,wep_3,wep_4,wep_5,wep_6,wep_7;

    public Text coinsText, scullsText, crystalText;


    public Animation anim;
    public string animationName;
    public Scrollbar SCRbar;


    [Header("Quality")]
    public NextGenItem.ItemQuality quality;
    [Header("Type")]
    public NextGenItem.ItemType type;

    public IEnumerator Start()
    {
        coinsText.text = LevelModel.Player.Inventory.WarCoins.ToString();
        scullsText.text = LevelModel.Player.Inventory.TrophySkulls.ToString();
        crystalText.text = LevelModel.Player.Inventory.Crystals.ToString();

        LevelModel.Player.Inventory.OnWarCoinsCountChange += (int obj) => coinsText.text = obj.ToString();
        LevelModel.Player.Inventory.OnTrophySkullsCountChange += (int obj) => scullsText.text = obj.ToString();
        LevelModel.Player.Inventory.OnCrystalsCountChange += (int obj) => crystalText.text = obj.ToString();


        if (anim != null)
        {
            anim.Play(animationName);
            anim[animationName].speed = 0;
        }
        wep_1.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.STANDARD, type), 15);
        wep_2.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.STANDARD, type), 15);
        wep_3.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.STANDARD, type), 15);
        wep_4.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.STANDARD, type), 15);
        wep_5.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.POWERFUL, type), 10);
        wep_6.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.POWERFUL, type), 10);
        wep_7.SetCard(LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.LEGENDARY, type), 5);
        yield return 1;
    }

    public void SliderValChange(){
        if (anim != null)
        {
            anim[animationName].normalizedTime = SCRbar.value;
        }
    }

    public void Open()
    {
        if (windowToOpen)
        {
            windowToOpen.SetActive(true);
        }
    }
}
