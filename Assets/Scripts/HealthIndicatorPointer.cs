using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicatorPointer : MonoBehaviour {

    [SerializeField] Slider hpBar;


    public void SetMax(float val){
        hpBar.maxValue = val;
    }

    public void SetCurrent(float val){
        hpBar.value = val;
    }

}
