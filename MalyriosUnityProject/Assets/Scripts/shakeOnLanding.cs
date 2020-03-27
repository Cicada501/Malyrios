using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeOnLanding : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            
            CameraShake_Cinemachine.Shake(0.3f, 0.4f, 15f);
        }

    }
}
