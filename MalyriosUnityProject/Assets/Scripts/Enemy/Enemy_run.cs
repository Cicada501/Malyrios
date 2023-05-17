using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_run : StateMachineBehaviour
{

    public float basicSpeed = 1f;
    float speed;
    private Enemy enemy;
    Rigidbody2D rb;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = basicSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb != null)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos =
                Vector2.MoveTowards(rb.position, target,
                    speed * Time.fixedDeltaTime); //last arg gives how much to move each update( how long the vector is)
            rb.MovePosition(newPos);
            //if player is in range and enemy is alive, attack
            if (Vector2.Distance(rb.position, player.position) <= enemy.attackRange && !animator.GetBool("isDead"))
            {
                if (Time.time >= enemy.nextAttackTime)
                {
                    animator.SetTrigger("Attack");
                    speed = 0f;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
