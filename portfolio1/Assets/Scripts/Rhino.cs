using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    private float walkSpeed = 3.0f;     // 걸을 때 속도
    private float runSpeed = 10.0f;     // 달릴 때 속도
    private float IdleTime = 0.0f;      // 가만히 있는 시간
    private float attackRate = 0.0f;    // 공격 주기
    private float rushTime = 0.0f;      // 돌진 시간
    private float attackPattern;        // 공격 패턴
    private Vector3 runDir;     // 돌진 방향

    /// <summary>
    /// animator, 몬스터의 상태들, 몬스터의 초기 상태, 최대 체력, 현재 체력 초기화
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        setUp();
        monState = monStates["Patrol"];
        maxHp = 50;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch ((int)monState)      // 몬스터의 상태에 따라
        {
            case 0:     // Patrol
                Patrol(walkSpeed, ref IdleTime);
                break;
            case 1:     // Battle
                Vector3 dirToChar = new Vector3(character.transform.position.x - transform.position.x, 0, character.transform.position.z - transform.position.z);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    IdleTime += Time.deltaTime;
                    if (IdleTime > 2.0f)
                    {
                        IdleTime = 0.0f;
                        attackPattern = Random.Range(0.0f, 1.0f);
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
                        }
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && other.gameObject.CompareTag("Player"))
        {
            if (!character.canHit)
            {
                return;
            }
            Vector3 damagedVec = new Vector3(other.ClosestPoint(capsuleCollider.center).x - capsuleCollider.center.x, character.transform.position.y, other.ClosestPoint(capsuleCollider.center).z - capsuleCollider.center.z);
            character.TakeDamage(20.0f, damagedVec);
        }
        if (other.gameObject.CompareTag("Weapon"))
        {
            Damaged(10.0f);
            uiMgr.ShowDamage(other.ClosestPoint(capsuleCollider.center), 10.0f);
            if (monState == monStates["Patrol"])
            {
                Shout();
            }
        }
        if (other.gameObject.CompareTag("Slinger"))
        {
            Destroy(other.gameObject);
            Damaged(2.0f);
            uiMgr.ShowDamage(other.ClosestPoint(capsuleCollider.center), 2.0f);
            if (monState == monStates["Patrol"])
            {
                Shout();
            }
        }
    }

    private void Rush()
    {
        animator.SetBool("Rush", true);
    }

    private void HeadButt()
    {
        animator.SetTrigger("HeadButt");
    }
}
