using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DataBase;

public class MainHero : Human, IDamageble
{
    public bool isGod;
    public Renderer charRender;

    public Transform WepPos, ShildPos, ShildPosBack, RunaPos, BowPos;

    public ParticleSystem HitEffect;
    public ParticleSystem LevelUpEffect;

    public SimpleTouchController TouchController;
    public CharacterController CharCont;

    public SwordSplash swordSplash;
    public float TCMovementSpeed;

    public void Awake()
    {
        LevelModel.Player.OnLevelChange += OnLevelUP;
    }

    public override void Start()
    {
        base.Start();
        if (LevelModel.Player.heroCard.ChoosedMaterial)
        {
            charRender.material = LevelModel.Player.heroCard.ChoosedMaterial;
        }

        if (LevelModel.Player.charecter.isBR_animator == true)
        {
            anim.runtimeAnimatorController = LevelModel.Player.charecter.Class.BR_animator;
        }
        else if (LevelModel.Player.charecter.isBR_animator == false)
        {
            anim.runtimeAnimatorController = LevelModel.Player.charecter.Class.Simple_animator;
        }
        TouchController = SimpleTouchController.instance;
        LayerToScan = 1 << LayerMask.NameToLayer("Enemy");
    }



    private void OnLevelUP()
    {
        SoundFX.play.playerLevelUp();
        LevelUpEffect.Play();
        anim.SetTrigger("LevelUp");
    }

    public void Update()
    {
       
        if (TouchController)
        {
            if (TouchController.GetTouchPosition.magnitude > 0.1f)
            {
                anim.SetFloat("Run", 1);
                CharCont.SimpleMove(new Vector3(TouchController.GetTouchPosition.x, 0, TouchController.GetTouchPosition.y).normalized * TCMovementSpeed);
            }
            else
            {
                anim.SetFloat("Run", 0);
                CharCont.SimpleMove(Vector3.zero);

            }

            if (TouchController.GetTouchPosition != Vector2.zero)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(TouchController.GetTouchPosition.x, TouchController.GetTouchPosition.y) * Mathf.Rad2Deg, transform.eulerAngles.z);
            }

            if (TouchController.GetTouchPosition == Vector2.zero & currentEnemyToAttack)
            {
                var lookPos = currentEnemyToAttack.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion targettolook = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targettolook, 0.1f);
            }
            else
            {
                currentEnemyToAttack = null;
            }
        }
    }


    public override void GetDamage(float damage, IDamageble damagble)
    {
        if(isGod)  {  return;  } //CHEAT

        base.GetDamage(damage, damagble);
        if (HitEffect != null)
        {
            Instantiate(HitEffect, transform.position, transform.rotation);
        }
        LevelModel.Player.Health -= damage;
        if (LevelModel.Player.Health <= 0)
        {
            Death();
        }
        if (LevelModel.Player.OnHealthChange != null)
        {
            LevelModel.Player.OnHealthChange.Invoke();
        }
    }



    public override void Hit()
    {
        anim.SetTrigger("Atack");
    }

    public override void SetDamage()
    {
        base.SetDamage();
        SFX.PlayRandomSwordSwapSound();


        if (CombatUtility.LineView(hitPos.transform, FildOfView, LayerToScan, false) != null)
        {
            currentEnemyToAttack = CombatUtility.LineView(hitPos.transform, FildOfView, LayerToScan, false);

            IDamageble targetToHit = currentEnemyToAttack.GetComponent<IDamageble>();

            if (targetToHit != null)
            {
                SmartCameraBehavior.instance.noise(0.3f, 1);
                SFX.PlayRandomSwordSound();
                float DMG;

                if (CombatUtility.probability(LevelModel.Player.CriticalChanse))
                {
                    DMG = LevelModel.Player.CriticalDamage;
                } 
                else 
                {
                    DMG = LevelModel.Player.Damage;
                } 

                targetToHit.GetDamage(DMG, this);


                if (targetToHit.GetTag() != "Brakble")

                {
                    LevelModel.Dungeon.OnEnemyDamaged.Invoke(currentEnemyToAttack.GetComponent<NPC>(),DMG);
                }

                if (targetToHit.GetHealthPoints() <= 0)
                {
                    currentEnemyToAttack = null;
                }
            }

        }


        if (CombatUtility.RadiusView(transform, FildOfView, LayerToScan, false) != null)
        {
            foreach (Collider item in CombatUtility.RadiusView(transform, FildOfView, LayerToScan, false))
            {
                if (item.GetComponent<IDamageble>() != null)
                {
                    if (item.gameObject != currentEnemyToAttack)
                    {
                        SFX.PlayRandomSwordSound();
                        item.GetComponent<IDamageble>().GetDamage(LevelModel.Player.Damage / 3, this);
                    }
                    if (currentEnemyToAttack == null)
                    {
                        currentEnemyToAttack = item.gameObject;
                        if (currentEnemyToAttack.tag != "Brakble")
                        {
                            LevelModel.Dungeon.OnEnemyDamaged.Invoke(currentEnemyToAttack.GetComponent<NPC>(),LevelModel.Player.Damage / 3);
                        }
                    }
                }
            }
        }
        if (HitEffect != null)
        {
            HitEffect.Play();
        }
    }

    public override void Fire()
    {
        base.Fire();

        if (LevelModel.Player.Inventory.weapon.bullet == null)
        {
            return;
        }

        BulletForce BF = Instantiate(LevelModel.Player.Inventory.weapon.bullet, LevelModel.Player.MainHeroScript.hitPos.transform.position, LevelModel.Player.MainHeroScript.gameObject.transform.rotation);

        SFX.PlayRandomSwordSwapSound();

        if (CombatUtility.LineView(hitPos.transform, FildOfView + 10, LayerToScan, false) != null)
        {
            currentEnemyToAttack = CombatUtility.LineView(hitPos.transform, FildOfView, LayerToScan, false);
        }

        // Искать врагов
        if (CombatUtility.SmartRadiusView(RealModelCenter.transform, FildOfView + 15, LayerToScan) != null)
        {
            Collider[] colls = CombatUtility.SmartRadiusView(RealModelCenter.transform, FildOfView + 15, LayerToScan);
            if (colls != null)
            {
                currentEnemyToAttack = CombatUtility.GetClosestObject(RealModelCenter.transform, colls);
            }
        }

        if (currentEnemyToAttack != null)
        {
            if (currentEnemyToAttack.GetComponent<NPC>()){
                BF.target = currentEnemyToAttack.GetComponent<NPC>().RealModelCenter.transform;
                BF.damage = LevelModel.Player.Damage;
            }
        }


        if (HitEffect != null)
        {
            HitEffect.Play();
        }

    }

    public void Spell(int index)
    {
        if (index == 0)
        {
            anim.SetTrigger("Skill 1");
            LevelModel.Player.charecter.Skill_One.FirsfEffect();
        }
        if (index == 1)
        {
            anim.SetTrigger("Skill 2");
            LevelModel.Player.charecter.Skill_Two.FirsfEffect();
        }
        if (index == 2)
        {
            anim.SetTrigger("Skill 3");
            LevelModel.Player.charecter.Skill_Three.FirsfEffect();
        }
    }
    public void AnimSpell(int index)
    {
        if (index == 0)
        {
            LevelModel.Player.charecter.Skill_One.SecondEffect();
        }
        if (index == 1)
        {
            LevelModel.Player.charecter.Skill_Two.SecondEffect();
        }
        if (index == 2)
        {
            LevelModel.Player.charecter.Skill_Three.SecondEffect();
        }
    }

    public void OnTriggerEnter(Collider other)
    {


    }

    public float GetHealthPoints()
    {
        return LevelModel.Player.Health;
    }

    public override void Death()
    {
        anim.Play("Death");
        gameObject.layer = LayerMask.NameToLayer("Dead");
        gameObject.tag = "Untagged";
        isDead = true;
    }

    public string GetTag()
    {
        return tag;
    }
}
