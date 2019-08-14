using UnityEngine;
using UnityEngine.UI;

public class SpellFillAmount : MonoBehaviour {

	public Image FillImage;
	public float Timer;
	public bool coolDown;
	public Button btn;

	void Update () {
		if	(coolDown){
			FillImage.fillAmount += 1.0f / Timer * Time.deltaTime; 
			if(FillImage.fillAmount >= 1){
				coolDown = false;
				btn.interactable = true;
			}
		}
	}
	
	public void StartFillAmount(){
		FillImage.fillAmount = 0;
		coolDown = true;
		btn.interactable = false;
	}

}
