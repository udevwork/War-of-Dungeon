// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Fire Sphere")]
public class FireSphere : Skill
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
        Collider[] colls = CombatUtility.RadiusView(LevelModel.Player.MainHeroScript.RealModelCenter.transform,10,LevelModel.Player.MainHeroScript.GetLayerToScan,true);
        if(colls != null){
            foreach (Collider item in colls)
            {
                item.GetComponent<IDamageble>().GetDamage(10*LevelModel.Player.SkillDamage, null);
            }
        }
    }


}

