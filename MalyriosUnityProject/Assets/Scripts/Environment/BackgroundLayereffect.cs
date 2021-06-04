using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayereffect : MonoBehaviour {

	private float lengthX, startposX;
	private float lengthY, startposY;
	[SerializeField] GameObject cam = null;
	float camPosY;
	[SerializeField] float parallaxEffectX = 0.0f;
	[SerializeField] float parallaxEffectY = 0.0f;
	// Use this for initialization
	void Start () {
		camPosY = cam.transform.position.y;
		startposX = transform.position.x;
		startposY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float distX = (cam.transform.position.x * parallaxEffectX);
		float distY = ((cam.transform.position.y-camPosY) * parallaxEffectY);

		transform.position = new Vector3(startposX + distX,startposY+ distY, transform.position.z);
	}
}

//https://www.youtube.com/watch?v=zit45k6CUMk