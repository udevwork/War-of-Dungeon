using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTCAMERACONTROLL : MonoBehaviour {


    public GameObject target;

    public Vector3 offset;
    public Vector3 rot;


    void Update () {
        transform.position = target.transform.position + offset;
        transform.rotation = Quaternion.Euler(rot);
	}


}
