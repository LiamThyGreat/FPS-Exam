using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIdleBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SwordAttackScript.instance.isAttacking = false;
        SwordAttackScript.instance.canRecieveInput = true;
        SwordAttackScript.instance.RemoveCollider();

        if (SwordAttackScript.instance.isGrounded != true)
        {
            SwordAttackScript.instance.movement.enabled = true;
            SwordAttackScript.instance.rb.useGravity = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SwordAttackScript.instance.inputRecieved)
        {
            SwordAttackScript.instance.EnableCollider();
            SwordAttackScript.instance.isAttacking = true;
            SwordAttackScript.instance.InputManager();
            animator.SetTrigger("Attack1");
        }
       
    }

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
