// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuCharUpgradeControll : MonoBehaviour {

    public Animator anim;
    public GameObject daddy;
    public ParticleSystem VFX;

    public void Start()
    {
        CoolScrol.instance.OnCardIndexChange += SpellAnimation;
        anim = daddy.GetComponentInChildren<Animator>();
    }

    public void SpellAnimation(){

        if (CoolScrol.instance.cardIndex == 1)
        {
            anim.SetTrigger("spell");
          //  VFX.Play();
        }
    }
}
