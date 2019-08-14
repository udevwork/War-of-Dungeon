// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArsenalCard : MonoBehaviour {

  
    public Image icon;
    public Text nameText;
    public Text fragments;


    public void SetCard(NextGenItem item)
    {
        this.icon.sprite = item.icon;
        this.fragments.text = item.TotalItemSegment.ToString();
        nameText.text = item.itemName;    
    }
}
