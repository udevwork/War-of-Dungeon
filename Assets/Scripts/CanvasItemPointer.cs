// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasItemPointer : MonoBehaviour
{

    public RectTransform element;
    public Transform targetToFollow;
    public UnityEngine.UI.Text text;
    public Animation anim;
    public Camera MainCam;

   
    public void CreateDMG(Transform objTransform, float dmg)
    {
        targetToFollow = objTransform;
        text.text = dmg.ToString();
        if (element)
        {
            if (targetToFollow)
            {
                Vector2 screenPos = MainCam.WorldToScreenPoint(targetToFollow.position);
                element.position = screenPos;
            }
        }
        anim.Play();
    }

    public void ReturnToPool()
    {
        DamageVisualizer.instance.itemsPool.Add(this);
    }
}
