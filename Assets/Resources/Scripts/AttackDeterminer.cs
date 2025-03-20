using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDeterminer : StateMachineBehaviour
{
    private int lightAttackIndex = -1; 
    private int heavyAttackIndex = -1;
    const int lightAttackCount = 1;
    const int heavyAttackCount = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int lightAttack = animator.GetInteger("LightAttack");
        int heavyAttack = animator.GetInteger("HeavyAttack");

        if (lightAttack > 0) 
        {
            // For attack variations later if we want
            // lightAttackIndex = (lightAttackIndex < (lightAttackCount - 1)) ? lightAttackIndex + 1 : 0;
            // animator.SetInteger("LightAttackIndex", lightAttackIndex);
            animator.SetInteger("LightAttackIndex", 0);
        }
        else if (heavyAttack > 0) 
        {
            // heavyAttackIndex = (heavyAttackIndex < (heavyAttackCount - 1)) ? heavyAttackIndex + 1 : 0;
            // animator.SetInteger("HeavyAttackIndex", heavyAttackIndex);
            animator.SetInteger("HeavyAttackIndex", 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
