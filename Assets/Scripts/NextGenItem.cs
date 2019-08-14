using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/NextGen Item")]
public class NextGenItem : ScriptableObject
{

    public int id;
    public int TempId;
    public string itemName;
    public string itemDescription;
    public int itemlevel = 1;
    public int itemSegments = 0;
    public int TotalItemSegment = 0;
    public GameObject objectToSpawn;
    public Sprite icon;

    public GameObject SplashEffect;
    public GameObject HitEffect;
    public GameObject AdditionalDeathEffect;
    public BulletForce bullet;

    [System.Serializable]
    public enum ItemType
    {
        Weapon,
        Shild,
        Runa
    }
    public bool isForLeftHand;


    public ItemType itemType = ItemType.Weapon;

    public enum ItemQuality
    {
        STANDARD,
        POWERFUL,
        LEGENDARY,
    }


    public ItemQuality itemItemQuality = ItemQuality.STANDARD;

    public enum ItemStars
    {
        One,
        Two,
        Three,
        Four
    }

    public ItemStars Stars = ItemStars.One;

    public CharecterClass[] BelongClasses;

    public void AddSegment(int segment){
        if (itemlevel < 101)
        {
            TotalItemSegment += segment;
            UpGrade(segment);
        }
    }

    private void UpGrade(int segment)
    {
        if(itemlevel >= 100){
            itemlevel = 100;
            Debug.Log("MAXIMUM LEVEL");
            return;
        }

        switch (Stars)
        {
            case ItemStars.One :
                AddBehevior(segment, 10);
                break;
            case ItemStars.Two:
                AddBehevior(segment, 20);
                break;
            case ItemStars.Three:
                AddBehevior(segment, 30);
                break;
            case ItemStars.Four:
                AddBehevior(segment, 40);
                break;
        }
    }

    private void AddBehevior(int segment,int segmentRequired)
    {
        int pointsNeed = segmentRequired - itemSegments;
        if (segment > pointsNeed)
        {
            int tosave = segment - pointsNeed;
            itemSegments = 0;
            itemlevel += 1;
            StarController();
            UpGrade(tosave);
        }
        else if (segment < pointsNeed)
        {
            itemSegments += segment;
        }
        else if (segment == pointsNeed)
        {
            itemSegments = 0;
            itemlevel += 1;
            StarController();
        }
    }


    private void StarController(){
        if (itemlevel >= 1 && itemlevel <= 25)
        {
            Stars = ItemStars.One;
        }
        if (itemlevel > 25 && itemlevel <= 50)
        {
            Stars = ItemStars.Two;
        }
        if (itemlevel > 50 && itemlevel <= 75)
        {
            Stars = ItemStars.Three;
        }
        if (itemlevel > 75 && itemlevel <= 100)
        {
            Stars = ItemStars.Four;
        }
    }


    [Header("УРОН")]
    [SerializeField]
    private float _Damage = 1;

    [Header("ЗАЩИТА")]
    [SerializeField]
    private float _Defense = 1;

    [Header("МАГИЯ")]
    [SerializeField]
    private float _MagicForce = 1;



    public float Damage { get { return _Damage * itemlevel; }}

    public float Defense{ get { return _Defense * itemlevel; } }

    public float MagicForce{ get { return _MagicForce * itemlevel; } }


    public void SaveItem(){
        if (TotalItemSegment > 0)
        {
            PlayerPrefs.SetInt("SM_ID_" + id+"_LEVEL", itemlevel);
            PlayerPrefs.SetInt("SM_ID_" + id + "_itemSegments", itemSegments);
            PlayerPrefs.SetInt("SM_ID_" + id + "_TotalItemSegment", TotalItemSegment);
        } else {
            Debug.Log("nothing to save. Use def values");
        }
    }
    public void LoadItem()
    {
        if (PlayerPrefs.HasKey("SM_ID_" + id + "_TotalItemSegment"))
        {
            itemlevel = PlayerPrefs.GetInt("SM_ID_" + id + "_LEVEL");
            itemSegments = PlayerPrefs.GetInt("SM_ID_" + id + "_itemSegments");
            TotalItemSegment = PlayerPrefs.GetInt("SM_ID_" + id + "_TotalItemSegment");
        }
    }

}
