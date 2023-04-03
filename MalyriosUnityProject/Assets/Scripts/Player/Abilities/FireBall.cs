using System;
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
    private Vector2 direction;

    private void Start()
    {
        abilityButtonImage = GameObject.Find("ButtonFireball").GetComponent<Image>();
        player = ReferencesManager.Instance.player;
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
    
    public void OnPointerDown(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        startPoint = eventData.position;
        print("ArrowPrefab:"+arrowPrefab);
        arrowInstance = Instantiate(arrowPrefab, player.transform.position, Quaternion.identity);
        isDragging = true;
    }
    
    private void Update()
    {
        if (isDragging)
        {
            Vector2 currentPoint = Input.mousePosition;
            direction = (currentPoint - startPoint).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}