using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : StateMachineBehaviour {

	Rigidbody2D rb;
    Transform player;
    private Enemy enemy;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		rb = animator.GetComponent<Rigidbody2D>();
		enemy = animator.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Vector2.Distance(rb.position, player.position) <= enemy.attackRange &&!animator.GetBool("isDead"))
        {
            if(Time.time>=enemy.nextAttackTime){
                animator.SetTrigger("Attack"); 
            }
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
