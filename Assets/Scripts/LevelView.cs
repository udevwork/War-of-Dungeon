using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;

public class LevelView : MonoBehaviour
{

    // FILDS FOR UI
    [SerializeField] private Text CurrentFloorText;
    [SerializeField] private Text NextFloorText;


    [SerializeField] private Text firstCircleText;
    [SerializeField] private Text SecontCircleText;
    [SerializeField] private Slider StageSlider;



    [SerializeField] private Text MoneyText;
    [SerializeField] private Text PlatinumText;
    [SerializeField] private Text GemText;

    [SerializeField] private Image PlayerIcon;

    [SerializeField] private GameObject LevelCompleteWindow;

    [SerializeField] private HealthIndicatorPointer PlayerHPindicator;
    [SerializeField] private HealthIndicatorPointer PlayerXPindicator;


    [SerializeField] private Text playerLevel;



    [SerializeField] Animation enemyAvatarAnimation;
    [SerializeField] private HealthIndicatorPointer EnemyHPindicator;

    [SerializeField] private Animation money_anim, platinum_anim, gem_anim;

    [SerializeField] private GameObject LoseCanvas;

    /// SETTINGS 

    [SerializeField] private Text musicValueText;
    [SerializeField] private Text SoundsEffectsValueText;
    [SerializeField] private Toggle useShadersToggle;
    [SerializeField] private Slider musicValueSlider;
    [SerializeField] private Slider SoundsEffectsValueSlider;
    [SerializeField] private UnityEngine.UI.Dropdown QualityDropDown;

    private GameObject pl_wep;
    private GameObject pl_shi;
    private GameObject pl_run;



    public Transform DaddysParent;
    public Transform DaddysWeapon;
    public Transform DaddysRuna;
    public Transform DaddysShild;

    [SerializeField] private Text ReloadTimeCounter;


    // ИНВЕНТАРЬ

    // Кнопки спэлов
    [SerializeField] private SpellFillAmount spell_1, spell_2, spell_3;
    [SerializeField] private Button HitButton;

    private void Awake()
    {
        LevelModel.Dungeon.Stage.TotalClearRoomsDungeonChenged += (int obj) => StageSliderContoll(obj);


        LevelModel.Player.Inventory.OnWarCoinsCountChange += (int obj) => SetMoneyText(obj); 
        LevelModel.Player.Inventory.OnTrophySkullsCountChange += (int obj) => SetPlatinumText(obj);
        LevelModel.Player.Inventory.OnCrystalsCountChange += (int obj) => SetGemText(obj); 


        LevelModel.Player.Inventory.OnWeaponPutOn += (NextGenItem obj) => SetWearedWeapon(obj);
        LevelModel.Player.Inventory.OnShildPutOn += (NextGenItem obj) => SetWearedShild(obj);
        LevelModel.Player.Inventory.OnRunaPutOn += (NextGenItem obj) => SetWearedRuna(obj);

        LevelModel.ItemDataBase.OnSegmentAdded += (NextGenItem arg1, int arg2) => SegmentAdded(arg1,arg2);
        LevelModel.Player.OnHealthChange += SetIndicators;
        LevelModel.Player.Experience.OnExperienceAdd += SetIndicators;

        LevelModel.Player.OnLevelChange += SetIndicators;
        LevelModel.Dungeon.OnEnemyDamaged += (NPC obj, float dmg) => SomeoneGetHit(obj, dmg);

        LevelModel.Player.Stats.OnStatsChange += SetIndicators;
        LevelModel.Player.OnLevelChange += SetIndicators;
        LevelModel.Player.OnPlayerDead += PlayerDeadBehavior;
        LevelController.instance.OnLevelComplete += LevelCompleted;
        LevelController.instance.OnLevelStart += LevelStart;
        SceneLoadingManager.load.OnAnimationComplete += delegate {
            if (LevelModel.Dungeon.levelToLoad == LevelModel.Dungeon.LevelType.BossLevel)
            {
                NotificationController.show.SmokeEffect("BOSS FIGHT");
            }
            else if (LevelModel.Dungeon.levelToLoad == LevelModel.Dungeon.LevelType.SimpleDungeon)
            {
                NotificationController.show.SmokeEffect("FLOOR " + LevelModel.Dungeon.curentFloor);
            }
        
        };

        GameObject daddymodel =  Instantiate(LevelModel.Player.heroCard.CallMeDaddyModel, DaddysParent);
        if (LevelModel.Player.heroCard.ChoosedMaterial)
        {
            daddymodel.GetComponentInChildren<Renderer>().material = LevelModel.Player.heroCard.ChoosedMaterial;
        }
    }

    public void Start()
    {

        PlayerIcon.sprite = LevelModel.Player.heroCard.CharIcon;


        HitButton.onClick.AddListener(LevelModel.Player.MainHeroScript.Hit);

        spell_1.btn.onClick.AddListener( delegate { LevelModel.Player.MainHeroScript.Spell(0); });
        spell_2.btn.onClick.AddListener( delegate { LevelModel.Player.MainHeroScript.Spell(1); });
        spell_3.btn.onClick.AddListener( delegate { LevelModel.Player.MainHeroScript.Spell(2); });


        spell_1.Timer = LevelModel.Player.charecter.Skill_One.ReloadTime;
        spell_2.Timer = LevelModel.Player.charecter.Skill_Two.ReloadTime;
        spell_3.Timer = LevelModel.Player.charecter.Skill_Three.ReloadTime;

        spell_1.FillImage.sprite = LevelModel.Player.charecter.Skill_One.SpellIcon;
        spell_2.FillImage.sprite = LevelModel.Player.charecter.Skill_Two.SpellIcon;
        spell_3.FillImage.sprite = LevelModel.Player.charecter.Skill_Three.SpellIcon;

        CurrentFloorText.text = LevelModel.Dungeon.curentFloor.ToString();
        NextFloorText.text = (LevelModel.Dungeon.curentFloor + 1).ToString();

        if(LevelController.instance.isBossFight){
            CurrentFloorText.text = "BOSS";
        } 
        if ((LevelModel.Dungeon.Stage.currentDungeonCount + 1) == LevelModel.Dungeon.Stage.FinalDungeon) {
            NextFloorText.text = "BOSS";
        } 


        if (LevelModel.Player.Inventory.weapon)
        {
            SetWearedWeapon(LevelModel.Player.Inventory.weapon);
        }
        else
        {
            Debug.LogWarning("NO WEAPON");
        }
        if (LevelModel.Player.Inventory.shild)
        {
            SetWearedShild(LevelModel.Player.Inventory.shild);
        }
        else
        {
            Debug.LogWarning("NO SHILD");
        }
        if (LevelModel.Player.Inventory.runa)
        {
            SetWearedRuna(LevelModel.Player.Inventory.runa);
        }
        else
        {
            Debug.LogWarning("NO RUNA");
        }

     

        LevelModel.Player.Health = LevelModel.Player.MaxHealth;


        SetMoneyText(LevelModel.Player.Inventory.WarCoins);
        SetPlatinumText(LevelModel.Player.Inventory.TrophySkulls);
        SetGemText(LevelModel.Player.Inventory.Crystals);
        SetIndicators();

        // SETTINGS START SET
        musicValueText.text = Settings.MusicVolume.ToString();
        SoundsEffectsValueText.text = Settings.SoundEffectsVolume.ToString();
        useShadersToggle.isOn =  Settings.UseShaders;
        musicValueSlider.value = Settings.MusicVolume;
        SoundsEffectsValueSlider.value = Settings.SoundEffectsVolume;
        QualityDropDown.value = Settings.quality;

        StageSliderContoll(LevelModel.Dungeon.Stage.TotalClearRoomsDungeon);

    }

    public void LevelStart()   // USE FOT INITIALIZE ALL TEXT FILDS ETC. // CAN BE INIT IN MODEL BY INVOKED EVENT!!!
    {
        LevelCompleteWindow.SetActive(false);
        SetIndicators();
        SoundFX.play.startSound();
    }


    public void StageSliderContoll(int x){
        Debug.Log("4444444");
        StageSlider.minValue = 0;
        StageSlider.maxValue = LevelModel.Dungeon.Stage.TotalRoomsDungeon;
        StageSlider.value = x;
        firstCircleText.text = LevelModel.Dungeon.Stage.SaveStartRoom.ToString();
        SecontCircleText.text = (LevelModel.Dungeon.Stage.SaveStartRoom + 2).ToString();
    }

    public void LevelCompleted()
    {
        LevelCompleteWindow.SetActive(true);
        PlayerGameCenter.PostScoreOnLeaderBoard(LevelModel.Player.Inventory.TrophySkulls);
        SoundFX.play.playerWin();
    }

    public void GoToNextLevel()
    {
        LevelController.instance.GoToNextLevel();
    }
    public void OpenChest()
    {
        if (LevelController.instance.isBossFight)
        {
            RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Large, Random.Range(1,130));
        }
        else
        {
            RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Small, Random.Range(1, 130));
        }
    }

    private void SetMoneyText(int v)
    {
        MoneyText.text = v.ToString();
        money_anim.Play();
    }

    private void SetPlatinumText(int v)
    {
        PlatinumText.text = v.ToString();
        platinum_anim.Play();
    }

    private void SetGemText(int v)
    {
        GemText.text = v.ToString();
        gem_anim.Play();
    }

    public void SegmentAdded(NextGenItem item, int segments)
    {

    }


    private void SetWearedWeapon(NextGenItem v)
    {
        if(pl_wep != null){
            Destroy(pl_wep);
        }

        if (v.isForLeftHand == false)
        {
            Vector3 p = LevelModel.Player.MainHeroScript.WepPos.position;
            Quaternion q = v.objectToSpawn.transform.rotation;
            Transform c = LevelModel.Player.MainHeroScript.WepPos;

            pl_wep = Instantiate(v.objectToSpawn, p, q, c);

            // SPLASH EFFECT
            p = LevelModel.Player.MainHeroScript.gameObject.transform.position;
            q = Quaternion.identity;
            c = LevelModel.Player.MainHeroScript.gameObject.transform;

            GameObject Splash = Instantiate(v.SplashEffect, p, q, c);
            LevelModel.Player.MainHeroScript.swordSplash.SplashFX = Splash.GetComponent<ParticleSystem>();
        }
        if(v.isForLeftHand == true){
            Transform  c = LevelModel.Player.MainHeroScript.BowPos;
            pl_wep = Instantiate(v.objectToSpawn);
            pl_wep.transform.SetParent(c);
            pl_wep.transform.localPosition = Vector3.zero;
            pl_wep.transform.localRotation = Quaternion.Euler(0, 0, 0);
            pl_wep.transform.localScale = Vector3.one;
        } 

    }
    private void SetWearedShild(NextGenItem s)
    {
        if (pl_shi != null)
        {
            Destroy(pl_shi);
        }
        Vector3 p = LevelModel.Player.MainHeroScript.ShildPos.position;
        Quaternion q = s.objectToSpawn.transform.rotation;
        Transform  c = LevelModel.Player.MainHeroScript.ShildPos;

        if (LevelModel.Player.charecter.Class.IsShildOnBack == true)
        {
            p = LevelModel.Player.MainHeroScript.ShildPosBack.position;
            c = LevelModel.Player.MainHeroScript.ShildPosBack;
        }

        pl_shi = Instantiate(s.objectToSpawn, p, q, c);
    }
    private void SetWearedRuna(NextGenItem r)
    {

        if (pl_run != null)
        {
            Destroy(pl_run);
        }
        Vector3 p = LevelModel.Player.MainHeroScript.RunaPos.position;
        Quaternion q = Quaternion.identity;
        Transform c = LevelModel.Player.MainHeroScript.RunaPos;
        pl_run = Instantiate(r.objectToSpawn, p, q, c);
    }


    public void SomeoneGetHit(NPC npc,float dmg)  // FOR HEALTHPINTS INDICATOR
    {
        enemyAvatarAnimation.Rewind();
        enemyAvatarAnimation.Play();
        EnemyHPindicator.SetMax(npc.maxHealth);
        EnemyHPindicator.SetCurrent(npc.health);
    }

    public void SetIndicators()  // FOR HEALTHPOINTS INDICATOR
    {
        PlayerHPindicator.SetMax(LevelModel.Player.MaxHealth);
        PlayerHPindicator.SetCurrent(LevelModel.Player.Health);

        PlayerXPindicator.SetMax(LevelModel.Player.Experience.ExperienceRequired);
        PlayerXPindicator.SetCurrent(LevelModel.Player.Experience.CurrentExperience);

        playerLevel.text = LevelModel.Player.Level.ToString();

    }

    public void PlayerDeadBehavior()
    {
        LevelModel.Dungeon.ClearDungeonStaff();
        LevelModel.ClearListners();
        StartCoroutine("delaylevelreload");
    }

    IEnumerator delaylevelreload()
    {
        yield return new WaitForSeconds(3f);
        LoseCanvas.SetActive(true);
        ReloadTimeCounter.text = "SPAWN IN VILLAGE... 3";
        yield return new WaitForSeconds(1f);
        ReloadTimeCounter.text = "SPAWN IN VILLAGE... 2";
        yield return new WaitForSeconds(1f);
        ReloadTimeCounter.text = "SPAWN IN VILLAGE... 1";
        yield return new WaitForSeconds(1f);
        LevelController.instance.GoToVillage();
    }


    public void ExitToMainMenuButton()
    {
        LevelController.instance.ExitToMainMenuButton();
    }


    // SETTINGS START
    public void SetQuality()
    {
        Settings.SetQuality(QualityDropDown.value);
    }

    public void SetSoundVolume(){
        Settings.SoundEffectsVolume = SoundsEffectsValueSlider.value;
        SoundsEffectsValueText.text = SoundsEffectsValueSlider.value.ToString();
    }

    public void SetMusicVolume()
    {
        Settings.MusicVolume = musicValueSlider.value;
        musicValueText.text = musicValueSlider.value.ToString();
    }

    public void SetUseShaders()
    {
        Settings.UseShaders = useShadersToggle.isOn;
    }

    public void ShowLeaderbord()
    {
        Social.ShowLeaderboardUI();
    }

    public void ClearAllSave(){
        PlayerPrefs.DeleteAll();
    }
    // SETTINGS END


}
