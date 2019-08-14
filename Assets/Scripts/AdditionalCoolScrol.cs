// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AdditionalCoolScrol : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GameShop gameShop;

    public GameObject content;
    public float sensetive;
    public bool isDragg;
    public Scrollbar SCRbar;

    [SerializeField] Button LeftButton;
    [SerializeField] Button RaightButton;

    private int ChildCount = 0;
    private List<float> xpos = new List<float>();
    public int cardIndex = 0;

    public Action OnCardIndexChange;

    private void OnEnable()
    {
        OnCardIndexChange += gameShop.SetTest;
    }

    private void Start()
    {

    }

    public void BuildScroll()
    {
        cardIndex = 0;
        xpos.Clear();
        ChildCount = gameShop.ItemsIDOnScreen.Count - 1 ;

        float step = 1f / ChildCount;
        xpos.Add(0);
        for (int i = 0; i < ChildCount; i++)
        {
            xpos.Add(xpos[i] + step);
        }
        ActiveDisactiveScrollButtonsCheck();
    }

    private void Update()
    {
        if (!isDragg && ChildCount > 0)
        {
            Lerphandler(xpos[cardIndex]);
        }
    }


    public void CalcIndex()
    {
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

    public void setCardIndex(int index)
    {
        cardIndex = index;
        if (OnCardIndexChange != null)
        {
            OnCardIndexChange.Invoke();
        }
    }


    public void LeftButtonLogic()
    {
        if (cardIndex > 0){
            setCardIndex(cardIndex -= 1);
        }

        ActiveDisactiveScrollButtonsCheck();

    }

    public void RightButtonLogic(){
        if (cardIndex < ChildCount) {
            setCardIndex(cardIndex += 1);
        }

        ActiveDisactiveScrollButtonsCheck();
    }

    public void ActiveDisactiveScrollButtonsCheck(){
        if (cardIndex == 0)
        {
            LeftButton.interactable = false;
        } else {
            LeftButton.interactable = true;

        }

        if (cardIndex == ChildCount)
        {
            RaightButton.interactable = false;
        } else {
            RaightButton.interactable = true;
        }
    }

    public void OnDisable()
    {
        OnCardIndexChange = null;
    }
}
