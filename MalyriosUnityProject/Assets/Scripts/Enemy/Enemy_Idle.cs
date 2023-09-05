using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform player;
    private Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        player = ReferencesManager.Instance.player.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb != null && !animator.GetBool("isDead"))
        {
            Debug.Log($"passed: rb != null and not dead dist to player is: {enemy.distToPlayer} ({enemy.gameObject.name})");

            if (enemy.isRanged && enemy.distToPlayer is > 1f and <= 5f)
            {
                Debug.Log($"passed: isRanged and Dist check({enemy.gameObject.name})");
                if (Time.time >= enemy.nextAttackTime)
                {
                    Debug.Log($"passed: nextAttacktime, attack triggeried({enemy.gameObject.name})");
                    animator.SetTrigger("RangedAttack");
                    enemy.SetNextAttackTime();
                }
            }
            else if (enemy.distToPlayer <= enemy.attackRange)
            {
                if (Time.time >= enemy.nextAttackTime)
                {
                    animator.SetTrigger("Attack");
                    enemy.SetNextAttackTime();
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}