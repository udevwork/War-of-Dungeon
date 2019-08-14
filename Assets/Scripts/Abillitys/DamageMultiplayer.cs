using UnityEngine;
using DataBase;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/New skill")]
public class DamageMultiplayer : Skill
{

    Collider[] colls;

    public override void FirsfEffect()
    {
        base.FirsfEffect();
    }


    public override void SecondEffect()
    {
        colls = CombatUtility.RadiusView(hero.gameObject.transform, 20, 512, false);


        if (colls != null)
        {
            SeconEffectPosition = colls[0].transform;
            base.SecondEffect();
            behavior();
        }
    }

    public override void behavior()
    {

        colls[0].GetComponent<IDamageble>().GetDamage(LevelModel.Player.SkillDamage * level,null);
        if (SFX)
        {
            SoundFX.play.CustomSound(SFX);
        }

    }
}

