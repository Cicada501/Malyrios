using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decisions : MonoBehaviour
{
    public GameObject fireballButton;
    public static bool bigRatAttack;
    public static bool learnedFireball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("learnedFireball: "+ learnedFireball);
        if (!learnedFireball)
        {
            fireballButton.SetActive(false);
        }
        else
        {
            fireballButton.SetActive(true);
        }
        
    }
}
