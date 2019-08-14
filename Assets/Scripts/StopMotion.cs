// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMotion : MonoBehaviour {
    
    public static StopMotion instance;

    void Start()
    {
        instance = this;
    }

    public void Slow(float time, float force)
    {
        StartCoroutine(INoise(time, force));
    }

    IEnumerator INoise(float time, float amlitude)
    {
        Time.timeScale = amlitude;
        yield return new WaitForSeconds(time);
        Time.timeScale = 1;
    }

}
