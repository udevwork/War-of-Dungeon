using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DataBase;

public class MenuView : MonoBehaviour
{

    public static MenuView instance;

    /// SETTINGS 

    [SerializeField] private Text MaxFloorText;
    [SerializeField] private Text musicValueText;
    [SerializeField] private Text SoundsEffectsValueText;
    [SerializeField] private Toggle useShadersToggle;
    [SerializeField] private Slider musicValueSlider;
    [SerializeField] private Slider SoundsEffectsValueSlider;
    [SerializeField] private UnityEngine.UI.Dropdown QualityDropDown;


    public void Awake()
    {
        
        instance = this;
        if (LevelModel.isSession == false)
        {
            if (LevelModel.difference.Minutes >= 10)
            {
                NotificationController.show.BlackNotifi("WELCOME!", "your revard: 120 coins");
                LevelModel.Player.Inventory.WarCoins += 120;
            }
        }
        LevelModel.isSession = true;
    }


 

    public void Start()
    {
        // SETTINGS START SET
        MaxFloorText.text = LevelModel.Player.Info.MaxFloor.ToString();
        musicValueText.text = Settings.MusicVolume.ToString();
        SoundsEffectsValueText.text = Settings.SoundEffectsVolume.ToString();
        useShadersToggle.isOn = Settings.UseShaders;
        musicValueSlider.value = Settings.MusicVolume;
        SoundsEffectsValueSlider.value = Settings.SoundEffectsVolume;
        QualityDropDown.value = Settings.quality;
    }


    public void SetQuality()
    {
        Settings.SetQuality(QualityDropDown.value);
    }

    public void SetSoundVolume()
    {
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

    public void OpenSmall(){
        RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Small, 130);
        SoundFX.play.PlayAlertOneSound();
    }

    public void OpenMedium()
    {
        RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Medium, 130);
        SoundFX.play.PlayAlertOneSound();

    }
    public void OpenLarge()
    {
        RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Large, 130);
        SoundFX.play.PlayAlertOneSound();

    }
    public void OpenEpic()
    {
        RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Epic, 130);
        SoundFX.play.PlayAlertOneSound();

    }


    public void CharectersChooseButton()
    {
        instance = null;
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.HEROES, "HEROES");

        SoundFX.play.PlayAlertTwoSound();
    }
    public void ClearAllSave()
    {
        PlayerPrefs.DeleteAll();
        NotificationController.show.BlackNotifi("WARNING", "ALL DATA IS CLEAR");
    }

    public void RestoreGameButton()
    {
        instance = null;
        SoundFX.play.PlayAlertTwoSound();
        LevelModel.Game.state = LevelModel.Game.State.Level;
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.DUNGEON, "DUNGEON");
    }

}
