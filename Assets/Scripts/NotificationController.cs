using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour {

    public static NotificationController show;
    [Header("Smash Notification")]

    public Animation SmashAnim;
    public Animation BlackAnim;
    public Text BlackTitletext;
    public Text BlackDescText;

    public Text SmashText;
    public Image SmashImg;
    public Sprite BossFightImg;

    [Header("Smash Notification")]
    public Animator SmokeAnim;
    public Text SmokeText;

    private void Awake()
    {
        show = this;
    }

    public void Smash(string note,Sprite img){
        SmashText.text = note;
        SmashImg.sprite = img;
        SmashAnim.Play();
    }

    public void BossFightSmash()
    {
        SmashText.text = "";
        SmashImg.sprite = BossFightImg;
        SmashAnim.Play();
    }

    public void SmokeEffect(string note){
        SmokeText.text = note;
        SmokeAnim.Play("MEGATITLE_2");
    }
    public void BlackNotifi(string title,string desc)
    {
        BlackTitletext.text = title;
        BlackDescText.text = desc;
        BlackAnim.Play();
    }

}
