// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostViewTest : MonoBehaviour {

    public GameObject DelayPrefab;
    public GameObject BoostParent;

	void Start () {
        DelayMethod.instance.OnNewDelayMethodCreated = (DelayMethod.IEnumClass obj) => CreateBoost(obj);
	}
	
     void CreateBoost(DelayMethod.IEnumClass ienum){
        GameObject boost = Instantiate(DelayPrefab, BoostParent.transform);
        boostIcon BI = boost.GetComponent<boostIcon>();
        ienum.SecondPass = (int obj) => BI.SetTimeText(obj);
        ienum.End = BI.ActionEnd;
    }

    public void CreateNewButton()
    {
        DelayMethod.instance.CreateNewDelayMethod(Random.Range(3,20));
    }

}
