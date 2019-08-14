using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Charecter/New char")]
public class Charecter : ScriptableObject
{
 
    public int Id;
    public GameObject objectToSpawn;
    public bool isBR_animator;
    public CharecterClass Class;
    public float Health;
    public int Vitality;
    public int Dexterity;
    public int Intelligence;
    public int Strength;
    public Skill Skill_One, Skill_Two, Skill_Three;

}
