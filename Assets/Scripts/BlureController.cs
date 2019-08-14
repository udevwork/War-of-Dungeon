// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlureController : MonoBehaviour {

    public Material mat;
    public int val;

	void ChangeRadius () {
        mat.SetInt("Radius",val);
	}
	

}
