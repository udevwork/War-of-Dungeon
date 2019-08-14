using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoading : MonoBehaviour {

    public string NextScaneName;

	void Start () {
        SceneLoadingManager.load.Scene(SceneLoadingManager.Scenes.MAINMENU, "GAME START");
	}

}
