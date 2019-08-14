// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

public class OclussionCullinEnabler : MonoBehaviour {

    public Camera cam;

	void Start () {
        if(cam == false){
            cam = GetComponent<Camera>();
        }

	}

}
