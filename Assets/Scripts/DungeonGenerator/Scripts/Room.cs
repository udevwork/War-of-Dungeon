using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataBase;

namespace EL.Dungeon { 
    public class Room : MonoBehaviour {

        public bool CountedRoom;

        public enum RoomType
        {
            room,corridor
        }
        public RoomType roomType;

        public List<EL.Dungeon.GeneratorDoor> doors;

        [SerializeField] EnemysSet RoomEnemySet;
        [SerializeField] EnemysSet RoomBossSet;

        public bool IsSpawnBoss;
        public bool isAllEnemysSpawned = false; // in this room
        public bool isRoomClear = false; // in this room
        public Transform[] RoomEnemysSpawnPoints;
        public List<NPC> enemysSpawnedIn;

        //WARNING - when doing the dungeon gen we sometimes Instantiate a room, check if it will fit and if it doesn't
        //we IMMEDIATLY destroy it.  Awake() is called with instantiation - Start() waits until the function returns..
        //SO to be safe, don't use Awake if you don't have to.  Put all enemy and room specific instantiation in START!
        void Awake() {
            //DungeonGenerator.roomsCalledStart++;
            //Debug.Log("AWAKE");
        }

	    void Start () {
            DungeonGenerator.roomsCalledStart++;
            if (RoomEnemysSpawnPoints.Length > 0)
            {
                LevelModel.Dungeon.RoomsCount++;
            }
	    }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    if(roomType == RoomType.room){
                        GameObject.Find("CM vcam1").GetComponent<Animator>().SetBool("room", true);
                    }   
                    if (roomType == RoomType.corridor)
                    {
                        GameObject.Find("CM vcam1").GetComponent<Animator>().SetBool("room", false);
                    }
                }
            }

            if(isAllEnemysSpawned == true){
                return;
            }
            if(other != null){
                if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
                    StartCoroutine("SpawnEnemys");
                }
            }
            if(IsSpawnBoss && LevelController.instance.isBossFight){
                NotificationController.show.BossFightSmash();
            } 
        }


        public void RemoveDeadEnemy(NPC npc){
            enemysSpawnedIn.Remove(npc);
            if(enemysSpawnedIn.Count == 0){
                isRoomClear = true;
                LevelModel.Dungeon.RoomsCount--;
                LevelModel.Dungeon.Stage.TotalClearRoomsDungeon += 1;
                NotificationController.show.BlackNotifi("ROOM CLEAR","money reward");
                LevelController.instance.AddCoinsToPlayer(100);
            }
        }

        [ContextMenu("SPAWN")]
        public IEnumerator SpawnEnemys()
        {
            if(IsSpawnBoss && LevelController.instance.isBossFight){
                enemysSpawnedIn.Add(SpawnBoss(RoomEnemysSpawnPoints[0]));
            } 

            if (!IsSpawnBoss && LevelController.instance.isBossFight){
                foreach (Transform item in RoomEnemysSpawnPoints)
                {
                    enemysSpawnedIn.Add(SpawnEnemyes(item));
                }
            }

            if (!IsSpawnBoss && !LevelController.instance.isBossFight)
            {
                foreach (Transform item in RoomEnemysSpawnPoints)
                {
                    enemysSpawnedIn.Add(SpawnEnemyes(item));
                }
            }

            if (IsSpawnBoss && !LevelController.instance.isBossFight)
            {
                foreach (Transform item in RoomEnemysSpawnPoints)
                {
                    enemysSpawnedIn.Add(SpawnEnemyes(item));
                }
            }
            isAllEnemysSpawned = true;
            yield return 1;
        }


        public NPC SpawnEnemyes(Transform pointtrans)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            GameObject E;
           
            E = Instantiate(RoomEnemySet.EnemysToSpawn[Random.Range(0, LevelController.instance.EnemysSet.EnemysToSpawn.Count)].ObjectToSpawn, pointtrans.position, rot, pointtrans);
                E.GetComponent<NPC>().roomBelongTo = this;
                return E.GetComponent<NPC>();

        }

        public NPC SpawnBoss(Transform pointtrans)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            GameObject E;

                E = Instantiate(RoomBossSet.EnemysToSpawn[Random.Range(0, LevelController.instance.BossSet.EnemysToSpawn.Count)].ObjectToSpawn, pointtrans.position, rot, pointtrans);
                E.GetComponent<NPC>().roomBelongTo = this;
                return E.GetComponent<NPC>();

        }

        private void OnDrawGizmos() {
             for (int i = 0; i < doors.Count; i++) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(doors[i].transform.position, 0.1f);

                Gizmos.DrawRay(new Ray(doors[i].transform.position, doors[i].transform.right));

                Gizmos.color = Color.green;
                Gizmos.DrawRay(new Ray(doors[i].transform.position, doors[i].transform.up));

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(new Ray(doors[i].transform.position, doors[i].transform.forward));
             }
        }

         public GeneratorDoor GetRandomDoor(DRandom random) {
             doors.Shuffle(random.random);
             for (int i = 0; i < doors.Count; i++) {
                 if (doors[i].open) return doors[i];
             }
             Debug.LogError("Room::GetRandomDoor() - No open doors...");
             return null;
         }

         public bool hasOpenDoors() {
             for (int i = 0; i < doors.Count; i++) {
                 if (doors[i].open) return true;
             }
             return false;
         }

         public GeneratorDoor GetFirstOpenDoor() {
             for (int i = 0; i < doors.Count; i++) {
                 if (doors[i].open) return doors[i];
             }
             Debug.LogError("Room::GetFirstOpenDoor() - No open doors...");
             return null;
         }
    }
}
