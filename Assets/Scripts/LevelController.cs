using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DataBase;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public EnemysSet EnemysSet;
    public EnemysSet BossSet;

    public DungeonGenerator DG;

    public Action OnLevelComplete;
    public Action OnLevelStart;
    public Action OnBossLevelStart;

    public Action GenerationStart;
    public Action GenerationEnd;


    public bool isBossFight;

    private void Awake()
    {
        LevelModel.Dungeon.OnRoomsCountChenged += (int obj) => WinCheck(obj);
        instance = this;
    }

    private void Start()
    {
        LevelModel.Game.isFirstLounch = false;

        StartCoroutine("GenerateDangeon");

        if(LevelModel.Dungeon.Stage.currentDungeonCount == 0){
            Debug.Log("SAVE CURRENT FLOOR");
            Debug.Log("LevelModel.Dungeon.Stage.currentDungeonCount:" + LevelModel.Dungeon.Stage.currentDungeonCount);
            LevelModel.Dungeon.Stage.SaveStartRoom = LevelModel.Dungeon.curentFloor; 
        }
    }

    IEnumerator GenerateDangeon()
    {
        if (GenerationStart != null)
        {
            GenerationStart.Invoke();
        }
        if (LevelModel.Dungeon.levelToLoad == LevelModel.Dungeon.LevelType.BossLevel)
        {
            isBossFight = true;
            DG.BuildStandartDungeon();
        }
        else if (LevelModel.Dungeon.levelToLoad == LevelModel.Dungeon.LevelType.SimpleDungeon)
        {
            isBossFight = false;
            DG.BuildStandartDungeon();
        } 
        //if (LevelModel.Dungeon.levelToLoad == LevelModel.Dungeon.LevelType.Village)
       // {
        //    isBossFight = false;
        //    DG.BuildVillage();
       // }

        PlayerSpawn();

        yield return new WaitForSeconds(1);

        if (GenerationEnd != null)
        {
            GenerationEnd.Invoke();
        }
        if (OnLevelStart != null)
        {
            OnLevelStart.Invoke();
        }

        Debug.Log("| rooms count: " + LevelModel.Dungeon.RoomsCount+ " |");
    }

    public void PlayerSpawn()
    {
        GameObject g = Instantiate(LevelModel.Player.charecter.objectToSpawn);
        LevelModel.Player.MainHeroScript = g.GetComponent<MainHero>();
        LevelModel.Player.MainHeroScript.gameObject.transform.position = GameObject.Find("PlayerSpawnPoint").transform.position;
    }

   

    public void AddFloorCount()
    {
        LevelModel.Dungeon.curentFloor += 1;
        LevelModel.Player.Info.MaxFloor += 1;
    }

    public void GoToNextLevel()
    {
        LevelModel.Dungeon.ClearDungeonStaff();
        LevelModel.ClearListners();
        AddFloorCount();
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.DUNGEON, "DUNGEON");
    }

    public void ExitToMainMenuButton()
    {
        LevelModel.Game.state = LevelModel.Game.State.Menu;
        LevelModel.Dungeon.ClearDungeonStaff();
        LevelModel.Dungeon.Stage.currentDungeonCount = 0;
        LevelModel.Dungeon.Stage.TotalClearRoomsDungeon = 0;
        LevelModel.ClearListners();
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.MAINMENU, "MAIN MENU");
    }

    public void GoToVillage()
    {
        LevelModel.Game.state = LevelModel.Game.State.Menu;
        LevelModel.Dungeon.ClearDungeonStaff();
        LevelModel.Dungeon.Stage.currentDungeonCount = 0;
        LevelModel.Dungeon.Stage.TotalClearRoomsDungeon = 0;
        LevelModel.ClearListners();
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.VILLAGE, "VILLAGE");
    }

    public void AddEnemy(NPC npc)
    {
        LevelModel.Dungeon.AddEnemy(npc);
    }
    public void RemoveEnemy(NPC npc)
    {
        LevelModel.Dungeon.RemoveEnemy(npc);
    }

    public void WinCheck(int count){
        if(count == 0){
            if (OnLevelComplete != null)
            {
                OnLevelComplete.Invoke();
                AddSegments(UnityEngine.Random.Range(2, 30));
            }
        }
    }
    public void AddCoinsToPlayer(int val)
    {
        LevelModel.Player.Inventory.WarCoins += val;
    }
    public void AddSkullsToPlayer(int val)
    {
        LevelModel.Player.Inventory.TrophySkulls += val;
    }
    public void AddCrystalsToPlayer(int val)
    {
        LevelModel.Player.Inventory.Crystals += val;
    }
    public void AddExpToPlayer(int val)
    {
        LevelModel.Player.Experience.AddExperience(val);
    }
    public void AddSegments(int val)
    {
        val += 1;
    }

}
