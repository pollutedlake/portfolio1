using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    private float walkSpeed = 3.0f;
    private float runSpeed = 10.0f;
    private float IdleTime = 0.0f;
    private float attackRate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        setUp();
        monState = monStates["Patrol"];
        maxHp = 10;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch ((int)monState)
        {
            case 0:     // Patrol
                Patrol(walkSpeed, ref IdleTime);
                break;
            case 1:
                Vector3 dirToChar = character.transform.position - transform.position;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    transform.forward = dirToChar;
                    attackRate += Time.deltaTime;
                    if (attackRate > 2.0f)
                    {
                        attackRate = 0.0f;
                        float attackPattern;
                        if (dirToChar.sqrMagnitude > 10.0f)
                        {
                            attackPattern = Random.Range(0.0f, 1.0f);
                            if (attackPattern > 0.25f)
                            {
                                animator.SetBool("Rush", true);
                            }
                            else
                            {
                                animator.SetBool("Walk", true);
                                transform.position += dirToChar.normalized * Time.deltaTime * walkSpeed;
                            }
                        }
                        else
                        {

                        }
                    }
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    transform.position += transform.forward * runSpeed * Time.deltaTime;
                }
                break;
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
