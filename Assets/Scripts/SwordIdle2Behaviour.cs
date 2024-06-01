using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIdle2Behaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SwordAttackScript.instance.RemoveCollider();
        SwordAttackScript.instance.canRecieveInput = true;
        SwordAttackScript.instance.isAttacking = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SwordAttackScript.instance.inputRecieved)
        {
            SwordAttackScript.instance.EnableCollider();
            SwordAttackScript.instance.isAttacking = true;
            SwordAttackScript.instance.InputManager();
            //SwordAttackScript.instance.inputRecieved = false;
            animator.SetTrigger("Attack3");
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
