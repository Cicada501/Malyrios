using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayereffect : MonoBehaviour
{


	private Transform parentObjectPositionOffset;
	private float lengthX, startposX;
	private float lengthY, startposY;
	GameObject cam = null;
	//float camPosY = 19.5f; //base value, so that the backgrounds spawn in the right position wherever the player spawns
	[SerializeField] float parallaxEffectX = 0.0f;
	[SerializeField] float parallaxEffectY = 0.0f;
	

	// Use this for initialization
	void Start ()
	{
		parentObjectPositionOffset = transform.parent;
		cam = GameObject.Find("Camera");
		startposX = transform.position.x + parentObjectPositionOffset.transform.position.x ;
		startposY = transform.position.y + parentObjectPositionOffset.transform.position.y ;
	}
	
	// Update is called once per frame
	void Update () {
		float distX = (cam.transform.position.x * parallaxEffectX);
		float distY = ((cam.transform.position.y) * parallaxEffectY);

		transform.position = new Vector3(startposX + distX,startposY+ distY, transform.position.z);
	}
}

//https://www.youtube.com/watch?v=zit45k6CUMk