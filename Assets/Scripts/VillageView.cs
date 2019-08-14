// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using DataBase;

public class VillageView : MonoBehaviour {
    

    public void GoToDungeon()
    {
        SoundFX.play.PlayAlertTwoSound();
        LevelModel.Game.state = LevelModel.Game.State.Level;
        LevelModel.ClearListners();
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.DUNGEON, "DUNGEON");
    }

   

    public void ShowAd(string type)
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback += (ShowResult obj) => HandleShowResult(obj, type);
        Advertisement.Show("rewardedVideo", options);
    }

    void HandleShowResult(ShowResult result, string type)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            if (type == "money")
            {
                NotificationController.show.BlackNotifi("THANKS!", "YOU GET A REVARD");
                LevelController.instance.AddCoinsToPlayer(1000);
            }
            if (type == "chest")
            {
                NotificationController.show.BlackNotifi("THANKS!", "YOU GET A CHEST");
                RewardChestBehavior.instance.Reward(RewardChestBehavior.ChestType.Small, 20);
            }
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
