using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSplash : MonoBehaviour {


	public List<GameObject> EmptyObjects;
	public ParticleSystem SplashFX;
	public Transform Parent;
	public Transform SpashPosition;

	public void PlayFX (int hitIndex) {
        if (SplashFX)
        {
            SplashFX.transform.position = EmptyObjects[hitIndex].transform.position;
            SplashFX.transform.rotation = EmptyObjects[hitIndex].transform.rotation;
            SplashFX.Play();
        }
	}

	[ContextMenu("SET POSITION")]
	public void setpos () {
		SplashFX.transform.position = SpashPosition.transform.position;
		SplashFX.transform.rotation = SpashPosition.transform.rotation;

	}
	

	[ContextMenu("AddTransform")]
	public void AddTransform(){
		GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
		g.transform.position = SplashFX.gameObject.transform.position;
		g.transform.rotation = SplashFX.gameObject.transform.rotation;
		g.transform.SetParent(Parent);
		EmptyObjects.Add(g);
		EmptyObjects[EmptyObjects.Count-1].transform.name = "SwordTrialPosition " + EmptyObjects.Count.ToString();
	}
}
