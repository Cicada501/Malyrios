using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    private float width;
    private float height;
    bool joystickActive;


    void Awake()
    {
        width = (float)Screen.width / 3.0f;
        height = (float)Screen.height / 3.0f;

        // Position used for the cube.
        
    }

    void Start()
    {
        joystickActive = false;
        //joystick.gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        // Show joystick on touch at touch position
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 pos = touch.position;

            if(pos.x < width && pos.y < height && !joystickActive)
            {
                joystick.gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
                //joystick.gameObject.SetActive(true);
                joystickActive = true;
            }
        }
        else
        {
            //joystick.gameObject.SetActive(false);
            joystickActive = false;
        }

    }
}
