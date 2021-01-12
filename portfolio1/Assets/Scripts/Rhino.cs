using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    // 이동 속도 관련 변수
    private float walkSpeed = 3.0f;
    private float runSpeed = 10.0f;

    // 공격 관련 변수
    private float IdleTime = 0.0f;    
    private float attackRate = 0.0f;    
    private float rushTime = 0.0f;      
    private float attackPattern;        
    private Vector3 runDir;     

    private Ray areaCheckRay;
    public FootPrint footPrintPrefab;
    private float footPrintT = 0.0f;

    /// <summary>
    /// animator, Monster의 상태들, 초기 상태, 최대 체력, 현재 체력 초기화
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        monState = monStates["Patrol"];
        maxHp = 50;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch (monState)      // Monster의 상태에 따라
        {
            case State.Patrol:
                Patrol(walkSpeed, ref IdleTime, ref footPrintT, footPrintPrefab);
                
                break;
            case State.Battle:
                //Character까지의 방향
                Vector3 dirToChar = new Vector3(character.transform.position.x - transform.position.x, 0, character.transform.position.z - transform.position.z);

                // 가만히 있을 때 일정 주기마다 랜덤으로 거리에 따라 패턴이 다르다.
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    IdleTime += Time.deltaTime;
                    if (IdleTime > 2.0f)
                    {
                        IdleTime = 0.0f;
                        attackPattern = Random.Range(0.0f, 1.0f);

                        // 거리가 가깝다면 박치기나 돌진 중 사용
                        if (dirToChar.sqrMagnitude < 100.0f)
                        {
                            if (attackPattern > 0.4f)
                            {
                                animator.SetTrigger("HeadButt");
                            }
                            else if (attackPattern < 0.1f)
                            {
                                runDir = dirToChar.normalized;
                                transform.forward = runDir;
                                animator.SetBool("Rush", true);
                            }
                            else
                            {
                                animator.SetBool("Turn", true);
                            }
                        }

                        // 거리가 멀다면 돌진이나 Character를 향해 걸어간다.
                        else
                        {
                            if (attackPattern > 0.25f)
                            {
                                animator.SetBool("Walk", true);
                            }
                            else
                            {
                                runDir = dirToChar.normalized;
                                transform.forward = runDir;
                                animator.SetBool("Rush", true);
                            }
                        }
                    }
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
                {
                    if ((transform.forward - dirToChar.normalized).sqrMagnitude < 0.1f)
                    {
                        animator.SetBool("Turn", false);
                    }
                    else
                    {
                        transform.forward = Vector3.Lerp(transform.forward, dirToChar.normalized, Time.deltaTime * 3.0f);
                    }
                }

                // 걷고 있을 때는 일정 거리 이내에 들어오면 멈추고 일정거리 이상이라면 공격주기마다 랜덤으로 돌진을 사용한다.
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    transform.forward = dirToChar.normalized;
                    if (dirToChar.sqrMagnitude < 100.0f)
                    {
                        animator.SetBool("Walk", false);
                    }
                    else if (dirToChar.sqrMagnitude > 1600.0f)
                    {
                        attackRate += Time.deltaTime;
                        if (attackRate > 2.0f)
                        {
                            attackRate = 0.0f;
                            runDir = transform.forward;
                            animator.SetBool("Rush", true);
                            animator.SetBool("Walk", false);
                        }
                    }
                    transform.position += transform.forward * Time.deltaTime * walkSpeed;
                }

                // 돌진상태라면 일정시간까지 계속 돌진하고 지나면 돌진이 끝난다.
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    rushTime += Time.deltaTime;
                    transform.position += runDir * runSpeed * Time.deltaTime;
                    if (rushTime > 5.0f)
                    {
                        rushTime = 0.0f;
                        animator.SetBool("Rush", false);
                    }
                }
                break;
            case State.Die:
                capsuleCollider.center = new Vector3(-0.7f, 0, capsuleCollider.center.z);
                break;
        }
        areaCheckRay = new Ray(transform.position, new Vector3(0.0f, -2.0f, 0.0f));
        RaycastHit hitInfo;
        bool isArea = Physics.Raycast(areaCheckRay, out hitInfo, 2.0f, 1 << 9);
        if (isArea)
        {
            string areaName = hitInfo.collider.name.ToString();
            GameManager.instance.curMonsterArea[0] = (int)areaName[areaName.Length - 1] - 48;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player와 부딪혔을 때 밀리지 않게 하기 위해서
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidbody.isKinematic = true;
        }
        else
        {
            rigidbody.isKinematic = false;
        }

        // 돌진 중 Player와 부딪혔을 때 Character가 맞을 수 있는 상태가 아니라면 return
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && collision.gameObject.CompareTag("Player"))
        {
            if (!character.canHit)
            {
                return;
            }
            // Character가 데미지를 받는 방향
            Vector3 damagedVec = new Vector3(collision.collider.ClosestPoint(capsuleCollider.center).x - capsuleCollider.center.x, character.transform.position.y, collision.collider.ClosestPoint(capsuleCollider.center).z - capsuleCollider.center.z);
            
            character.TakeDamage(20.0f, damagedVec);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Weapon이라면 데미지를 받고 데미지를 화면에 띄워주고 Patrol상태였다면 포효를 시전한다.
        if (other.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(10.0f);
            uiMgr.ShowDamage(other.ClosestPoint(capsuleCollider.center), 10.0f);
            objectManager.ShowHitEffect(other.ClosestPoint(capsuleCollider.center), "Hit");
            if (monState == monStates["Patrol"])
            {
                Shout();
            }
        }
        // Slinger이라면 Slinger Destroy한다.
        if (other.gameObject.CompareTag("Slinger"))
        {
            Destroy(other.gameObject);
            TakeDamage(2.0f);
            uiMgr.ShowDamage(other.ClosestPoint(capsuleCollider.center), 2.0f);
            if (monState == monStates["Patrol"])
            {
                Shout();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(areaCheckRay);
    }
}
