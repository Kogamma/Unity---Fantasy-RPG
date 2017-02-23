using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameObject CameraObj;
	public Camera camera;

	public bool left = false;
	public bool right = false;
	public bool cameraIn = false;
	public bool cameraOut = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp (0))
		{
			left = false;
			right = false;
			cameraIn =false;
			cameraOut = false;
		}
		if(left == true)
		{
			CameraObj.transform.RotateAround(Vector3.down, Time.deltaTime * 5);
		}
		if(right == true)
		{
			CameraObj.transform.RotateAround(Vector3.up, Time.deltaTime * 5);	
		}
		if(cameraIn == true)
		{
			if(camera.fieldOfView >= 20)
			{
				camera.fieldOfView--;
			}
		}
		if(cameraOut == true)
		{
			if(camera.fieldOfView <= 60)
			{
				camera.fieldOfView++;
			}
		}
	
	}
	void OnGUI() {

	//	GUI.skin.button.fontSize = 10;

		if (GUI.RepeatButton(new Rect(Screen.width/2 - 170, 450, 100, 50), "Zoom In"))
		{
			cameraIn = true;
		}
		if (GUI.Button(new Rect(Screen.width/2 - 50, 450, 100, 50), "Zoom Reset"))
		{
			camera.fieldOfView = 40;
		}
		if (GUI.RepeatButton(new Rect(Screen.width/2 + 70, 450, 100, 50), "Zoom Out"))
		{
			cameraOut = true;
		}

		if (GUI.RepeatButton(new Rect(Screen.width/2 - 170, 520, 100, 50), "Camera Left"))
		{
			left = true;
		}
		if (GUI.Button(new Rect(Screen.width/2 - 50, 520, 100, 50), "Camera Reset"))
		{
			CameraObj.transform.eulerAngles = new Vector3(0,0,0);
		}
		if (GUI.RepeatButton(new Rect(Screen.width/2 + 70, 520, 100, 50), "Camera Right"))
		{
			right = true;
		}
	}
}