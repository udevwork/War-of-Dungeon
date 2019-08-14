// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other != null){
            if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
                LevelController.instance.GoToNextLevel();
            }
        }
    }
}
