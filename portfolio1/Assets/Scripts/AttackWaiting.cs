using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaiting : StateMachineBehaviour
{
    private float attackRate = 0.0f;        //  공격이 들어오기까지 기다리는 시간
    private int attackCount;        // 몇번째 공격인지 카운트하기 위한 변수
    private bool attackRock = false;        // 입력이 한번만 들어오게 하기위한 bool 변수

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 변수들 초기화
        attackRate = 0.0f;
        attackRock = false;
        attackCount = animator.GetInteger("AttackCount");
        animator.SetInteger("AttackCount", -1);     // animator state 변동을 막기 위해서
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
        // 공격이 들어오지 않으면 animator의 AttackCount변수가 0으로 초기화되어 Idle상태로 돌아간다.
        else
        {
            animator.SetInteger("AttackCount", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator의 state가 공격이 아닌 상태로 가면 AttackCount를 0으로 초기화한다.
        if(!animator.GetCurrentAnimatorStateInfo(layerIndex).IsTag("Attack"))
        {
            animator.SetInteger("AttackCount", 0);
        }
    }
}
