// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/New Bullet skill")]
public class SpellBullet : Skill {

    public override void FirsfEffect()
    {
        base.FirsfEffect();
     
    }

    public override void SecondEffect()
    {
        GameObject obj = CombatUtility.GetClosestObject(LevelModel.Player.MainHeroScript.hitPos.transform, CombatUtility.RadiusView(LevelModel.Player.MainHeroScript.hitPos.transform, 20, LevelModel.Player.MainHeroScript.GetLayerToScan,true));
        Debug.Log(obj.name);
        BulletForce bullet = AdittionalEffect.GetComponent<BulletForce>();
        bullet.damage = DefMagicForce * level;
        bullet.speed = 50;
        if(obj== null){
            bullet.target = LevelModel.Player.MainHeroScript.hitPos.transform;
        } else {
            Debug.Log("HELLO, BITCHES! __" + obj.name);
            bullet.target = obj.GetComponent<NPC>().RealModelCenter.transform;
            LevelModel.Player.MainHeroScript.currentEnemyToAttack = obj;    
        }
        base.SecondEffect();

    }

    public override void behavior()
    {
        base.behavior();
    }

}




