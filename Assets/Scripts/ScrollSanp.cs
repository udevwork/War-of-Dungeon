using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSanp : MonoBehaviour {

	public float MODUL = 250.0f;
	public string animationName;
	public Animation anim;
	public Slider sliderchik;


	public RectTransform ScrollPanel;
	public RectTransform[] _RectsTrans;
	public RectTransform _center;
	public ScrollRect _RectPanel;

	// Private Variable

	public bool Dragging;

	private float[] _distance;
	private bool isDragging = false;
	private int bttnDistance;
	private int minButtonNum;
	private bool isEnable = false;

	void Start () {

		anim.Play(animationName);
		anim[animationName].speed = 0;
		sliderchik.wholeNumbers = false;
		sliderchik.maxValue = _RectsTrans.Length-1;

		isEnable = false;
		int bttnLenght = _RectsTrans.Length;
		_distance=new float[bttnLenght];

		bttnDistance = (int)Mathf.Abs (_RectsTrans[1].anchoredPosition.x-
			_RectsTrans[0].anchoredPosition.x);
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(0)){
			Dragging = true;
		} else {
			Dragging = false;
		}

		if(!Dragging){

            if (_RectPanel.velocity.magnitude < MODUL)
            {

                for (int i = 0; i < _RectsTrans.Length; i++)
                {

                    _distance[i] = Mathf.Abs(_center.anchoredPosition.x - _RectsTrans[i].transform.position.x);


                }

                float minDistance = Mathf.Min(_distance);

                for (int k = 0; k < _RectsTrans.Length; k++)
                {

                    if (minDistance == _distance[k])
                    {

                        minButtonNum = k;
                        sliderchik.value = Mathf.Lerp(sliderchik.value, (k), 0.2f);
                        ScaleUpAndScaleDown (k);

                    }

                }

                    LerpToTargetPosition(minButtonNum * -bttnDistance);

            }
	}
		
	}
	public void onSliderScroll(){
		anim[animationName].normalizedTime = sliderchik.normalizedValue;
	}

	void LerpToTargetPosition(int pos){

		float newX = Mathf.Lerp (ScrollPanel.anchoredPosition.x,pos,Time.deltaTime*8f);
		Vector2 newPosition = new Vector2 (newX, ScrollPanel.anchoredPosition.y);
		ScrollPanel.anchoredPosition = newPosition;

	}

	Vector3 scale =new  Vector3(0.0085f,0.0085f,0.0085f);
	void ScaleUpAndScaleDown(int index){
		
		for (int i = 0; i < _RectsTrans.Length; i++) {

			if (i == index) {
				if (_RectsTrans [i].localScale.x <= 1.0f) {

					_RectsTrans [i].localScale += Vector3.Lerp (scale, _RectsTrans [i].localScale, Time.deltaTime * 0.5f);


				}

			} else {
				if (_RectsTrans [i].localScale.x >= 0.85f) {
					
					_RectsTrans [i].localScale -= Vector3.Lerp (scale, _RectsTrans [i].localScale, Time.deltaTime * 0.5f);
//					
				}

			}

		}

	}

}
