using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBGLayerEffect : MonoBehaviour
{
    private float startposX;
    private float startposY;
    [SerializeField] GameObject cam = null;
    [SerializeField] float parallaxEffectX = 0.0f;
    [SerializeField] float parallaxEffectY = 0.0f;
    private Vector3 camStartPos;

    // Use this for initialization
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        camStartPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distX = (cam.transform.position.x - camStartPos.x)*parallaxEffectX;
        float distY = ((cam.transform.position.y - camStartPos.y)*parallaxEffectY );

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);
    }
}

//https://www.youtube.com/watch?v=zit45k6CUMk