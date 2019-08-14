// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Hill Buff")]
public class HillBuf : Skill {


    public override void FirsfEffect()
    {
        base.FirsfEffect();
    }

    public override void SecondEffect()
    {
        SeconEffectPosition = LevelModel.Player.MainHeroScript.RealModelCenter.transform;
        LevelModel.Player.MainHeroScript.gameObject.AddComponent<HillBuf_Effect>().StartBuff(2);
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



public class HillBuf_Effect : BuffTimer
{

    public override void StartBuff(float TIMER)
    {
        base.StartBuff(TIMER);
        InvokeRepeating("Hill",0,0.1f);
    }

    public void Hill(){
        if (LevelModel.Player.Health != LevelModel.Player.MaxHealth)
        {
            LevelModel.Player.Health += 5;
        }
    }

    public override void EndBuff()
    {
        CancelInvoke("Hill");
        base.EndBuff();
    }
}