// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Shilds Circle")]
public class ShildCircle : Skill
{

    public override void FirsfEffect()
    {
        base.FirsfEffect();
    }

    public override void SecondEffect()
    {
        SeconEffectPosition = LevelModel.Player.MainHeroScript.RealModelCenter.transform;
        base.SecondEffect();
        behavior();
    }

    public override void behavior()
    {
        if (SFX)
        {
            SoundFX.play.CustomSound(SFX);
        }
        LevelModel.Player.MainHeroScript.gameObject.AddComponent<ShildCircle_Effect>().StartBuff(4);
    }
}


public class ShildCircle_Effect : BuffTimer
{
    float saveValue;
    NPC enemy;

    public override void StartBuff(float TIMER)
    {
        LevelModel.Player.MainHeroScript.OnDamaged += (float arg1, IDamageble arg2) => Behavior(arg1,arg2);
        base.StartBuff(TIMER);
    }

    void Behavior(float damage, IDamageble damageble ){
        float x = damage * 20 / 100;
        damageble.GetDamage(x,null);
        LevelModel.Player.Add_Defense += LevelModel.Player.SkillDamage;
    }

    public override void EndBuff()
    {
        LevelModel.Player.MainHeroScript.OnDamaged -= Behavior;
        LevelModel.Player.Add_Defense -= LevelModel.Player.SkillDamage;
        base.EndBuff();
    }
}