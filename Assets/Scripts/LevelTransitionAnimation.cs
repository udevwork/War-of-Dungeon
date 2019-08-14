using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionAnimation : MonoBehaviour
{

    public Animator anim;
    public Text FloorCountText;

    void Start()
    {
        LevelController.instance.GenerationStart += LevelTransitionStart;
        LevelController.instance.GenerationEnd += LevelTransitionEnd;
    }

    public void LevelTransitionStart()
    {

        anim.Play("on");
    }


    public void LevelTransitionEnd()
    {

        anim.Play("off");
    }
    private void OnDisable()
    {
        LevelController.instance.GenerationStart += LevelTransitionStart;
        LevelController.instance.GenerationEnd += LevelTransitionEnd;
    }

}
