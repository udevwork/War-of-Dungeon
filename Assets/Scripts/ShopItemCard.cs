// Denis super code 

using UnityEngine;
using UnityEngine.UI;
using DataBase;
using UnityEngine.Advertisements;

public class ShopItemCard : MonoBehaviour
{


    public Image icon;
    public Text descText;
    public Text costText;
    public Text fragmentsCountText;

    public Image CostIcon;
    public Sprite WarCoins;
    public Sprite Sculls;
    public Sprite Crystal;

    public int cost;
    public int fragments;
    public NextGenItem NGI;

    public void SetCard(NextGenItem ITEM, int fragments)
    {
        NGI = ITEM;
        icon.sprite = NGI.icon;
        this.fragments = fragments;
        if (NGI.itemType == NextGenItem.ItemType.Runa)
        {
            descText.text = NGI.MagicForce.ToString() + " Magic force. ";
          //  descText.text += "Runa for " + NGI.BelongTo.ToString() + ". ";
            descText.text += "Type " + NGI.itemItemQuality.ToString() + ". ";
        }
        if (NGI.itemType == NextGenItem.ItemType.Shild)
        {
            descText.text = NGI.Defense.ToString() + " Defense. ";
          //  descText.text += "Shild for " + NGI.BelongTo.ToString() + ". ";
            descText.text += "Type " + NGI.itemItemQuality.ToString() + ". ";
        }
        if (NGI.itemType == NextGenItem.ItemType.Weapon)
        {
            descText.text = NGI.Damage.ToString() + " Damage. ";
          //  descText.text += "Damage for " + NGI.BelongTo.ToString() + ". ";
            descText.text += "Type " + NGI.itemItemQuality.ToString() + ". ";
        }

        if (NGI.itemItemQuality == NextGenItem.ItemQuality.STANDARD)
        {
            cost = fragments * 1000;
        }
        if (NGI.itemItemQuality == NextGenItem.ItemQuality.POWERFUL)
        {
            cost = fragments * 100;
        }
        if (NGI.itemItemQuality == NextGenItem.ItemQuality.LEGENDARY)
        {
            cost = fragments * 10;
        }
        costText.text = "COST: " + cost.ToString();
        fragmentsCountText.text = "GET " + fragments + " FRAGMENTS";
    }

    public void Buy(){
        Debug.Log("BUY");
        if (NGI.itemItemQuality == NextGenItem.ItemQuality.STANDARD)
        {
            if (cost <= LevelModel.Player.Inventory.WarCoins)
            {
                LevelModel.Player.Inventory.WarCoins -= cost;
                NGI.AddSegment(fragments);
                NotificationController.show.BlackNotifi("PURCHASED", NGI.itemName);
            } else {
                NotificationController.show.BlackNotifi("ERROR", "Not enough coins!");
            }
        }
        if (NGI.itemItemQuality == NextGenItem.ItemQuality.POWERFUL)
        {
            if (cost <= LevelModel.Player.Inventory.TrophySkulls)
            {
                LevelModel.Player.Inventory.TrophySkulls -= cost;
                NGI.AddSegment(fragments);
                NotificationController.show.BlackNotifi("PURCHASED", NGI.itemName);
            }else
            {
                NotificationController.show.BlackNotifi("ERROR", "Not enough Sculls!");
            }
        }
        if (NGI.itemItemQuality == NextGenItem.ItemQuality.LEGENDARY)
        {
            if (cost <= LevelModel.Player.Inventory.Crystals)
            {
                LevelModel.Player.Inventory.Crystals -= cost;
                NGI.AddSegment(fragments);
                NotificationController.show.BlackNotifi("PURCHASED", NGI.itemName);
            }else
            {
                NotificationController.show.BlackNotifi("ERROR", "Not enough crystals!");
            }
        }

    }

    public void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show("rewardedVideo", options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            NotificationController.show.BlackNotifi("THANKS!", "YOU GET A REVARD");
            LevelController.instance.AddCoinsToPlayer(1000);
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");
            NotificationController.show.BlackNotifi("Video was skipped", "lol");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
            NotificationController.show.BlackNotifi("failed to show", "sorry");
        }
    }

}
