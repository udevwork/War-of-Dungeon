// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Dragon Force")]
public class DragonForce : Skill
{
    
    public override void FirsfEffect()
    {
        base.FirsfEffect();
    }

    public override void SecondEffect()
    {
        SeconEffectPosition = LevelModel.Player.MainHeroScript.RealModelCenter.transform;
        LevelModel.Player.MainHeroScript.gameObject.AddComponent<DragonForce_Effect>().StartBuff(level);
        //SecondEffectParticles.GetComponent<DecalDestroyer>().lifeTime = level;
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

public class DragonForce_Effect : BuffTimer{

    float saveDamage;

    public override void StartBuff(float TIMER)
    {
        saveDamage = (LevelModel.Player.SkillDamage * 2);
        LevelModel.Player.Add_Damage += saveDamage;
        base.StartBuff(TIMER);
    }

    public override void EndBuff()
    {
        LevelModel.Player.Add_Damage -= saveDamage;
        base.EndBuff();
    }
}

