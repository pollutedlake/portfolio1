using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float runSpeed = 5.0f;
    public float speed = 3.0f;
    public float walkSpeed = 3.0f;
    private Animator animator;

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
    /// <param name="direction"> character가 이동할 방향</param>
    /// <returns></returns>
    //public Vector3 Move(Vector3 direction, bool turn, int attackCount)
    public Vector3 Move(Vector3 direction)
    {
        Vector3 velocity = direction * speed * Time.deltaTime;

        if (direction.sqrMagnitude > 0.0f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }

        //if (!turn && attackCount == 0)
        //{
        //    transform.position += velocity;
        //}
        transform.position += velocity;
        animator.SetFloat("Velocity", velocity.magnitude);
        return velocity;
    }

    public bool Walk(Vector3 velocity)
    {
        if (velocity.sqrMagnitude > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Turn180(Vector3 direction, Vector3 forward)
    {
        if(Vector3.Dot(direction, forward) < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Run(bool isRun)
    {
        if (isRun)
        {
            speed = runSpeed;
            animator.SetBool("Run", true);
        }
        else
        {
            speed = walkSpeed;
            animator.SetBool("Run", false);
        }
    }
}
