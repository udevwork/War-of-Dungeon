// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Ice Stan")]
public class IceStan : Skill
{

    public override void FirsfEffect()
    {
        base.FirsfEffect();
    }

    public override void SecondEffect()
    {
        Collider[] colls = CombatUtility.RadiusView(LevelModel.Player.MainHeroScript.RealModelCenter.transform, 20, LevelModel.Player.MainHeroScript.GetLayerToScan, true);

        if (colls != null)
        {
            GameObject obj = CombatUtility.GetClosestObject(LevelModel.Player.MainHeroScript.RealModelCenter.transform, colls);
            SeconEffectPosition = obj.transform;
            obj.AddComponent<IceStan_Effect>();
            obj.GetComponent<IceStan_Effect>().StartBuff(5);
        }
        base.SecondEffect();
        behavior();
    }

    public override void behavior()
    {
        if (SFX)
        {
            SoundFX.play.CustomSound(SFX);
        }

    }
}

public class IceStan_Effect : BuffTimer
{
    NPC enemy;
    
    public override void StartBuff(float TIMER)
    {
        enemy = gameObject.GetComponent<NPC>();
        enemy.navAgent.enabled = false;
        enemy.navAgent.speed = 0;
        enemy.anim.speed = 0;
        base.StartBuff(TIMER);
    }

    public override void EndBuff()
    {
        enemy.navAgent.enabled = true;
        enemy.anim.speed = 1;
        base.EndBuff();
    }
}
