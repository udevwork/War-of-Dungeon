using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Advertisements;
using UnityEngine.iOS;

namespace DataBase
{
    public class LevelModel : MonoBehaviour
    {
        public GameSaveManager SaveManager = new GameSaveManager();

        public static class Dungeon
        {
            public enum LevelType
            {
                SimpleDungeon, BossLevel
            }

            public static LevelType levelToLoad = LevelType.SimpleDungeon;

            public static Action<int> OnRoomsCountChenged;
            private static int _RoomsCount;
            public static int RoomsCount
            {
                get { return _RoomsCount; }
                set
                {
                    _RoomsCount = value;
                    OnRoomsCountChenged.Invoke(_RoomsCount);
                }
            }

            private static int _CurentFloor = 1;
            public static int curentFloor
            {
                get { return _CurentFloor; }
                set
                {
                    _CurentFloor = value;
                    Stage.currentDungeonCount += 1;
                    Stage.BossLevelCheker();
                }
            }



            public static Action<NPC, float> OnEnemyDamaged;

            public static void AddEnemy(NPC npc)
            {
                enemyList.Add(npc);

            }
            public static void RemoveEnemy(NPC npc)
            {
                enemyList.Remove(npc);

            }

            public static NPC GetEnemyById(int id)
            {
                return enemyList[id];
            }

            public static void ClearDungeonStaff()
            {
                enemyList.Clear();
                _RoomsCount = 0;
            }

            static List<NPC> enemyList = new List<NPC>();
            public static int EnemyCount
            {
                get { return enemyList.Count; }
            }



            public class Stage{
                
                public static int[] stageRoomsCount = { 3, 4, 5, 6, 7 };
                public static readonly int FinalDungeon = 4;
                public static int currentDungeonCount;
                public static readonly int TotalRoomsDungeon = 25;

                private static int _TotalClearRoomsDungeon;
                public static Action<int> TotalClearRoomsDungeonChenged;
                public static int TotalClearRoomsDungeon
                {
                    get { return _TotalClearRoomsDungeon; }
                    set
                    {
                        _TotalClearRoomsDungeon = value;
                        if (TotalClearRoomsDungeonChenged != null)
                        {
                            TotalClearRoomsDungeonChenged.Invoke(_TotalClearRoomsDungeon);
                        }
                    }
                }

                public static int SaveStartRoom;

                public static void BossLevelCheker()
                {
                    if (currentDungeonCount == FinalDungeon)
                    {

                        currentDungeonCount = 0;
                        TotalClearRoomsDungeon = 0;
                        levelToLoad = LevelType.BossLevel;
                    }
                    else
                    {
                        levelToLoad = LevelType.SimpleDungeon;
                    }
                }
            }

        }


        public static class Player
        {
            private static string _PlayerName = "%username%";
            public static string playerName { get { return _PlayerName; } set { _PlayerName = value; } }

            public static Charecter charecter;
            public static HeroManiMenuCard heroCard;
            public static MainHero MainHeroScript;
            public static Action OnLevelChange;
            public static Action OnHealthChange;
            public static Action OnPlayerDead;
            public static bool isdead;
            private static int _Level = 1;
            public static int Level
            {
                get { return _Level; }
                set
                {
                    _Level = value;
                    CharecterOpeningBehavior.CheckForUnlockByLevel(value);
                    if (OnLevelChange != null)
                    {
                        OnLevelChange.Invoke();
                    }
                }
            }

            private static float _Health;
            public static float Health
            {
                get { return _Health + Add_Health; }
                set
                {
                    _Health = value;

                    if (_Health <= 0)
                    {
                        if (OnPlayerDead != null)
                        {
                            OnPlayerDead.Invoke();
                        }
                    }

                    if (OnHealthChange != null)
                    {
                        OnHealthChange.Invoke();
                    }
                }
            }


            public static float MaxHealth { get { return CalculateHealth(); } } // максимальное HР, не текущее

            public static float Defense { get { return CalculateDefense() + Add_Defense; } }

            public static float Damage { get { return CalculateDamage() + Add_Damage; } }

            public static float SkillDamage { get { return CalculateSkillDamage() + Add_SkillDamage; } }

            public static float CriticalDamage { get { return CalculateCriticalDamage(); } }

            public static float CriticalChanse { get { return CalculateCriticalHitChanse(); } }

            public static float Add_Health, Add_Damage, Add_Defense, Add_SkillDamage;


            public static class Stats
            {
                public static Action OnStatsChange;

                private static int _Intelligence;
                public static int Intelligence
                {
                    get { return _Intelligence; }
                    set { _Intelligence = value; if (OnStatsChange != null) { OnStatsChange.Invoke(); } }
                }

                private static int _Dexterity;
                public static int Dexterity
                {
                    get { return _Dexterity; }
                    set { _Dexterity = value; if (OnStatsChange != null) { OnStatsChange.Invoke(); } }
                }


                private static int _Strength;
                public static int Strength
                {
                    get { return _Strength; }
                    set { _Strength = value; if (OnStatsChange != null) { OnStatsChange.Invoke(); } }
                }

            
                private static int _Vitality;
                public static int Vitality
                {
                    get { return _Vitality; }
                    set { _Vitality = value; if (OnStatsChange != null) { OnStatsChange.Invoke(); } }
                }

            }


            public static class Experience
            {
                // EXPERIENCE
                public static Action OnExperienceAdd;
                readonly private static float expMultiplayer = 1.7f;

                private static float _ExperienceRequired = 100; //достичь какого опыта, что бы левелапнуться
                public static float ExperienceRequired { get { return _ExperienceRequired; } set { _ExperienceRequired = value; } }

                private static float _CurrentExperience;
                public static float CurrentExperience { get { return _CurrentExperience; } set { _CurrentExperience = value; } }

                public static int StatsPointEvalable = 5;

                public static int SkillsPointEvalable = 1;


                public static void AddExperience(float val)
                {
                    float pointsNeed = ExperienceRequired - CurrentExperience;
                    if (val > pointsNeed)
                    {
                        float tosave = val - pointsNeed;
                        CurrentExperience = 0;
                        Level += 1;
                        StatsPointEvalable += 5;
                        SkillsPointEvalable += 1;
                        Health = MaxHealth;
                        ExperienceRequired = (ExperienceRequired * expMultiplayer);
                        AddExperience(val);
                    }
                    else if (val < pointsNeed)
                    {
                        CurrentExperience += val;
                    }
                    else if (val == pointsNeed)
                    {
                        CurrentExperience = 0;
                        Level += 1;
                        StatsPointEvalable += 5;
                        SkillsPointEvalable += 1;
                        Health = MaxHealth;
                        ExperienceRequired = (ExperienceRequired * expMultiplayer);
                    }
                    if (OnExperienceAdd != null)
                    {
                        OnExperienceAdd.Invoke();
                    }
                }
            }

            public static class Inventory
            {

                public static NextGenItem weapon;
                public static Action<NextGenItem> OnWeaponPutOn;

                public static NextGenItem shild;
                public static Action<NextGenItem> OnShildPutOn;

                public static NextGenItem runa;
                public static Action<NextGenItem> OnRunaPutOn;

                public static Action<int> OnWarCoinsCountChange;
                private static int _WarCoins;
                /// <summary>
                ///  Игровая валюта
                /// </summary>
                /// <value> Игровая валюта</value>
                public static int WarCoins
                {
                    get { return _WarCoins; }
                    set
                    {
                        _WarCoins = value;
                        if (OnWarCoinsCountChange != null)
                        {
                            OnWarCoinsCountChange.Invoke(_WarCoins);
                        }
                    }
                }

                public static Action<int> OnTrophySkullsCountChange;
                private static int _TrophySkulls;
                /// <summary>
                /// полу донат валюта
                /// </summary>
                /// <value>// полу донат валюта</value>
                public static int TrophySkulls
                {
                    get { return _TrophySkulls; }
                    set
                    {
                        _TrophySkulls = value;
                        if (OnTrophySkullsCountChange != null)
                        {
                           OnTrophySkullsCountChange.Invoke(_TrophySkulls);
                        }
                    }
                }

                public static Action<int> OnCrystalsCountChange;
                private static int _Crystals;
                /// <summary>
                ///  Донат валюта
                /// </summary>
                /// <value> Донат валюта</value>
                public static int Crystals
                {
                    get { return _Crystals; }
                    set
                    {
                        _Crystals = value;
                        if (OnCrystalsCountChange != null)
                        {
                           OnCrystalsCountChange.Invoke(_Crystals);
                        }
                    }
                }

                public static Action<int> OnHealthRemedyCountChange;
                private static int _HealthRemedy;
                /// <summary>
                ///  Донат валюта
                /// </summary>
                /// <value> Донат валюта</value>
                public static int HealthRemedy
                {
                    get { return _HealthRemedy; }
                    set
                    {
                        _HealthRemedy = value;
                        if (OnHealthRemedyCountChange != null)
                        {
                            OnHealthRemedyCountChange.Invoke(_HealthRemedy);
                        }
                    }
                }

                public static Action<int> OnSmallCestCountChange;
                private static int _SmallCest;
                /// <summary>
                ///  Донат валюта
                /// </summary>
                /// <value> Донат валюта</value>
                public static int SmallCest
                {
                    get { return _SmallCest; }
                    set
                    {
                        _SmallCest = value;
                        if (OnSmallCestCountChange != null)
                        {
                            OnSmallCestCountChange.Invoke(_SmallCest);
                        }
                    }
                }

                public static Action<int> OnMediumCestCountChange;
                private static int _MediumCest;
                /// <summary>
                ///  Донат валюта
                /// </summary>
                /// <value> Донат валюта</value>
                public static int MediumCest
                {
                    get { return _MediumCest; }
                    set
                    {
                        _MediumCest = value;
                        if (OnMediumCestCountChange != null)
                        {
                            OnMediumCestCountChange.Invoke(_MediumCest);
                        }
                    }
                }

                public static Action<int> OnLargeCestCountChange;
                private static int _LargeCest;
                /// <summary>
                ///  Донат валюта
                /// </summary>
                /// <value> Донат валюта</value>
                public static int LargeCest
                {
                    get { return _LargeCest; }
                    set
                    {
                        _LargeCest = value;
                        if (OnLargeCestCountChange != null)
                        {
                            OnLargeCestCountChange.Invoke(_LargeCest);
                        }
                    }
                }

                public static Action<int> OnEpicCestCountChange;
                private static int _EpicCest;
                /// <summary>
                ///  Донат валюта
                /// </summary>
                /// <value> Донат валюта</value>
                public static int EpicCest
                {
                    get { return _EpicCest; }
                    set
                    {
                        _EpicCest = value;
                        if (OnEpicCestCountChange != null)
                        {
                            OnEpicCestCountChange.Invoke(_EpicCest);
                        }
                    }
                }


                public static void PutOnItem(NextGenItem item)
                {
                    if (item.itemType == NextGenItem.ItemType.Weapon)
                    {
                        weapon = item;
                        OnWeaponPutOn.Invoke(weapon);
                    }
                    if (item.itemType == NextGenItem.ItemType.Shild)
                    {
                        shild = item;
                        OnShildPutOn.Invoke(shild);
                    }
                    if (item.itemType == NextGenItem.ItemType.Runa)
                    {
                        runa = item;
                        OnRunaPutOn.Invoke(runa);
                    }

                }

            }


            public static class Info
            {
  
                private static int _MaxFloor;
                public static int MaxFloor
                {
                    get { return _MaxFloor; }
                    set
                    {
                        _MaxFloor = value;
                        CharecterOpeningBehavior.CheckForUnlockByFloor(value);
                    }
                }

                private static int _totalKills;
                public static int totalKills
                {
                    get { return _totalKills; }
                    set
                    {
                        _totalKills = value;
                        CharecterOpeningBehavior.CheckForUnlockByKills(value);
                    }
                }

            }

            public static void ApplyPlayerInfo()
            {
                _Health = charecter.Health;
                if (Game.isFirstLounch == true)
                {
                    Stats.Vitality = charecter.Vitality;
                    Stats.Dexterity = charecter.Dexterity;
                    Stats.Intelligence = charecter.Intelligence;
                    Stats.Strength = charecter.Strength;
                    Inventory.weapon = charecter.Class.DefoultWeapon;
                    Inventory.shild = null;
                    Inventory.runa = null;
                }
                else if (Game.isFirstLounch == false)
                {
                    Inventory.weapon = charecter.Class.DefoultWeapon;
                    Inventory.shild = null;
                    Inventory.runa = null;
                }
            }


        }

        public static class ItemDataBase
        {
            private static List<NextGenItem> AllItems;
            private static Dictionary<int, NextGenItem> dict = new Dictionary<int, NextGenItem>();
            // RANGE BY QUALITY
            private static List<NextGenItem> SimpleItems = new List<NextGenItem>();
            private static List<NextGenItem> PowerfulItems = new List<NextGenItem>();
            private static List<NextGenItem> LegendaryItems = new List<NextGenItem>();

            public static Action<NextGenItem, int> OnSegmentAdded;

            public static void LoadResourses()
            {
                AllItems = Resources.LoadAll("NextItems", typeof(NextGenItem)).Cast<NextGenItem>().ToList();
                foreach (NextGenItem item in AllItems)
                {
                    dict.Add(item.id, item);
                    switch (item.itemItemQuality)
                    {
                        case NextGenItem.ItemQuality.STANDARD:
                            SimpleItems.Add(item);
                            break;
                        case NextGenItem.ItemQuality.POWERFUL:
                            PowerfulItems.Add(item);
                            break;
                        case NextGenItem.ItemQuality.LEGENDARY:
                            LegendaryItems.Add(item);
                            break;
                    }

                }
            }

            public static NextGenItem GetItem(int id)
            {
                return dict[id];
            }
            /// <summary>
            /// get random item.
            /// </summary>
            /// <returns>get random item.</returns>
            /// <param name="quality">Quality.</param>
            public static NextGenItem GetItem(NextGenItem.ItemQuality quality)
            {
                NextGenItem h = null;
                switch (quality)
                {
                    case NextGenItem.ItemQuality.STANDARD:
                        h = SimpleItems[UnityEngine.Random.Range(0, SimpleItems.Count - 1)];
                        break;
                    case NextGenItem.ItemQuality.POWERFUL:
                        h = PowerfulItems[UnityEngine.Random.Range(0, PowerfulItems.Count - 1)];
                        break;
                    case NextGenItem.ItemQuality.LEGENDARY:
                        h = LegendaryItems[UnityEngine.Random.Range(0, LegendaryItems.Count - 1)];
                        break;
                }
                return h;
            }


            public static NextGenItem GetItem(NextGenItem.ItemQuality quality, NextGenItem.ItemType type)
            {
                List<NextGenItem> h = null;
                switch (quality)
                {
                    case NextGenItem.ItemQuality.STANDARD:
                        h = SimpleItems.FindAll(x => x.itemType == type);
                        break;
                    case NextGenItem.ItemQuality.POWERFUL:
                        h = PowerfulItems.FindAll(x => x.itemType == type);
                        break;
                    case NextGenItem.ItemQuality.LEGENDARY:
                        h = LegendaryItems.FindAll(x => x.itemType == type);
                        break;
                }
                return h[UnityEngine.Random.Range(0, h.Count - 1)];
            }

            public static void UpgradeItem(int itemId, int segments)
            {
                dict[itemId].AddSegment(segments);
                if (OnSegmentAdded != null)
                {
                    OnSegmentAdded.Invoke(dict[itemId], segments);
                }
            }

            public static List<NextGenItem> getAllItems()
            {
                return AllItems;
            }
        }


        public static class Game
        {
            public static List<Charecter> AllChars;
            public static List<HeroManiMenuCard> AllHeroCard;
            public static List<CharecterClass> AllCharClasses;
            public static List<Skill> AllSkills;

            public static bool isFirstLounch = true;

            public static Action<State> OnGameStateChange;
            public enum State
            {
                Menu, Level
            }

            private static State _state;
            public static State state { get { return _state; } set { _state = value; OnGameStateChange.Invoke(_state); } }

            public static void LoadResourses()
            {
                AllChars = Resources.LoadAll("chars", typeof(Charecter)).Cast<Charecter>().ToList();
                AllHeroCard = Resources.LoadAll("HeroMenu", typeof(HeroManiMenuCard)).Cast<HeroManiMenuCard>().ToList();
                AllSkills = Resources.LoadAll("Skills", typeof(Skill)).Cast<Skill>().ToList();
                AllCharClasses = Resources.LoadAll("Charecters Classes", typeof(CharecterClass)).Cast<CharecterClass>().ToList();
            }

        }

        public void Awake()
        {
            Settings.LoadSettings();
            ItemDataBase.LoadResourses();
            Game.LoadResourses();
        }


        DateTime currentDate;
        DateTime oldDate;
        public static TimeSpan difference;

        /// <summary>
        /// false on game start, true if player lounch menu second or more times
        /// </summary>
        public static bool isSession;


        public void Start()
        {
            Advertisement.Initialize("2789679");
            currentDate = DateTime.Now;
            if (PlayerPrefs.HasKey("lastPlayTime"))
            {
                long temp = Convert.ToInt64(PlayerPrefs.GetString("lastPlayTime"));
                oldDate = DateTime.FromBinary(temp);
                difference = currentDate.Subtract(oldDate);
            }

            PlayerGameCenter.AuthenticateUser();
            if (Game.isFirstLounch == true)
            {
                SaveManager.Load();
            }

        }


        private void Update()
        {
            if(Input.GetKey(KeyCode.A)){
                if (Player.Inventory.OnWarCoinsCountChange != null)
                {
                    foreach (Delegate item in Player.Inventory.OnWarCoinsCountChange.GetInvocationList())
                    {
                        if (item.Method != null)
                        {
                            Debug.Log(item.Method.Name);
                        }
                        else
                        {
                            Debug.Log("NO METHOD");

                        }
                    }
                }
            }
        }

        void OnApplicationQuit()
        {
            isSession = false;
            Settings.SaveSettings();
            if (Game.isFirstLounch == false)
            {
                SaveManager.Save();
            }
            PlayerPrefs.SetString("lastPlayTime", DateTime.Now.ToBinary().ToString());
        }


        private static float CalculateHealth()
        {
            float val = Player.Level + Player.charecter.Health * Player.Stats.Strength * 1.2f;
            return val;
        }

        private static float CalculateDefense()
        {
            float val;
            if (Player.Inventory.shild != null)
            {
                val = Player.Level + Player.charecter.Strength * Player.Stats.Dexterity * 1.2f + Player.Inventory.shild.Defense;  
            }
            else
            {
                val = Player.Level + Player.charecter.Strength * Player.Stats.Dexterity * 1.2f;  
            }

            return val;
        }

        private static float CalculateDamage()
        {
            float val;
            if (Player.Inventory.shild != null)
            {
                val = Player.Level + Player.charecter.Dexterity * Player.Stats.Dexterity * 1.2f + Player.Inventory.weapon.Damage;
            }
            else
            {
                val = Player.Level + Player.charecter.Dexterity * Player.Stats.Dexterity * 1.2f;
            }
            return val;
        }


        private static float CalculateSkillDamage()
        {
            float val;
            if (Player.Inventory.runa != null)
            {
                val = 1 * Player.Inventory.weapon.Damage * (Player.Stats.Strength);
            }
            else
            {
                val = 1 * (Player.Level * Player.Stats.Intelligence);
            }
            return val;
        }
        private static float CalculateCriticalDamage()
        {
            float val = Player.Damage*2;
            return val;
        }
        private static float CalculateCriticalHitChanse()
        {
            float val = Player.Stats.Dexterity / 4;
            return val;
        }


        public static void ClearListners()
        {
            Dungeon.OnEnemyDamaged = null;
            Dungeon.OnRoomsCountChenged = null;
            Player.OnLevelChange = null;
            Player.Experience.OnExperienceAdd = null;
            Player.Stats.OnStatsChange = null;
            Player.Inventory.OnWarCoinsCountChange = null;
            Player.Inventory.OnTrophySkullsCountChange = null;
            Player.Inventory.OnCrystalsCountChange = null;
            Player.OnPlayerDead = null;
            Player.Inventory.OnRunaPutOn = null;
            Player.Inventory.OnWeaponPutOn = null;
            Player.Inventory.OnShildPutOn = null;
            Player.OnHealthChange = null;
            ItemDataBase.OnSegmentAdded = null;
            Player.Inventory.OnHealthRemedyCountChange = null;
            Player.Inventory.OnSmallCestCountChange = null;
            Player.Inventory.OnMediumCestCountChange = null;
            Player.Inventory.OnLargeCestCountChange = null;
            Player.Inventory.OnEpicCestCountChange = null;
            Settings.OnUseShadersChange = null;
            Dungeon.Stage.TotalClearRoomsDungeonChenged = null;
        }



    }

    public static class Settings
    {
        public static void LoadSettings()
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            SoundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.5f);
            UseShaders = Convert.ToBoolean(PlayerPrefs.GetInt("UseShaders", 1));
            quality = PlayerPrefs.GetInt("quality", 3);
            SetQuality(quality);
        }

        public static void SaveSettings()
        {
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
            PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolume);
            PlayerPrefs.SetInt("UseShaders", Convert.ToInt32(UseShaders));
            PlayerPrefs.SetInt("quality", quality);
        }

        public static int quality;

        public static Action<float> OnMusicVolumeChange;
        private static float _MusicVolume = 0.5f;
        public static float MusicVolume
        {
            get { return _MusicVolume; }
            set
            {
                _MusicVolume = value;
                if (OnMusicVolumeChange != null)
                {
                    OnMusicVolumeChange.Invoke(_MusicVolume);
                }
            }
        }

        public static Action<float> OnSoundEffectsVolumeChange;
        private static float _SoundEffectsVolume = 0.5f;
        public static float SoundEffectsVolume
        {
            get { return _SoundEffectsVolume; }
            set
            {
                _SoundEffectsVolume = value;
                if (OnSoundEffectsVolumeChange != null)
                {
                    OnSoundEffectsVolumeChange.Invoke(_SoundEffectsVolume);
                }
            }
        }


        public static Action<bool> OnUseShadersChange;
        private static bool _UseShaders = false;
        public static bool UseShaders
        {
            get { return _UseShaders; }
            set
            {
                _UseShaders = value;
                if (OnUseShadersChange != null)
                {
                    OnUseShadersChange.Invoke(_UseShaders);
                }
            }
        }



        public static void SetQuality(int q)
        {
            QualitySettings.SetQualityLevel(q);
            quality = q;
        }

    }

    public static class PlayerGameCenter
    {
        public static bool isAutorize;
        static string leaderboardID = "war1";
        public static void AuthenticateUser()

        {

            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    isAutorize = true;
                    Debug.Log("success");
                }
                else
                {
                    Debug.Log("unsuccessful");
                }
            }
           );
        }

        public static void PostScoreOnLeaderBoard(int myScore)
        {

            if (isAutorize)
            {
                Social.ReportScore(myScore, leaderboardID, (bool success) =>
                {
                    if (success)
                        Debug.Log("Successfully uploaded");
                });
            }
            else
            {
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        isAutorize = true;
                        Social.ReportScore(myScore, leaderboardID, (bool successful) =>
                        {
                        });
                    }
                    else
                    {
                        Debug.Log("unsuccessful");
                    }
                });
            }
        }
    }
    public static class CharecterOpeningBehavior{
        public static void CheckForUnlockByFloor(int floor){
            switch (floor)
            {
                case 5:
                    LevelModel.Game.AllHeroCard[1].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[1].CharName, "IS UNLOCKED!");
                    break;
                case 20: 
                    LevelModel.Game.AllHeroCard[2].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[2].CharName,"IS UNLOCKED!");
                    break;
                case 40:
                    LevelModel.Game.AllHeroCard[3].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[3].CharName, "IS UNLOCKED!");
                    break;
                case 60:
                    LevelModel.Game.AllHeroCard[4].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[4].CharName, "IS UNLOCKED!");
                    break;
            }
        } 

        public static void CheckForUnlockByKills(int kills)
        {
            switch (kills)
            {
                case 100:
                    LevelModel.Game.AllHeroCard[5].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[5].CharName, "IS UNLOCKED!");
                    break;
                case 750:
                    LevelModel.Game.AllHeroCard[6].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[6].CharName, "IS UNLOCKED!");
                    break;
                case 1500:
                    LevelModel.Game.AllHeroCard[7].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[7].CharName, "IS UNLOCKED!");
                    break;
            }
        }

        public static void CheckForUnlockByLevel(int level){
            switch (level)
            {
                case 20: 
                    LevelModel.Game.AllHeroCard[8].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[8].CharName,"IS UNLOCKED!");
                    break;
                case 30:
                    LevelModel.Game.AllHeroCard[9].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[9].CharName, "IS UNLOCKED!");
                    break;
                case 40:
                    LevelModel.Game.AllHeroCard[10].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[10].CharName, "IS UNLOCKED!");
                    break;
                case 50:
                    LevelModel.Game.AllHeroCard[11].isCharecterUnlocked = true;
                    NotificationController.show.BlackNotifi(LevelModel.Game.AllHeroCard[11].CharName, "IS UNLOCKED!");
                    break;
            }
        } 
    }

}