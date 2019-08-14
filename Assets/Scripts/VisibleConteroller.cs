// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleConteroller : MonoBehaviour {

    public MeshRenderer Mrender;

    private void OnBecameVisible()
    {
        Debug.Log("sdf");
        Mrender.enabled = true;
    }

    private void OnBecameInvisible()
    {
        Debug.Log("sdfwer");

        Mrender.enabled = false;        
    }
}
