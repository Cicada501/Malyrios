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
        ts = Now - startTime;
        abilityButtonImage.fillAmount = cooldownPercent;
        cooldownPercent += 0.02f / fireballCooldownTime;
    }

    //called if the Fireballbutton is pressed
    public void OnClickFireball()
    {
        if (ts.Seconds >= fireballCooldownTime)
        {
            playerAnimator.SetTrigger("ThrowFireball");
        }
    }

    //called in player animation as AnimationEvent
    public void SpawnFireball()
    {
        Instantiate(fireball, fireBallSpawn.transform.position, fireBallSpawn.rotation);
        startTime = Now;
        ts = Now - startTime;
        cooldownPercent = 0;
    }
}