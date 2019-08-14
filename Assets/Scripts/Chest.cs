using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

public class Chest : MonoBehaviour, IDamageble, IRewardble
{

    public bool destoyGraphic;
    public GameObject graphic;

    public float health;

    [Header("REWARD")]
    public int rewardMoney;
    public int rewardPlatinum;
    public int rewardGem;


    public Animator anim;
    public ParticleSystem FX;

    public void GetDamage(float damage,IDamageble damagble)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    public float GetHealthPoints()
    {
        return health;
    }

    public void Death()
    {
        gameObject.layer = LayerMask.NameToLayer("Default"); // что бы наш объект больше небыл виден
        SoundFX.play.OpenChest();
        FX.Play();
        GetReward();
        if (!destoyGraphic)
        {
            anim.SetTrigger("open");
        }
        else
        {
            Destroy(graphic);
            Destroy(gameObject,5);
        }
        Destroy(this);
    }

    public void GetReward()
    {
        LevelController.instance.AddCoinsToPlayer(rewardMoney);
        LevelController.instance.AddSkullsToPlayer(rewardPlatinum);
        LevelController.instance.AddCrystalsToPlayer(rewardGem);
    }

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log("COLLISION");
        if (other != null)
        {
            Debug.Log("OH SHIT");
            BulletForce BF = other.GetComponent<BulletForce>();
            BF.applyBulletSpell(this);
            if (BF.hitEffect)
            {
                Instantiate(BF.hitEffect, transform.position, transform.rotation);
            }
        }
    }

    public string GetTag()
    {
        return tag;
    }
}
