using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Animator animator;
    private InputComponent inputComponent;
    public Weapon weapon;
    private Rigidbody rigidbody;
    public CapsuleCollider capsuleCollider;

    // 슬리어와 관련된 변수
    private Projectile slinger;
    private int slingerN;
    public GameObject leftHand;

    // 이동과 관련된 변수
    Vector3 velocity = new Vector3();
    public Vector3 direction = new Vector3();
    private CharacterMovement characterMovement;

    // 공격과 관련된 변수
    private int attackCount = 0;

    // 체력과 스태미나 관련 변수
    private float maxHp;
    private float curHp;
    private float maxStamina;
    private float curStamina;
    public RawImage hpBar;
    public RawImage staminaBar;

    // 데미지를 받는 것과 관련된 변수
    private float fallDownT = 0.0f;
    private bool fallDownEnd = false;
    public bool canHit = true;

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
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.enabled = true;
        }
        maxHp = 100.0f;
        curHp = maxHp;
        maxStamina = 100.0f;
        curStamina = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        attackCount = animator.GetInteger("AttackCount");

        // 캐릭터 이동입력받아 카메라 기준으로 방향 계산 후 움직일 수 있는 상태일 때 움직인다.
        Vector2 input = inputComponent.input;
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; 
        Vector3 right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        direction = (forward * input.y + right * input.x).normalized;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            velocity = characterMovement.Move(direction, transform.forward);
        }

        // leftShft입력이 들어왔을 때
        characterMovement.Run(inputComponent.isRun);

        // 스태미나 관련 움직임
        // 달리고 있을 때는 스태미나 감소 스태미나가 다 없어지면 탈진상태가 된다. 아닐 경우에는 최대수치까지 스태미나가 찬다.
        if (animator.GetFloat("MoveSpeed") == 1.5f && animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            curStamina -= Time.deltaTime * 10.0f;
            if (curStamina < 0.0f)
            {
                curStamina = 0.0f;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Exhausted"))
                {
                    animator.SetTrigger("Drained");
                    Exhausted();
                }
            }
        }
        else
        {
            if (curStamina < maxStamina)
            {
                curStamina += Time.deltaTime * 15.0f;
            }
            else
            {
                curStamina = maxStamina;
            }
        }
        staminaBar.rectTransform.localScale = new Vector3((float)curStamina / (float)maxStamina, 1.0f, 1.0f);

        // 마우스 왼클릭 시
        if (inputComponent.mouseLBtn)
        {
            inputComponent.mouseLBtn = false;
            // 무기를 뽑은 상태이고 움직이는 상황이라면 공격
            if (animator.GetBool("Draw Long Sword"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move") && !animator.GetCurrentAnimatorStateInfo(0).IsName("AttackWaiting"))
                {
                    attackCount++;
                    animator.SetInteger("AttackCount", attackCount);
                }
            }
            // 무기를 뽑지 않은 상태에서는 움직일 때는 무기를 뽑고 공격하고 가만히 있을 때는 무기만 뽑는다.
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

        // 무기를 뽑지 않고 움직이다 공격 시 무기가 뽑히지 않아 공격 시 강제로 무기 뽑게 한다.
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            DrawWeapon();
        }

        // 구르기 입력시 움직이는 상태일 때 스태미나를 소비하면서 구른다. 스태미나가 충분하지 않을 때는 구르지 않는다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
            {
                if(!(curStamina < 20.0f))
                {
                    curStamina -= 20.0f;
                    animator.SetBool("IsRoll", true);
                }
            }
        }
        else
        {
            animator.SetBool("IsRoll", false);
        }

        // 넘어지는게 끝났을 때 충분한 시간이 지나거나 이동 입력이 들어오면 자동으로 일어난다.
        if (fallDownEnd)
        {
            fallDownT += Time.deltaTime;
            if (fallDownT > 2.0f || input.sqrMagnitude > 0.0f)
            {
                canHit = true;
                animator.SetTrigger("Get Up");
                fallDownT = 0.0f;
                fallDownEnd = false;
            }
        }

        // 움직이고 있을 때 조준 키(C)를 입력한 상태에서 발사 키(R)를 입력하면 슬링어를 가지고 있는 상태에서 슬링어를 쏘고 가지고 있는 슬링어 개수 감소하고 다 쏘면 Destroy한다.
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            if (Input.GetKey(KeyCode.C))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (slinger != null)
                    {
                        animator.SetTrigger("Shoot");
                        slinger.Shoot();
                        slingerN--;
                        if (slingerN < 1)
                        {
                            Destroy(slinger);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 스태미나를 다 썼을 때 호출되어 지친 상태로 변동시키는 함수
    /// </summary>
    public void Exhausted()
    {
        animator.SetBool("Exhausted", true);
        StartCoroutine("EndExhausted");
    }

    /// <summary>
    /// 일정 시간 후 지친 상태가 끝나는 함수
    /// </summary>
    private IEnumerator EndExhausted()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Exhausted", false);
    }

    /// <summary>
    /// 무기를 뽑고 이동속도를 감소시키는 함수
    /// </summary>
    public void DrawWeapon()
    {
        weapon.DrawWeapon();
        characterMovement.speed = characterMovement.weaponSpeed;
    }


    /// <summary>
    /// 무기를 집어놓고 이동속도를 보통 걷는 속도로 변화시키는 함수
    /// </summary>
    public void SheathWeapon()
    {
        weapon.SheathWeapon();
        characterMovement.speed = characterMovement.walkSpeed;
    }


    /// <summary>
    /// 180도 도는게 끝났을 때 호출할 Event함수
    /// </summary>
    public void TurnEnd()
    {
        Quaternion lookRotation = Quaternion.LookRotation(-transform.forward);
        transform.rotation = lookRotation;
        characterMovement.turn180 = false;
        animator.SetBool("Turn 180", false);
    }


    /// <summary>
    /// 데미지를 받았을 때 호출할 함수
    /// </summary>
    /// <param name="damage"> 받는 데미지 </param>
    /// <param name="damagedVec"> 캐릭터가 데미지를 받고 날아갈 방향 </param>
    public void TakeDamage(float damage, Vector3 damagedVec)
    {
        // 데미지를 받을 수 있는 상태가 아니라면 return
        if (!canHit)
        {
            return;
        }
            animator.SetTrigger("Fall Down");
            if (curHp > 0)
            {
                canHit = false;
                curHp -= damage;
                rigidbody.AddForce(damagedVec.normalized * damage, ForceMode.Impulse);       // 데미지를 받으면 캐릭터가 날라간다.
            }
            if (!(curHp > 0))
            {
                // 사망 이벤트
            }
            hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1.0f, 1.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        // SlingerObject에 닿아있고 F키를 입력하면 slinger를 줍는다.
        if (other.CompareTag("SlingerObject"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                slinger = other.GetComponent<SlingerObject>().Interact(leftHand);
                slingerN = 20;
            }
        }
    }

    // 포효를 맞을 때 호출되는 함수
    public void Ducking()
    {
        animator.SetTrigger("Ducking");
    }

    // 넘어지는 애니메이션이 끝나면 호출되는 Event함수
    public void GetUp()
    {
        fallDownEnd = true;
    }
}
