using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayereffect : MonoBehaviour
{
	private float startposX;
	private float startposY;
	GameObject cam;
	[SerializeField] float parallaxEffectX;
	[SerializeField] float parallaxEffectY;
	/*
	 * This Background layer implements the idea of the Sprites all moving away towards the player with a certain percentage(defined by the paralax value)
	 * of the way between player and the initial location of the object. Like this it is guaranteed to have this object always at its initial location, if the
	 * player is at this location.
	 * Disadvantage: The speed of the layer effect decreases when the player gets closer to the initial location
	 */

	// Use this for initialization
	void Start ()
	{
		cam = GameObject.Find("Camera");
		startposX = transform.position.x;
		startposY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
		float distX = (cam.transform.position.x - startposX) * parallaxEffectX;
		float distY = (cam.transform.position.y - startposY) * parallaxEffectY;

		transform.position = new Vector3(startposX + distX,startposY+ distY, 0);
	}
}
