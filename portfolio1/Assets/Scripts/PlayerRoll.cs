using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : StateMachineBehaviour
{
    private Character character;
    private Vector3 direction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
        {
            character = animator.gameObject.GetComponent<Character>();
        }
        direction = character.direction;
        // 구르기 시 이동 입력이 없다면 Character가 바라보는 방향을 구를 방향으로 설정
        if(!(direction.sqrMagnitude > 0.0f))
        {
            direction = character.transform.forward;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 velocity = direction * 3.0f * Time.deltaTime;
        character.transform.position += velocity;

        // 구르기 시 맞지 않는다.
        character.canHit = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 구르기가 끝나면 다시 맞을 수 있는 상태로 변환
        character.canHit = true;
    }
}
