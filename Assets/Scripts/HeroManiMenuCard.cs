using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "HeroCard/New HeroCard")]

public class HeroManiMenuCard : ScriptableObject {

    public int Id;

	public Charecter charecter;
    public Sprite CharIcon;
	public string CharName;

    public bool isCharecterUnlocked;

    public Material ChoosedMaterial;
    public Material[] skins;

    public CharecterClass ClassOne;
    public CharecterClass ClassTwo;
    public CharecterClass ClassThree;
    public CharecterClass ClassFour;


    [HideInInspector] public float skin_Choosed;
    public GameObject CallMeDaddyModel;

    public void SaveChar(){
        if (isCharecterUnlocked)
        {
            PlayerPrefs.SetString("HEROCARD_"+Id,"open");
        } else {
            PlayerPrefs.SetString("HEROCARD_" + Id, "close");
        }
        for (int i = 0; i < skins.Length; i++)
        {
            if(skins[i] == ChoosedMaterial){
                PlayerPrefs.SetInt("CHAR_SKIN",i);
            }
        }
    }
    public void LoadChar(){
        if (PlayerPrefs.GetString("HEROCARD_" + Id) == "open")
        {
            isCharecterUnlocked = true;
        }
        else if (PlayerPrefs.GetString("HEROCARD_" + Id) == "close")
        {
            isCharecterUnlocked = false;
        }
        //ChoosedMaterial = skins[PlayerPrefs.GetInt("CHAR_SKIN")];
    }

}
