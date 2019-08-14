// Denis super code 
using UnityEngine;
using UnityEngine.UI;
using DataBase;

public class CanvasScaleHelper : CanvasScaler
{
    [SerializeField] public Vector2 resolution;

    protected override void Awake()
    {
        resolution.x = Screen.width;
        resolution.y = Screen.height;
        if (resolution == new Vector2(1125, 2436))
        {
            matchWidthOrHeight = 0.89f;
            Debug.Log("| resulution scaled for IPHONE X  (9 : 18,5) |");
        }
        else
        {
            matchWidthOrHeight = 1f;
            Debug.Log("| resulution scaled for NORMAL  (9 : 16) |");
        }
    }

}

