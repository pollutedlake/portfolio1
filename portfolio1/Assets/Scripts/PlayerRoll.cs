using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : StateMachineBehaviour
{
    private Character character;
    private InputComponent inputComponent;
    private Vector3 direction;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidbody;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
        {
            character = animator.gameObject.GetComponent<Character>();
        }
        if (inputComponent == null)
        {
            inputComponent = character.GetComponent<InputComponent>();
        }
        if(capsuleCollider == null)
        {
            capsuleCollider = character.GetComponent<CapsuleCollider>();
        }
        if (rigidbody == null)
        {
            rigidbody = character.GetComponent<Rigidbody>();
        }
        direction = character.direction;
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
        character.canHit = false;
        //capsuleCollider.enabled = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.canHit = true;
        //capsuleCollider.enabled = true;
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
