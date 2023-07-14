using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvents
{
    private static GameObject fireballButton;

    public void FireEvent(string eventName)
    {
        switch (eventName)
        {
            case "learn Fireball":
                PlayerData.LearnedFireball = true;
                fireballButton.SetActive(true); //still needs to be set again, after loading, where to do this?
                break;
            case "BigRatAttack":
                // Code, um das "BigRatAttack" Event auszulösen
                break;
            // Weitere Fälle...
        }
    }
}