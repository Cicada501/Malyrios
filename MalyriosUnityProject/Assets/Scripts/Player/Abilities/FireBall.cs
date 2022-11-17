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
    [SerializeField] private Animator playerAnimator;
    public DateTime startTime;
    private TimeSpan ts; private Image abilityButtonImage;

    private float cooldownPercent = 1;

    private void Start()
    {
        abilityButtonImage = GameObject.Find("ButtonFireball").GetComponent<Image>();
    }


    // Important that its Fixedupdate, so cooldown is same on all devices
    void FixedUpdate()
    {
        //Spawn Fireball
        if (ButtonScript.receivedAbility1_input && ts.Seconds >= fireballCooldownTime)
        {
            playerAnimator.SetTrigger("ThrowFireball");
        }

        if (ts.Seconds <= fireballCooldownTime)
        {
            ts = Now - startTime;
            //cooldownPercent = ts.Seconds / fireballCooldownTime;
            abilityButtonImage.fillAmount = cooldownPercent;
        }

        //to let the fill grow smooth approximate time 
        cooldownPercent += 0.02f / fireballCooldownTime;
        abilityButtonImage.fillAmount = cooldownPercent;
    }

    public void SpawnFireball()
    {
        Instantiate(fireball, fireBallSpawn.transform.position, fireBallSpawn.rotation);
        startTime = Now;
        ts = Now - startTime;
        cooldownPercent = 0;
    }
}