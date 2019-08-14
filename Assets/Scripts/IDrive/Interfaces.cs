using UnityEngine;
using UnityEngine.UI;
using DataBase;
using System;
using System.Collections.Generic;
using System.Collections;

public class Interfaces
{

}

/// <summary>
/// может взаимодействовать с игроком
/// </summary>
public interface IDamageble
{
    void GetDamage(float damage, IDamageble damagble);
    float GetHealthPoints();
    string GetTag();
    void Death();
}

public interface IRewardble
{
    void GetReward();
}


public interface IPicble
{
    void pickUp();
}


public class BuffTimer : MonoBehaviour
{
    public virtual void StartBuff(float TIMER){
        StartCoroutine("Delay",TIMER);
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        EndBuff();
    }

    public virtual void EndBuff(){
        Destroy(this);
    }

}



[System.Serializable]
public class Skill : ScriptableObject
{
    public int Id;
    public string skillName;
    public string skillDescription;
    public int level = 1;
    public int Cost = 10;
    public GameObject Main_VFX;
    public GameObject AdittionalEffect;
    public AudioClip SFX;
    public float ReloadTime;
    public Sprite SpellIcon;
    public float DefMagicForce;

    private bool coolingDown;
    [HideInInspector] public MainHero hero;
    [HideInInspector] public Transform SeconEffectPosition;

    public enum SkillType{
        Buff,Bullet,Range
    }

    public SkillType skillType;

    public virtual void FirsfEffect()
    {
        hero = LevelModel.Player.MainHeroScript;
        if (Main_VFX)
        {
            GameObject g = Instantiate(Main_VFX, hero.RealModelCenter.transform.position, hero.hitPos.transform.rotation);
            Destroy(g, 4);
        }
    }

    public virtual void SecondEffect()
    {
        if (AdittionalEffect)
        {
            if (skillType == SkillType.Buff)
            {
                if (SeconEffectPosition)
                {
                    GameObject g = Instantiate(AdittionalEffect, SeconEffectPosition.position, SeconEffectPosition.transform.rotation);
                    g.transform.SetParent(SeconEffectPosition);
                }
            }
            if (skillType == SkillType.Bullet)
            {
                GameObject g = Instantiate(AdittionalEffect, hero.hitPos.transform.position, hero.hitPos.transform.rotation);
            }
            if (skillType == SkillType.Range)
            {
                GameObject g = Instantiate(AdittionalEffect, hero.RealModelCenter.transform.position, AdittionalEffect.transform.rotation);
            }
            behavior();
        }
    }
 
    public void upgrade() {
        level += 1;       
    }   

    public virtual void behavior()
    {

    }

}


