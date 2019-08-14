// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

public class CharecterChooseButton : MonoBehaviour {

    public HeroManiMenuCard heroinfo;
    public bool IsClose;

    private void OnMouseUp()
    {
        if (IsClose == false)
        {
            if (!CoolScrol.instance.isDragg)
            {
                CharecterChooseView.instance.OpenWindow(heroinfo);
                LevelModel.Player.heroCard = heroinfo;
                LevelModel.Player.charecter = heroinfo.charecter;
            }
        }
    }

}
