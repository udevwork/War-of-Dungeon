// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Defence Buff")]
public class DefenceBuff : Skill {

    public override void FirsfEffect()
    {
        base.FirsfEffect();
    }

    public override void SecondEffect()
    {
        SeconEffectPosition = LevelModel.Player.MainHeroScript.RealModelCenter.transform;
        LevelModel.Player.MainHeroScript.gameObject.AddComponent<DefenceBuff_Effect>().StartBuff(5);
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

public class DefenceBuff_Effect : BuffTimer
{

    float saveDefense;

    public override void StartBuff(float TIMER)
    {
        saveDefense = (LevelModel.Player.SkillDamage * 2);
        LevelModel.Player.Add_Defense += saveDefense;
        base.StartBuff(TIMER);
    }

    public override void EndBuff()
    {
        LevelModel.Player.Add_Defense -= saveDefense;
        base.EndBuff();
    }
}


