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
    private Animator enemyAnimator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
        player = ReferencesManager.Instance.player.transform;
        speed = basicSpeed;
        enemyAnimator = animator;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.canMove && rb != null && !player.GetComponent<PlayerHealth>().isDead)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 direction = (target - rb.position).normalized;
            float raycastLength = .4f;

            // Check for wall or cliff in front of the enemy
            if (IsWallInFront(direction, raycastLength) ||IsCliffInFront(direction, raycastLength) )
            {
                animator.SetBool("inFrontOfWall", true);
                return;
            }
            
            MoveTowardsPlayer(target);
            AttemptToAttackPlayer();
        }
    }

    private bool IsWallInFront(Vector2 direction, float raycastLength)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(rb.position, direction, raycastLength);

        // Draw ray for debugging
        Debug.DrawRay(rb.position, direction * raycastLength, Color.red);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCliffInFront(Vector2 direction, float raycastLength)
    {
        Vector2 groundCheckPosition = rb.position + direction * raycastLength;
        Vector2 groundDirection = Vector2.down;
        float groundRaycastLength = .5f;

        RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPosition, groundDirection, groundRaycastLength);

        // Draw ray for debugging
        Debug.DrawRay(groundCheckPosition, groundDirection * groundRaycastLength, Color.green);

        return groundHit.collider == null;
    }

    private void MoveTowardsPlayer(Vector2 target)
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    private void AttemptToAttackPlayer()
    {
        
    
        if (!enemyAnimator.GetBool("isDead"))
        {
            if (enemy.isRanged && enemy.distToPlayer is > 1f and <= 5f)
            {
                if (Time.time >= enemy.nextAttackTime)
                {
                    enemyAnimator.SetTrigger("RangedAttack");
                    enemy.SetNextAttackTime();
                    speed = 0f; 
                }
            }
            else if (enemy.distToPlayer <= enemy.attackRange)
            {
                if (Time.time >= enemy.nextAttackTime)
                {
                    enemyAnimator.SetTrigger("Attack");
                    enemy.SetNextAttackTime();
                    speed = 0f;
                }
            }
        }
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}