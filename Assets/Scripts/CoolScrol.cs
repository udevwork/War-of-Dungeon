// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CoolScrol : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler{

    public static CoolScrol instance;


    public GameObject content;
    public float sensetive;
    public bool isDragg;
    public Scrollbar SCRbar;

    public Animation anim;
    public Animation anim_2;
    public string animationName;
    public string animationName_2;

    private int ChildCount = 0;
    private List<float> xpos = new List<float>();
    public int cardIndex = 0;

    public bool isAnyCardOpen;

    public List<CardAnimation> Cards;

    public Action OnCardIndexChange;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        ChildCount = content.transform.childCount-1;

        float step = 1f / ChildCount;
        xpos.Add(0);
        for (int i = 0; i < ChildCount; i++)
        {
            xpos.Add(xpos[i] + step);
        }

        if (anim != null)
        {
            anim.Play(animationName);
            anim[animationName].speed = 0;
        }
        if (anim_2 != null)
        {
            anim_2.Play(animationName_2);
            anim_2[animationName_2].speed = 0;
        }
    }

    private void Update()
    {
        if (!isDragg)
        {
            Lerphandler(xpos[cardIndex]);
        }
        if(isDragg && isAnyCardOpen){
            if (Cards.Count > 0)
            {
                CloseAllCards();
            }
        }
    }


    public void CalcIndex(){
        int newCardIndex = 0;
        float tempDist = 100;
        for (int i = 0; i < xpos.Count; i++)
        {
            if (Mathf.Abs(SCRbar.value - xpos[i]) < tempDist)
            {
                newCardIndex = i;
                tempDist = Mathf.Abs(SCRbar.value - xpos[i]);
            }
        }

        cardIndex = newCardIndex;
        if (OnCardIndexChange != null)
        {
            OnCardIndexChange.Invoke();
        }
    }


    void Lerphandler(float pos)
    {
        float newX = Mathf.Lerp(SCRbar.value, pos, Time.deltaTime * 8f);
        SCRbar.value = newX;
    }

    public void CloseAllCards(){
        foreach (CardAnimation cardBeha in Cards)
        {
            if (cardBeha.isOpen)
            {
                cardBeha.Close();
            }
        }
    }

    public void onSliderScroll()
    {
        if (anim != null)
        {
            anim[animationName].normalizedTime = SCRbar.value;
        }
        if (anim_2 != null)
        {
            anim_2[animationName_2].normalizedTime = SCRbar.value;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        SCRbar.value += -(eventData.delta.x / sensetive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragg = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragg = false;
        CalcIndex();
    }

    public void setCardIndex(int index){
        cardIndex = index;
        if (OnCardIndexChange != null)
        {
            OnCardIndexChange.Invoke();
        }
    }

    public void OnDisable()
    {
        OnCardIndexChange = null;
    }
}
