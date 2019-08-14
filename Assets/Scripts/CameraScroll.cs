using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScroll : MonoBehaviour {

	[SerializeField] private KeyCode activateKey;
	[SerializeField] private float  x_boundMIN;
	[SerializeField] private float  x_boundMAX;
	[SerializeField] private float  z_boundMIN;
	[SerializeField] private float  z_boundMAX;
	[SerializeField] private float averageFOV;
	[SerializeField] private float sensitivity;
	[SerializeField] private float speed;
	[SerializeField] private GameObject TargetToFollow;

	[SerializeField] private Camera cam;
    [SerializeField]private Vector3 cameraStartPosition;
    [SerializeField]private Vector3 scrollStartPosition;

	private void Awake()
	{
		cameraStartPosition = transform.position;
	}

	private void Update()
	{
		cam.transform.position = Vector3.Lerp (cam.transform.position,TargetToFollow.transform.position,speed);
//		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
//		{
//			return;
//		}
//
		if(!Input.GetKey(KeyCode.Mouse0))
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			scrollStartPosition = Input.mousePosition;
		}
		else
		{
			Vector3 deltaMovement = Input.mousePosition - scrollStartPosition;
			scrollStartPosition = Input.mousePosition;
			TargetToFollow.transform.localPosition = new Vector3 (
				Mathf.Clamp(transform.position.x - sensitivity * (averageFOV / cam.fieldOfView) * deltaMovement.x, x_boundMIN, x_boundMAX),
				transform.position.y,
				Mathf.Clamp(transform.position.z - sensitivity * (averageFOV / cam.fieldOfView) * deltaMovement.y, z_boundMIN, z_boundMAX)
			);
		}


	}
}





