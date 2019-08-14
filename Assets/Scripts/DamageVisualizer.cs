// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System.Linq;

public class DamageVisualizer : MonoBehaviour {

    public List<CanvasItemPointer> itemsPool;

    public static DamageVisualizer instance;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        LevelModel.Dungeon.OnEnemyDamaged += (NPC arg1, float arg2) => CreateText(arg1.RealModelCenter.transform, arg2);
    }

    void CreateText(Transform objTransform, float value){
        if (itemsPool.Count > 0)
        {
            itemsPool.First().CreateDMG(objTransform, Mathf.Round(value));
            itemsPool.Remove(itemsPool.First());
        }
    }
	
} 
