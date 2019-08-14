using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using EL.Dungeon;

public class NPC : Human, IRewardble, IDamageble
{

    public Enemy enemyData;

    public enum EnemyType
    {
        Enemy,Neutral
    }

    public EnemyType enemyType;

    public string enemyName;
    public float maxHealth;

    public float health;
    public float damage;
    public float defense;

    public float RadiusForFindPlayer;

    public RagdollScript ragdoll;

    public bool isRangeFire;
    public BulletForce bullet;

    public Room roomBelongTo;

    [Header("DEATH REWARD")]
    public int rewardMoney;
    public int rewardPlatinum;
    public int rewardGem;
    public int rewardExperiense;


    public override void Start()
    {
        base.Start();
        int multyplayer = LevelModel.Dungeon.curentFloor;
        health = enemyData.health * multyplayer;
        maxHealth = enemyData.health * multyplayer;
        damage = enemyData.damage * multyplayer;
        defense = enemyData.defense * multyplayer;
        RadiusForFindPlayer = enemyData.RadiusForFindPlayer;

        rewardMoney = enemyData.rewardMoney;
        rewardPlatinum = enemyData.rewardPlatinum;
        rewardGem = enemyData.rewardGem;
        rewardExperiense = enemyData.rewardExperiense;

        LayerToScan = 1 << LayerMask.NameToLayer("Player");
        LevelController.instance.AddEnemy(this);
        if (enemyType != EnemyType.Neutral)
        {
            if (AgentTarget == null)
            {
                InvokeRepeating("FindEnemy", 1, 0.5f);
            }
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        autoPilot();
    }



    // ВЫЗЫВАЕТСЯ В СТАРТЕ  InvokeRepeating
    public void FindEnemy()
    {
        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, FildOfView, LayerToScan);
        if (colls.Length != 0)
        {
            if (colls[0].gameObject.tag == "Player")
            {
                AgentTarget = colls[0].gameObject;
                currentEnemyToAttack = colls[0].gameObject;
                SFX.PlayRandomAgression();
                CancelInvoke("FindEnemy");
            }
        }
    }


    public override void GetDamage(float damage, IDamageble damagble)
    {
        base.GetDamage(damage,damagble);
        if(currentEnemyToAttack == null){
            currentEnemyToAttack = LevelModel.Player.MainHeroScript.gameObject;
        }
        if(AgentTarget == null){
            AgressiveRoar();
        }
        health -= damage;
        AgentTarget = LevelModel.Player.MainHeroScript.gameObject;
        Instantiate(LevelModel.Player.Inventory.weapon.HitEffect, transform.position, transform.rotation);
        SFX.PlayRandomPain();
        if(health <= 0){
            Death();
        }
    }


    public void AgressiveRoar(){
        Collider[] colls = CombatUtility.RadiusView(transform,20,512,true);
       
        if(colls != null){
            foreach (Collider item in colls)
            {
                item.GetComponent<NPC>().AgentTarget = LevelModel.Player.MainHeroScript.gameObject;
            }
        }
    }

    public void StartEppear(){
        
    }

    public void EndEppear()
    {

    }

    public override void SetDamage()
    {
        base.SetDamage();
        currentEnemyToAttack = serchForEnemy();
        if (currentEnemyToAttack)
        {
            if (currentEnemyToAttack.GetComponent<IDamageble>().GetHealthPoints() <= 0)
            {
                currentEnemyToAttack = null;
            } else {
                currentEnemyToAttack.GetComponent<IDamageble>().GetDamage(damage, this);
            }
        }
        else
        {
            //SoundFX.PlayRandomSwordSwapSound();
        }
    }


    public override void Fire()
    {
        base.Fire();

        if (bullet == null)
        {
            return;
        }

        BulletForce BF = Instantiate(bullet, hitPos.transform.position, transform.rotation);

     //   if (CombatUtility.LineView(hitPos.transform, FildOfView + 10, LayerToScan, false) != null)
     //   {
    //        currentEnemyToAttack = CombatUtility.LineView(hitPos.transform, FildOfView, LayerToScan, false);
    //    }

        // Искать врагов
        if (CombatUtility.SmartRadiusView(RealModelCenter.transform, FildOfView + 15, LayerToScan) != null)
        {
            Collider[] colls = CombatUtility.SmartRadiusView(RealModelCenter.transform, FildOfView + 15, LayerToScan);
            if (colls != null)
            {
                currentEnemyToAttack = CombatUtility.GetClosestObject(RealModelCenter.transform, colls);
            }
        }

        if (currentEnemyToAttack != null)
        {
            if (currentEnemyToAttack.GetComponent<NPC>())
            {
                BF.target = currentEnemyToAttack.GetComponent<NPC>().RealModelCenter.transform;
                BF.damage = damage;
            }
        }


      
    }


    public override void Death()
    {
        roomBelongTo.RemoveDeadEnemy(this);
        LevelModel.Player.Info.totalKills += 1;
        StopMotion.instance.Slow(0.1f, 0.5f);
        ragdoll.SetKinematic(false);
        Destroy(GetComponent<CapsuleCollider>());
        GetReward();
        Instantiate(LevelModel.Player.Inventory.weapon.AdditionalDeathEffect, transform.position, transform.rotation);
        LevelController.instance.RemoveEnemy(this);
        Destroy(anim);
        navAgent.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        isDead = true;
        SFX.PlayRandomDeathSound();
        Destroy(gameObject,5);
        Destroy(this);
    }

    public float GetHealthPoints()
    {
        return health;
    }
    public void GetReward()
    {
        LevelController.instance.AddCoinsToPlayer(rewardMoney);
        LevelController.instance.AddSkullsToPlayer(rewardPlatinum);
        LevelController.instance.AddCrystalsToPlayer(rewardGem);
        LevelController.instance.AddExpToPlayer(rewardExperiense);
    }

    public string GetTag()
    {
        return tag;
    }
}
