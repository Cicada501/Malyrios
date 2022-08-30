using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.DateTime;

public class FireBall : MonoBehaviour
{
    [SerializeField] Transform fireBallSpawn;
    [SerializeField] GameObject fireball;
    [SerializeField] float fireballCooldownTime;
    public DateTime startTime;
    private System.TimeSpan ts;
    private Image abilityButtonImage;

    private float cooldownPercent = 1;

    private void Start()
    {
        abilityButtonImage = GetComponent<Image>();
    }


    // Update is called once per frasme
    void FixedUpdate()
    {
        //Spawn Fireball
        if (ButtonScript.receivedAbility1_input && ts.Seconds >=fireballCooldownTime)
        {
            Instantiate(fireball, fireBallSpawn.transform.position, fireBallSpawn.rotation);
            startTime = Now;
            ts = Now - startTime;
            cooldownPercent = 0;

        }

        if (ts.Seconds <= fireballCooldownTime)
        {
            ts = Now - startTime;
            //cooldownPercent = ts.Seconds / fireballCooldownTime;
            abilityButtonImage.fillAmount = cooldownPercent;
        }
        //to let the fill grow smooth approximate time 
        cooldownPercent += 0.02f/fireballCooldownTime;
        abilityButtonImage.fillAmount = cooldownPercent;



    }
}
