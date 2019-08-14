using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour, IPicble
{
    public void pickUp()
    {
        SoundFX.play.PickUpPoison();
        GetComponent<Animator>().Play("PickUP");
        Destroy(gameObject,1);
    }
}
