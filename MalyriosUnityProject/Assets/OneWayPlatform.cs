using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayPlatform : MonoBehaviour
{
    private TilemapCollider2D platformCollider;
    private GameObject player;
    private Rigidbody2D rb;
    private bool collidedWithPlayer = false;
    private bool grounded;

    void Start()
    {
        platformCollider = GetComponent<TilemapCollider2D>();
        player = ReferencesManager.Instance.player;
        rb = player.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        grounded = player.GetComponent<CharacterController2D>().m_Grounded;
        
        if (rb.velocity.y < 0 && !grounded || collidedWithPlayer)
        {
            
            //Collide
            collidedWithPlayer = true;
            platformCollider.isTrigger = false;
            
        }
        else
        {
            //do not collide
            platformCollider.isTrigger = true;
        }

        if (IsInTargetAnimationState())
        {
            collidedWithPlayer = false;
        }
        print(collidedWithPlayer+" grounded:? "+grounded);
    }
    
    private bool IsInTargetAnimationState()
    {
        AnimatorStateInfo currentStateInfo = player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        return currentStateInfo.IsName("jumpUp");
    }


}