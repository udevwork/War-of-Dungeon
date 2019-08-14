// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, IDamageble {

    public float health = 100;
    public Animation anim;
    public BoxCollider dorCollider;
    public BoxCollider dorwayCollider_1;
    public BoxCollider dorwayCollider_2;

    public void Death()
    {
        Destroy(dorCollider);
        Destroy(dorwayCollider_1);
        Destroy(dorwayCollider_2);
        gameObject.layer = LayerMask.NameToLayer("Default");
        Destroy(this);
        anim.Play("DOOR OPEN");
       // LevelController.instance.DG.RecostructNavigarionSurface();
    }

    public void GetDamage(float damage, IDamageble damagble)
    {
        health -= 20;
        if (health <= 0)
        {
            Death();
        } else {
            anim.Play("DOOR DAMAGE");
        }

    }

    public float GetHealthPoints()
    {
        return health;
    }

    public string GetTag()
    {
        return tag;
    }
}
