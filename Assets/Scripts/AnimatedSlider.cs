using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimatedSlider : MonoBehaviour {

	public Animation anim;
	public Scrollbar scrollbar;
	void Start()
	{
		anim.Play("SliderAnimation");
		anim["SliderAnimation"].speed = 0;
	}
	public void onSliderScroll(){
		anim["SliderAnimation"].normalizedTime = scrollbar.value;
	}
}
