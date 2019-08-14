using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using Cinemachine;
public class SmartCameraBehavior : MonoBehaviour
{

   public CinemachineVirtualCamera cam;

    public static SmartCameraBehavior instance;
    CinemachineBasicMultiChannelPerlin perlin;
    public Animator anim;


    public void Awake()
    {
        SceneLoadingManager.load.OnAnimationComplete += PlayAnim;   
    }

    void Start()
    {
      instance = this;
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cam.m_LookAt = LevelModel.Player.MainHeroScript.gameObject.transform;
        cam.m_Follow = LevelModel.Player.MainHeroScript.gameObject.transform;
    }

    public void PlayAnim(){
        anim.Play("CM vcam1_ Start");
    }



    public void noise(float time,float force){
        StartCoroutine(INoise(time,force)); 
    }

    /// <summary>
    /// INs the oise.
    /// </summary>
    /// <returns>null</returns>
    /// <param name="time">Time to shake</param>
    /// <param name="amlitude">Amlitude.  Shoold be gather then 0</param>
    IEnumerator INoise(float time,float amlitude){
        perlin.m_AmplitudeGain = 0.04f * amlitude;
        perlin.m_FrequencyGain = 2f * amlitude;
        yield return new WaitForSeconds(time);
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }

}
