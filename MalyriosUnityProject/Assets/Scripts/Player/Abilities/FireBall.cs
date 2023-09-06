﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.DateTime;
using Object = UnityEngine.Object;

public class FireBall : MonoBehaviour
{
    [SerializeField] Transform fireBallSpawn;
    [SerializeField] GameObject fireball;
    [SerializeField] float fireballCooldownTime;
    [SerializeField] private Animator playerAnimator;
    public DateTime startTime;
    private TimeSpan ts;
    private Image abilityButtonImage;

    private float cooldownPercent = 1;
    private Vector2 startPoint;
    private GameObject arrowInstance;
    [SerializeField] private GameObject arrowPrefab;
    private GameObject player;
    private bool isDragging;
    public Vector2 direction;
    private Vector2 endPoint;
    private Vector2 fireballSpeed;
    private bool castingFireball = false;
    private CharacterController2D controller;

    private void Start()
    {
        abilityButtonImage = GameObject.Find("ButtonFireball").GetComponent<Image>();
        player = ReferencesManager.Instance.player;
        controller = GetComponent<CharacterController2D>();
    }


    // Important that its Fixedupdate, so cooldown is same on all devices
    void FixedUpdate()
    {
        ts = Now - startTime;
        abilityButtonImage.fillAmount = cooldownPercent;
        cooldownPercent += 0.02f / fireballCooldownTime;
    }

    //called in player animation as AnimationEvent
    public void SpawnFireball()
    {
        GameObject fireballInstance = Instantiate(fireball, fireBallSpawn.transform.position, fireBallSpawn.rotation);
        FireBallProjectile projectile = fireballInstance.GetComponent<FireBallProjectile>();
        projectile.FireballDirection = direction;
        startTime = Now;
        ts = Now - startTime;
        cooldownPercent = 0;
    }

    public void PlayFireballSound()
    {
        ReferencesManager.Instance.startFireballSound.Play();
    }


    public void OnPointerDown(BaseEventData data)
    {
        if (ts.Seconds >= fireballCooldownTime)
        {
            castingFireball = true;
            PointerEventData eventData = data as PointerEventData;
            startPoint = eventData.position;
            arrowInstance = Instantiate(arrowPrefab, player.transform.position, Quaternion.identity);
            isDragging = true;
            GetComponent<PlayerMovement>().disableMovement = true;
            playerAnimator.SetTrigger("CreateFireball");
        }
    }

    public void OnPointerUp(BaseEventData data)
    {
        if (castingFireball)
        {
            PointerEventData eventData = data as PointerEventData;
            isDragging = false;
            GetComponent<PlayerMovement>().disableMovement = false;
            endPoint = eventData.position;
            playerAnimator.SetTrigger("ThrowFireball");
            Destroy(arrowInstance);
            castingFireball = false;
        }
    }


    private void Update()
    {
        if (isDragging)
        {
            Vector2 currentPoint = Input.mousePosition;
            direction = (currentPoint - startPoint).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            //turn player in direction of arrow
            if (Math.Abs(angle) < 45 && !controller.m_FacingRight)
            {
                controller.Flip();
            }else if (Math.Abs(angle) > 135 && controller.m_FacingRight)
            {
                controller.Flip();
            }
            
            //restrict window, the player is able to shoot in to 90deg in front of him
            if (transform.localScale.x > 0)
            {
                angle = Mathf.Clamp(angle, -45f, 45f);
            }
            else
            {
                if (Mathf.Abs(angle) < 135f)
                {
                    angle = Mathf.Sign(angle) * 135f;
                }
            }

            // convert the angle to a Vector2 (to ensure that it represents a valid direction for the fireball)
            direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}