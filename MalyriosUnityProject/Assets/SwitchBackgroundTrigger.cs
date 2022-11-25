using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBackgroundTrigger : MonoBehaviour
{
    public GameObject background;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            background.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            background.SetActive(false);
        }
    }
}
