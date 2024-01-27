using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNextIdle : StateMachineBehaviour
{

    // Note - these are the cutoff values
    private float cutoffChanceOfCoreIdle = 0.40f;
    private float cutoffChanceOfAltIdle1 = 0.50f;
    private float cutoffChanceOfAltIdle2 = 0.65f;
    private float cutoffChanceOfAltIdle3 = 0.75f;
    private float cutoffChanceOfAltIdle4 = 0.80f;
    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Percent chance of playing core animation
        if (Random.value < cutoffChanceOfCoreIdle)
        {
            animator.SetInteger("NextIdle", 0);
        }
        else if (Random.value < cutoffChanceOfAltIdle1)
        {
            animator.SetInteger("NextIdle", 1);
        }
        else if (Random.value < cutoffChanceOfAltIdle2)
        {
            animator.SetInteger("NextIdle", 2);
        }
        else if (Random.value < cutoffChanceOfAltIdle3)
        {
            animator.SetInteger("NextIdle", 3);
        }
        else if (Random.value < cutoffChanceOfAltIdle4)
        {
            animator.SetInteger("NextIdle", 4);
        } else
        {
            animator.SetInteger("NextIdle", 5);
        }

        animator.speed = 1.0f + (Random.Range(-0.15f, 0.25f));
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
