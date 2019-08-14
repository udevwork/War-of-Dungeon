// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class screenShootEditor : MonoBehaviour {

    public List<GameObject> Objectslist;
    private static List<GameObject> NGIs = new List<GameObject>();

    public static GameObject currentObjectOnScreen;
    public static int CurrentObjectIndex;

   // [ContextMenu("LoadData")]
    public void LoadDataBase(){
        NGIs = Objectslist;
        Debug.Log(NGIs.Count + " objects loaded");
        currentObjectOnScreen = NGIs[0];
        CurrentObjectIndex = 0;
    }

   // [MenuItem("My Commands/Next Item _p")]
    static void NextItem()
    {
        Debug.Log("NEXT ITEM");
        currentObjectOnScreen.SetActive(false);
        CurrentObjectIndex += 1;
        currentObjectOnScreen = NGIs[CurrentObjectIndex];
        currentObjectOnScreen.SetActive(true); 
    }

  //  [MenuItem("My Commands/Take Screenshots _o")]
    static void ScreenShot()
    {
        Debug.Log("Try to take screenshot of "+currentObjectOnScreen.name);
        ScreenCapture.CaptureScreenshot(currentObjectOnScreen.name+".png");
        Debug.Log("READY !");
    }


 //   [MenuItem("My Commands/Previous Item _i")]
    static void PreviousItem()
    {
        Debug.Log("PRIVIOUS ITEM");
        currentObjectOnScreen.SetActive(false);
        CurrentObjectIndex -= 1;
        currentObjectOnScreen = NGIs[CurrentObjectIndex];
        currentObjectOnScreen.SetActive(true); 
    }
  

}
