using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataBase;



public class RagdollScript : MonoBehaviour {

    public List<Rigidbody> RBS;

    public void SetKinematic(bool newValue)
	{
        foreach (Rigidbody rb in RBS)
		{
			rb.isKinematic=newValue;
            rb.velocity = ((gameObject.transform.position - LevelModel.Player.MainHeroScript.gameObject.transform.position) * 2);
		}
	}
	

    [ContextMenu("FindRigidbodyes")]
    public void FindRigidbodyes(){
        RBS = GetComponentsInChildren<Rigidbody>().ToList();
    }
}
