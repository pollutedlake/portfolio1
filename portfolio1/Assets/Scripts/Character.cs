using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    private InputComponent inputComponent;
    private CharacterMovement characterMovement;
    public Weapon weapon;

    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;

    Vector3 velocity = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        if(animator== null)
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = inputComponent.input;
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;      // 카메라기준으로 forword벡터 생성
        Vector3 right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;        // 카메라기준으로 right벡터 생성
        Vector3 direction = (forward * input.y + right * input.x).normalized;       // 카메라 기준으로 입력받은 방향 계산
        if (characterMovement.Turn180(direction, transform.forward))
        {
            animator.SetTrigger("Turn 180");
        }
        else
        {
            if (characterMovement.Walk(velocity))
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
            if (characterMovement.Run())
            {
                characterMovement.speed = runSpeed;
                animator.SetBool("Run", true);
            }
            else
            {
                characterMovement.speed = walkSpeed;
                animator.SetBool("Run", false);
            }
        }
        velocity = characterMovement.Move(direction.normalized);        // Character이동
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
}
