// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class CardAnimation : MonoBehaviour {

    public Transform ITEM;
    public Animator anim;

    public AnimationCurve ACardX, ACardY;
    public RectTransform mainimg; // Object with destinationSize
    public RectTransform rectimg; // ObjectToResize
    public Button btn;

    public float CardOpenSpeed;
    public float TimeVal;

    public bool isOpen;

    public enum CardState {
        Open,
        Min,
        Close,
        Max
    }

    public CardState state;

    void Start () {
        CoolScrol.instance.Cards.Add(this);

        btn.onClick.AddListener(Open);

        ACardX = new AnimationCurve(new Keyframe(0,rectimg.rect.size.x), new Keyframe(CardOpenSpeed, mainimg.rect.size.x+10));
        ACardY = new AnimationCurve(new Keyframe(0, rectimg.rect.size.y), new Keyframe(CardOpenSpeed, mainimg.rect.size.y+10));
      //  ACardCenterX = new AnimationCurve(new Keyframe(0,rectimg.rect.size.x), new Keyframe(1,mainimg.rect.size.x));
      //  ACardCenterY = new AnimationCurve(new Keyframe(0,rectimg.rect.size.x), new Keyframe(1,mainimg.rect.size.x));
    }
	
	void Update () {
        if(state == CardState.Open){
            OpenBehavior();  
        } 
        if (state == CardState.Close)
        {
            CloseBehavior();
        }
        if (state == CardState.Max)
        {
            TimeVal = CardOpenSpeed;
        }
        if (state == CardState.Min)
        {
            TimeVal = 0;
        }
	}

    [ContextMenu("open")]
    public void Open(){
        ITEM.SetAsLastSibling();
        TimeVal = 0;
        state = CardState.Open;
        anim.SetTrigger("Open");
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(Close);
        CoolScrol.instance.isAnyCardOpen = true;
        isOpen = true;
        SoundFX.play.PlayCardCloseSound();
    }

    [ContextMenu("close")]
    public void Close(){
        TimeVal = CardOpenSpeed;
        state = CardState.Close;
        anim.SetTrigger("Close");
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(Open);
        CoolScrol.instance.isAnyCardOpen = false;
        isOpen = false;
        SoundFX.play.PlayCardCloseSound();
    }

    private void OpenBehavior(){
        TimeVal += Time.deltaTime;
        rectimg.sizeDelta = new Vector2(ACardX.Evaluate(TimeVal),ACardY.Evaluate(TimeVal));
        if(TimeVal >= CardOpenSpeed){
            state = CardState.Max;
        }
    }
    private void CloseBehavior(){
 
        TimeVal -= Time.deltaTime;
        rectimg.sizeDelta = new Vector2(ACardX.Evaluate(TimeVal), ACardY.Evaluate(TimeVal));
        if (TimeVal <= 0)
        {
            state = CardState.Min;
        }
    }
}
