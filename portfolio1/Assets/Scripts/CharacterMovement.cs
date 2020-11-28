using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // 속도 관련 변수
    public float runSpeed = 7.0f;
    public float speed = 5.0f;
    public float walkSpeed = 5.0f;
    public float weaponSpeed = 3.0f;
    private Vector3 velocity;

    // 상태 관련 변수
    private Animator animator;
    public bool turn180 = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    /// <summary>
    /// character 이동함수
    /// </summary>
    /// <param name="direction"> character가 이동할 방향 </param>
    /// <param name="forward"> character가 바라보고 있는 방향 </param>
    /// <returns></returns>
    public Vector3 Move(Vector3 direction, Vector3 forward)
    {
        // direction과 forward가 반대방향이라면
        if(Vector3.Dot(direction, forward) < -0.05f)        // 0.0f보다 작으면 90도 이동할 때도 되기 때문에 좀 더 작게 설정했다.
        {
            turn180 = true;
            animator.SetBool("Turn 180", true);
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
            {
                transform.position += velocity;
                animator.SetFloat("Velocity", velocity.magnitude);
            }
        }
        velocity = direction * speed * Time.deltaTime;      // 180도 돌면서 속도가 0이되어 Idle로 먼저 가기 때문에 velocity 배정을 여기서 한다.
        // 움직이는 방향으로 바라보게 한다.
        if (direction.sqrMagnitude > 0.0f && !turn180 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
        return velocity;
    }

    /// <summary>
    /// isRun이 true라면 움직이는 상태일 때 무기를 뽑아 놓은 상태면 무기를 집어 넣고 속도를 달리는 속도로 바꾸고 false라면 걷는속도로 바꾼다.
    /// </summary>
    /// <param name="isRun"> 달리는 키(LeftShift)입력이 들어왔는지 알려주는 bool 변수 </param>
    public void Run(bool isRun)
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            return;
        }
        if (isRun)
        {
            if(animator.GetBool("Draw Long Sword"))
            {
                animator.SetBool("Draw Long Sword", false);
            }
            speed = runSpeed;
            animator.SetFloat("MoveSpeed", 1.5f);
        }
        else
        {
            speed = walkSpeed;
            animator.SetFloat("MoveSpeed", 1.0f);
        }
    }
}
