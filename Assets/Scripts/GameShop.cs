// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;

public class GameShop : MonoBehaviour
{


    [SerializeField] private AdditionalCoolScrol CardScroll;
    [SerializeField] private GameObject weaponInfo;
    [SerializeField] private GameObject NoWeapon;

    [SerializeField] Button simpleButton;
    [SerializeField] Button powerfulButton;
    [SerializeField] Button legendaryButton;


    [SerializeField] private Sprite DAM, DEF, MAG;
    [SerializeField] private Image EffectImage;
    [SerializeField] Text shopItemEfectText;

    [SerializeField] private GameObject[] Stars;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemParent;

    public List<int> ItemsIDOnScreen;
    private NextGenItem currentSelectedItem;

    [SerializeField] Text shopItemLevel;
    [SerializeField] Text shopItemSegmentsNeeded;

    [SerializeField] Text PutOnButtonText;
    [SerializeField] Button PutOnButton;


    [SerializeField] Slider shopSlider;

    [SerializeField] Text TotalItemsOpened;
    [SerializeField] int TotalItemsCount;

    private bool itemCanBePutedOn;

    void Start()
    {
        GenerateItems(NextGenItem.ItemQuality.STANDARD);
        if (ItemsIDOnScreen.Count > 0)
        {
            SetTest();
        }
    }


    public void GenerateItems(NextGenItem.ItemQuality type)
    {
        TotalItemsCount = 0;
        ItemsIDOnScreen.Clear();
        ClearCards();
        foreach (NextGenItem item in LevelModel.ItemDataBase.getAllItems())
        {
            if (item.TotalItemSegment > 0)
            {
                TotalItemsCount += 1;
                if (item.itemItemQuality == type)
                {
                    ItemsIDOnScreen.Add(item.id);
                }
            }
          
        }

        TotalItemsOpened.text = "YOU OPEN " + TotalItemsCount.ToString() + " ITEMS!";

        if(ItemsIDOnScreen.Count == 0){
            weaponInfo.SetActive(false);
            NoWeapon.SetActive(true);
            return;
        }
        if(ItemsIDOnScreen.Count > 0){
            weaponInfo.SetActive(true);
            NoWeapon.SetActive(false);
        }

        foreach (int item in ItemsIDOnScreen)
        {
                GameObject card = Instantiate(itemPrefab, itemParent);
              card.GetComponent<ArsenalCard>().SetCard(LevelModel.ItemDataBase.GetItem(item));
        }
        CardScroll.BuildScroll();


    }

    private void ClearCards()
    {
        int childs = itemParent.childCount;
        if (childs > 0)
        {
            for (int i = childs - 1; i > -1; i--)
            {
                Destroy(itemParent.GetChild(i).gameObject);
            }
        }
    }

    public void SetTest()
    {
        itemCanBePutedOn = false;
        currentSelectedItem = LevelModel.ItemDataBase.GetItem(ItemsIDOnScreen[CardScroll.cardIndex]);
        shopItemLevel.text = "Level: " + currentSelectedItem.itemlevel;

        Stars[0].SetActive(false);
        Stars[1].SetActive(false);
        Stars[2].SetActive(false);
        Stars[3].SetActive(false);
        if (currentSelectedItem.Stars == NextGenItem.ItemStars.One)
        {
            Stars[0].SetActive(true);
            shopSlider.maxValue = 10;
            shopItemSegmentsNeeded.text = "SEGMENTS TO LEVELUP: " + (10 -currentSelectedItem.itemSegments);
        }
        if (currentSelectedItem.Stars == NextGenItem.ItemStars.Two)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            shopSlider.maxValue = 20;
            shopItemSegmentsNeeded.text = "SEGMENTS TO LEVELUP: " + (20 - currentSelectedItem.itemSegments);

        }
        if (currentSelectedItem.Stars == NextGenItem.ItemStars.Three)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            shopSlider.maxValue = 30;
            shopItemSegmentsNeeded.text = "SEGMENTS TO LEVELUP: " + (30 - currentSelectedItem.itemSegments);

        }
        if (currentSelectedItem.Stars == NextGenItem.ItemStars.Four)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            Stars[3].SetActive(true);
            shopSlider.maxValue = 40;
            shopItemSegmentsNeeded.text = "SEGMENTS TO LEVELUP: " + (40 - currentSelectedItem.itemSegments);

        }

        if (currentSelectedItem.itemType == NextGenItem.ItemType.Weapon)
        {
            EffectImage.sprite = DAM;
            shopItemEfectText.text = "Damage: " + currentSelectedItem.Damage;
        }
        if (currentSelectedItem.itemType == NextGenItem.ItemType.Shild)
        {
            EffectImage.sprite = DEF;
            shopItemEfectText.text = "Defance: " + currentSelectedItem.Defense;
        }
        if (currentSelectedItem.itemType == NextGenItem.ItemType.Runa)
        {
            EffectImage.sprite = MAG;
            shopItemEfectText.text = "Magic: " + currentSelectedItem.MagicForce;
        }

        if(PutOnButton != null){
            if(LevelModel.Player.Inventory.weapon == currentSelectedItem){
                PutOnButton.interactable = false;
                PutOnButtonText.text = "ALREADY IN USE";
            } else {
                PutOnButton.interactable = true;
                PutOnButtonText.text = "PUT ON";
            }

            foreach (CharecterClass item in LevelModel.Player.Inventory.weapon.BelongClasses)
            {
                foreach (CharecterClass item2 in currentSelectedItem.BelongClasses)
                {
                    if (item == item2)
                    {
                        itemCanBePutedOn = true;
                        Debug.Log("TRUE");
                    }
                }

            }

            if (itemCanBePutedOn == true)
            {
                PutOnButton.interactable = true;
                PutOnButtonText.text = "PUT ON";
            }
            if (itemCanBePutedOn == false)
            {
                PutOnButton.interactable = false;
                PutOnButtonText.text = "NOT FOR " + LevelModel.Player.charecter.Class.ClassName.ToUpper() + "S";
            }
        }

        shopSlider.value = currentSelectedItem.itemSegments;

    }

    public void PutItem()
    {
        LevelModel.Player.Inventory.PutOnItem(currentSelectedItem);
        NotificationController.show.Smash(currentSelectedItem.itemName.ToUpper() , currentSelectedItem.icon);
        SoundFX.play.PutOn();
    }


    public void ShowSimpleItems()
    {
        GenerateItems(NextGenItem.ItemQuality.STANDARD);
        SetTest();
        CardScroll.ActiveDisactiveScrollButtonsCheck();
        SoundFX.play.Clic();
    }
    public void ShowPowerfulItems()
    {
        GenerateItems(NextGenItem.ItemQuality.POWERFUL);
        SetTest();
        CardScroll.ActiveDisactiveScrollButtonsCheck();
        SoundFX.play.Clic();

    }
    public void ShowLegendaryItems()
    {
        GenerateItems(NextGenItem.ItemQuality.LEGENDARY);
        SetTest();
        CardScroll.ActiveDisactiveScrollButtonsCheck();
        SoundFX.play.Clic();

    }

   
}
