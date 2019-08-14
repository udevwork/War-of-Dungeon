// Denis super code 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

public class GameDefoultInitialization : MonoBehaviour {
    public Charecter charecter;

    private void Awake()
    {
        LevelModel.Player.charecter = this.charecter;
    }
}
