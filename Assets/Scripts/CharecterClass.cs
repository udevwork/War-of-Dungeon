// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Charecter/Class")]
public class CharecterClass : ScriptableObject {

    public int Id;
    public string ClassName;
    public string ClassDecription;
    public Sprite ClassIcon;


    public bool IsShildOnBack;

    public NextGenItem DefoultWeapon;

    public RuntimeAnimatorController BR_animator;
    public RuntimeAnimatorController Simple_animator;


}
