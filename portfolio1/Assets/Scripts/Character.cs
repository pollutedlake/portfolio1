﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    private InputComponent inputComponent;
    private CharacterMovement characterMovement;
    public Weapon weapon;
    private Rigidbody rigidbody;

    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;

    Vector3 velocity = new Vector3();
    public Vector3 direction = new Vector3();

    private int attackCount = 0;
    private float attackRate = 0.0f;

    private float maxHp;
    private float curHp;

    private float fallDownT = 0.0f;
    private bool fallDownEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (inputComponent == null)
        {

            inputComponent = GetComponent<InputComponent>();
        }
        if (characterMovement == null)
        {

            characterMovement = GetComponent<CharacterMovement>();
        }
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        maxHp = 100;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        attackCount = animator.GetInteger("AttackCount");
        Vector2 input = inputComponent.input; // 입력받은 이동 가져온다.
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;      // 카메라기준으로 forword벡터 생성
        Vector3 right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;        // 카메라기준으로 right벡터 생성
        direction = (forward * input.y + right * input.x).normalized;       // 카메라 기준으로 입력받은 방향 계산
        velocity = characterMovement.Move(direction, transform.forward);
        characterMovement.Run(inputComponent.isRun);
        if (inputComponent.mouseLBtn)
        {
            inputComponent.mouseLBtn = false;
            if (animator.GetBool("Draw Long Sword"))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {
                    attackCount++;
                    animator.SetInteger("AttackCount", attackCount);
                }
            }
            else
            {
                if(direction.sqrMagnitude > 0)
                {
                    
                }
                else
                {
                    animator.SetBool("Draw Long Sword", true);
                    characterMovement.speed = characterMovement.weaponSpeed;
                }
            }
        }
        //characterMovement.Turn180(direction, transform.forward);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("Roll", true);
        //}
        //if (characterMovement.Turn180(direction, transform.forward))
        //{
        //    animator.SetBool("Turn 180", true);
        //}
        //else
        //{
        //    if (characterMovement.Walk(velocity))
        //    {
        //        animator.SetBool("Walk", true);
        //    }
        //    else
        //    {
        //        animator.SetBool("Walk", false);
        //    }
        //    if (characterMovement.Run())
        //    {
        //        characterMovement.speed = runSpeed;
        //        animator.SetBool("Run", true);
        //    }
        //    else
        //    {
        //        characterMovement.speed = walkSpeed;
        //        animator.SetBool("Run", false);
        //    }
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (animator.GetBool("Walk"))
        //    {
        //        attackCount++;
        //        animator.SetBool("Draw Long Sword", true);
        //        DrawWeapon();
        //        animator.SetInteger("AttackCount", attackCount);
        //    }
        //    else if (animator.GetBool("Draw Long Sword") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        //    {
        //        attackCount++;
        //        animator.SetInteger("AttackCount", attackCount);
        //    }
        //    else
        //    {
        //        animator.SetBool("Draw Long Sword", true);
        //    }
        //}
        //if (animator.GetBool("Draw Long Sword") && Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    animator.SetBool("Draw Long Sword", false);
        //}
        //if (!animator.GetBool("Roll"))
        //{
        //    velocity = characterMovement.Move(direction.normalized, animator.GetBool("Turn 180"), attackCount);        // Character이동
        //}
        //if(fallDownEnd)
        //{
        //    fallDownT += Time.deltaTime;
        //    if (fallDownT > 2.0f || input.sqrMagnitude > 0.0f)
        //    {
        //        animator.SetBool("Fall Down", false);
        //        fallDownT = 0.0f;
        //        fallDownEnd = false;
        //    }
        //}
    }

    public void DrawWeapon()
    {
        weapon.DrawWeapon();
        characterMovement.speed = 1.0f;
    }

    public void SheathWeapon()
    {
        weapon.SheathWeapon();
        characterMovement.speed = walkSpeed;
    }

    public void TurnEnd()
    {
        Quaternion lookRotation = Quaternion.LookRotation(-transform.forward);
        transform.rotation = lookRotation;
        characterMovement.turn180 = false;
        animator.SetBool("Turn 180", false);
    }

    public void TakeDamage(float damage, Vector3 damagedVec)
    {
        animator.SetBool("Fall Down", true);
        if (curHp > 0)
        {
            curHp -= damage;
            transform.forward = -damagedVec.normalized;
            rigidbody.AddForce(-transform.forward * 5.0f, ForceMode.Impulse);
        }
        if (!(curHp > 0))
        {
            // 죽는 모션
        }
    }

    public void GetUp()
    {
        fallDownEnd = true;
    }
}
