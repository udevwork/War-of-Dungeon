// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonsBehavior : MonoBehaviour {

    public Animator HeaderAnimator;

    public Animation anim;
    public Text lable;

    public WinLevel[] WindowLevel;

    private int TempLevel; // Current WindowLevel

    // вызвать извне
    public void SetLevel(int level)
    {
        TempLevel = level;
    }

    // вызвать извне
    public void SetText(string text){
        WindowLevel[TempLevel].TempText = text;
        anim.Play();
    }

    // вызвать извне
    public void SetScroll(ScrollRect scrollrect)
    {
        WindowLevel[TempLevel].TempRect = scrollrect;
    }
    // вызвать извне
    public void SetAction(Animator animator)
    {
        WindowLevel[TempLevel].TempAnimator = animator;
    }
    public void SetButton(Button button)
    {
        WindowLevel[TempLevel].TempButton = button;
    }


    // вешаем в клипе анимации
    public void AnimationAction(){
        lable.text = WindowLevel[TempLevel].TempText;
    }

    // вешаем на саму кнопку
    public void MainAction(){

        if((TempLevel-1) == -1){
            HeaderAnimator.SetTrigger("Close");
        } 


        WindowLevel[TempLevel].TempAnimator.SetTrigger("Back");
        WindowLevel[TempLevel].TempRect.enabled = true;
        if (WindowLevel[TempLevel].TempButton != null)
        {
            WindowLevel[TempLevel].TempButton.interactable = true;
        }
        if(TempLevel > 0){
            TempLevel = TempLevel - 1;
            SetText(WindowLevel[TempLevel].TempText);
            SetScroll(WindowLevel[TempLevel].TempRect);
            SetAction(WindowLevel[TempLevel].TempAnimator);
            SetButton(WindowLevel[TempLevel].TempButton);
        }
    }

    [System.Serializable]
    public struct WinLevel{

        public Animator TempAnimator;
        public string TempText;
        public ScrollRect TempRect;
        public Button TempButton;
    }

}
