using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaiting : StateMachineBehaviour
{
    private float attackRate = 0.0f;
    private int attackCount;
    private bool attackRock = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackRate = 0.0f;
        attackRock = false;
        attackCount = animator.GetInteger("AttackCount");
        animator.SetInteger("AttackCount", -1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackRate += Time.deltaTime;
        if (attackRate < 1.5f)
        {
            if (Input.GetMouseButtonDown(0) && !attackRock)
            {
                attackRock = true;
                switch (++attackCount % 3)
                {
                    case 0:
                        animator.SetInteger("AttackCount", 3);
                        break;
                    case 1:
                        animator.SetInteger("AttackCount", 4);
                        break;
                    case 2:
                        animator.SetInteger("AttackCount", 2);
                        break;
                }
            }
        }
        else
        {
            animator.SetInteger("AttackCount", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetNextAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetInteger("AttackCount", 0);
        }
    }

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
