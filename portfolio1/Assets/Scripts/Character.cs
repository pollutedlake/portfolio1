using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    private InputComponent inputComponent;
    private CharacterMovement characterMovement;
    public Weapon weapon;
    private Rigidbody rigidbody;

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            velocity = characterMovement.Move(direction, transform.forward);
        }
        characterMovement.Run(inputComponent.isRun);
        if (inputComponent.mouseLBtn)
        {
            inputComponent.mouseLBtn = false;
            if (animator.GetBool("Draw Long Sword"))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("AttackWaiting"))
                {
                    attackCount++;
                    animator.SetInteger("AttackCount", attackCount);
                }
            }
            else
            {
                if(direction.sqrMagnitude > 0)
                {
                    attackCount++;
                    animator.SetBool("Draw Long Sword", true);
                    animator.SetInteger("AttackCount", attackCount);
                }
                else
                {
                    animator.SetBool("Draw Long Sword", true);
                }
            }
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            DrawWeapon();
        }
        if (inputComponent.isRoll)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
            {
                inputComponent.isRoll = false;
                animator.SetTrigger("Roll");
            }
        }
        if (fallDownEnd)
        {
            fallDownT += Time.deltaTime;
            if (fallDownT > 2.0f || input.sqrMagnitude > 0.0f)
            {
                animator.SetTrigger("Get Up");
                fallDownT = 0.0f;
                fallDownEnd = false;
            }
        }
    }

    public void DrawWeapon()
    {
        weapon.DrawWeapon();
        characterMovement.speed = characterMovement.weaponSpeed;
    }

    public void SheathWeapon()
    {
        weapon.SheathWeapon();
        characterMovement.speed = characterMovement.walkSpeed;
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Fall Down"))
        {
            animator.SetTrigger("Fall Down");
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
    }

    public void GetUp()
    {
        fallDownEnd = true;
    }
}
