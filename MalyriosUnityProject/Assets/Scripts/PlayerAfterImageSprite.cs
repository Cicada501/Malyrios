using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=ylsWcc4IP3E
public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 1f;
    private float timeActivated;
    private float alpha;
    
    [SerializeField] 
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.95f;
    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = ReferencesManager.Instance.player.transform;
        playerSR = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.transform.localScale;
        timeActivated = Time.time;

        
    }

    private void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;
        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject); //instead of destroying and then recreating the object use a pool of reused AfterImages
        }
    }

}