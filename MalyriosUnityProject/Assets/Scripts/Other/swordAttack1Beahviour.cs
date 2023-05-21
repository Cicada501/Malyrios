using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAttack1Beahviour : StateMachineBehaviour
{
    public static bool swordActive;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swordActive = true;
        //Debug.Log("swordActive");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swordActive = false;
    }

}
