using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float runSpeed = 7.0f;
    public float speed = 5.0f;
    public float walkSpeed = 5.0f;
    public float weaponSpeed = 3.0f;
    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private Vector3 velocity;
    public bool turn180 = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }
    }

    /// <summary>
    /// character 이동함수
    /// </summary>
    /// <param name="direction"> character가 이동할 방향</param>
    /// <returns></returns>
    //public Vector3 Move(Vector3 direction, bool turn, int attackCount)
    public Vector3 Move(Vector3 direction, Vector3 forward)
    {
        if(Vector3.Dot(direction, forward) < 0)
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
        velocity = direction * speed * Time.deltaTime;
        if (direction.sqrMagnitude > 0.0f && !turn180 && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Not Move"))
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
        return velocity;
    }

    public void Run(bool isRun)
    {
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
