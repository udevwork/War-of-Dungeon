// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boostIcon : MonoBehaviour {

    public Text timeText;
    public Animation anim;

    public void SetTimeText(int time){
        timeText.text = "TIME LEFT: " + time;
    }

    public void ActionEnd(){
        anim.Play("END BOOST");
        Destroy(gameObject,0.5f);
    }
	
}
