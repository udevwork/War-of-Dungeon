// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;
using UnityEngine.Advertisements;

public class RewardChestBehavior : MonoBehaviour
{

    public static RewardChestBehavior instance;

    public enum ChestType
    {
        Small,
        Large,
        Medium,
        Epic
    }


    public GameObject smallChest, largeChest, mediumChest, epicChest;
    public Animation sceneAnimator;
    public Animation ChestAnim;

    public Text rewardText;
    public Image rewardImage;

    public Image CloudImage;
    public Color smallColor, largeColor, mediumColor, epicColor;

    private void Awake()
    {
        instance = this;   
    }


    public void Reward(ChestType type,int SegmentsCount){

        smallChest.SetActive(false);
        largeChest.SetActive(false);
        mediumChest.SetActive(false);
        epicChest.SetActive(false);

        switch (type)
        {
            case ChestType.Small:
                CloudImage.color = smallColor;
                smallChest.SetActive(true);
                ChestAnim = smallChest.GetComponent<Animation>();
                GenerateReward(SegmentsCount,LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.STANDARD));
                break;
            case ChestType.Medium:
                CloudImage.color = mediumColor;
                mediumChest.SetActive(true);
                ChestAnim = mediumChest.GetComponent<Animation>();
                GenerateReward(SegmentsCount, LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.STANDARD));
                break;
            case ChestType.Large:
                CloudImage.color = largeColor;
                largeChest.SetActive(true);
                ChestAnim = largeChest.GetComponent<Animation>();
                GenerateReward(SegmentsCount, LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.POWERFUL));
                break;
            case ChestType.Epic:
                CloudImage.color = epicColor;
                epicChest.SetActive(true);
                ChestAnim = epicChest.GetComponent<Animation>();
                GenerateReward(SegmentsCount, LevelModel.ItemDataBase.GetItem(NextGenItem.ItemQuality.LEGENDARY));
                break;
        }
        sceneAnimator.Play("REWARD CHEST OPEN");
    }

    public void PlayMusic(){
        SoundFX.play.PlayChestOpenSound();
    }

    public void StartSceneAnimation()
    {
        sceneAnimator.Play();
    }

    public void OpenChest()
    {
        SoundFX.play.OpenChest();

        ChestAnim.Play("CHEST OPENING");

    }

    public void GenerateReward(int segments,NextGenItem item){
        LevelModel.ItemDataBase.UpgradeItem(item.id, segments);
        rewardImage.sprite = LevelModel.ItemDataBase.GetItem(item.id).icon;
        rewardText.text = "YOU GET " + segments + " FRAGMENTS OF: " + item.itemName.ToUpper() + "!";
            }

    public void CollectRevardButton()
    {
        ChestAnim.Play("CHEST CLOSENNING");
        sceneAnimator.Play("REWARD CHEST OPEN END");
    }


}

