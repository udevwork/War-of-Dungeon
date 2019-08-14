// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;
using System.Linq;

public class CharecterChooseView : MonoBehaviour {

    public static CharecterChooseView instance;

    public List<HeroManiMenuCard> HeroCards;
    public List<Renderer> charectersMaterials;
    public List<CharecterChooseButton> chooseButtons;
    public List<GameObject> lockImage;

    public GameObject SkinSelector;

    public Material BlockedCharecter;

    [SerializeField] private Image icon;
    [SerializeField] private Text text;
    [SerializeField] private Animator startWindowAnim;


    [SerializeField] private Image ClassOne_icon;
    [SerializeField] private Image ClassTwo_icon;
    [SerializeField] private Image ClassThree_icon;
    [SerializeField] private Image ClassFour_icon;

    [SerializeField] private Image StartClassOne_icon;
    [SerializeField] private Image StartClassTwo_icon;
    [SerializeField] private Image StartClassThree_icon;
    [SerializeField] private Image StartClassFour_icon;

    [SerializeField] private Text StartClassOne_Text;
    [SerializeField] private Text StartClassTwo_Text;
    [SerializeField] private Text StartClassThree_Text;
    [SerializeField] private Text StartClassFour_Text;

    [SerializeField] private Text StartClassDescription;

    [SerializeField] private Button StartBattleButton;

    [SerializeField] private Text IntText;
    [SerializeField] private Text StrText;
    [SerializeField] private Text DexText;
    [SerializeField] private Text VitText;


    private int skinIndex;
    public void Awake()
    {
        instance = this;
    }

     void Start()
    {
        CoolScrol.instance.OnCardIndexChange += SetHeroStatsAndSpell;   
        SetHeroStatsAndSpell();
        for (int i = 0; i < HeroCards.Count; i++)
        {
            if(HeroCards[i].isCharecterUnlocked == false){
                charectersMaterials[i].material = BlockedCharecter;
                chooseButtons[i].IsClose = true;
                lockImage[i].SetActive(true);
            }
        }
    }

    public void SetHeroStatsAndSpell(){

        ClassOne_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassOne.ClassIcon;
        ClassTwo_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassTwo.ClassIcon;
        ClassThree_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassThree.ClassIcon;
        ClassFour_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassFour.ClassIcon;

        IntText.text = HeroCards[CoolScrol.instance.cardIndex].charecter.Intelligence.ToString();
        StrText.text = HeroCards[CoolScrol.instance.cardIndex].charecter.Strength.ToString();
        DexText.text = HeroCards[CoolScrol.instance.cardIndex].charecter.Dexterity.ToString();
        VitText.text = HeroCards[CoolScrol.instance.cardIndex].charecter.Vitality.ToString();
        if(HeroCards[CoolScrol.instance.cardIndex].isCharecterUnlocked){
            SkinSelector.SetActive(true);
        } else {
            SkinSelector.SetActive(false);
        }

        skinIndex = 0;
    }

    public void OpenWindow(HeroManiMenuCard info){
        SoundFX.play.Clic();
        StartClassOne_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassOne.ClassIcon;
        StartClassTwo_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassTwo.ClassIcon;
        StartClassThree_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassThree.ClassIcon;
        StartClassFour_icon.sprite = HeroCards[CoolScrol.instance.cardIndex].ClassFour.ClassIcon;

        StartClassOne_Text.text = HeroCards[CoolScrol.instance.cardIndex].ClassOne.ClassName;
        StartClassTwo_Text.text = HeroCards[CoolScrol.instance.cardIndex].ClassTwo.ClassName;
        StartClassThree_Text.text = HeroCards[CoolScrol.instance.cardIndex].ClassThree.ClassName;
        StartClassFour_Text.text = HeroCards[CoolScrol.instance.cardIndex].ClassFour.ClassName;

        icon.sprite = info.CharIcon;
        text.text = info.CharName;

        StartBattleButton.interactable = false;
        startWindowAnim.Play("startwindowOpen");
    }

    public void ClassOneChoose(){
        StartClassDescription.text = HeroCards[CoolScrol.instance.cardIndex].ClassOne.ClassDecription;
        LevelModel.Player.charecter.Class = HeroCards[CoolScrol.instance.cardIndex].ClassOne;
        StartBattleButton.interactable = true;
    }
    public void ClassTwoChoose()
    {
        StartClassDescription.text = HeroCards[CoolScrol.instance.cardIndex].ClassTwo.ClassDecription;
        LevelModel.Player.charecter.Class = HeroCards[CoolScrol.instance.cardIndex].ClassTwo;
        StartBattleButton.interactable = true;
    }
    public void ClassThreeChoose()
    {
        StartClassDescription.text = HeroCards[CoolScrol.instance.cardIndex].ClassThree.ClassDecription;
        LevelModel.Player.charecter.Class = HeroCards[CoolScrol.instance.cardIndex].ClassThree;
        StartBattleButton.interactable = true;
    }
    public void ClassFourChoose()
    {
        StartClassDescription.text = HeroCards[CoolScrol.instance.cardIndex].ClassFour.ClassDecription;
        LevelModel.Player.charecter.Class = HeroCards[CoolScrol.instance.cardIndex].ClassFour;
        StartBattleButton.interactable = true;
    }



    public void CloseWindow()
    {
        SoundFX.play.Clic();
        startWindowAnim.Play("startWindowClose");
    }


    public void PlayButton()
    {
        SoundFX.play.PlayAlertTwoSound();
        LevelModel.Game.state = LevelModel.Game.State.Level;
        instance = null;
        LevelModel.Player.ApplyPlayerInfo();
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.DUNGEON,"DUNGEON");
    }


    public void ExitToMainMenuButton()
    {
        SoundFX.play.PlayAlertTwoSound();
        instance = null;
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.MAINMENU, "MAIN MENU");
    }

    public void BackToMenuButton()
    {
        SoundFX.play.Clic();
        instance = null;
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.MAINMENU, "MAIN MENU");
    }

    public void SkinChooseLeft(){
        Debug.Log("LOH: " + skinIndex);
        if ((skinIndex - 1) == -1)
        {
            skinIndex = HeroCards[CoolScrol.instance.cardIndex].skins.Length -1;
            ApplaySkin(charectersMaterials[CoolScrol.instance.cardIndex], HeroCards[CoolScrol.instance.cardIndex].skins[skinIndex]);
        }
        else
        {
            skinIndex -= 1;
            ApplaySkin(charectersMaterials[CoolScrol.instance.cardIndex], HeroCards[CoolScrol.instance.cardIndex].skins[skinIndex]);
        }

    }

    public void SkinChooseRight()
    {
        Debug.Log("LOH: " + skinIndex);
        if ((skinIndex + 1) == HeroCards[CoolScrol.instance.cardIndex].skins.Length)
        {
            skinIndex = 0;
            ApplaySkin(charectersMaterials[CoolScrol.instance.cardIndex], HeroCards[CoolScrol.instance.cardIndex].skins[skinIndex]);
        }
        else
        {
            skinIndex += 1;
            ApplaySkin(charectersMaterials[CoolScrol.instance.cardIndex], HeroCards[CoolScrol.instance.cardIndex].skins[skinIndex]);
        }
    }

    public void ApplaySkin(Renderer render, Material skin){
        render.material = skin;
        HeroCards[CoolScrol.instance.cardIndex].ChoosedMaterial = skin;
    }

}
