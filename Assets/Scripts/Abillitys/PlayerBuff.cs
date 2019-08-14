using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Player Buff")]
public class PlayerBuff : Skill
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

    }
}

