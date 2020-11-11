﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum State
    {
        Patrol, Battle, Die
    }
    public Dictionary<string, State> monStates = new Dictionary<string, State>();
    public State monState;
    public float maxHp;
    public float curHp;
    public float attack;
    public GameObject[] patrolPoint;
    private int patrolIdx = 0;
    public Animator animator;
    public Character character;

    // Start is called before the first frame update
    public void setUp()
    {
        if (animator == null)
        {
            animator = transform.GetComponent<Animator>();
        }
        monStates.Add("Patrol", State.Patrol);
        monStates.Add("Battle", State.Battle);
        monStates.Add("Die", State.Die);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Damaged(10);
            Debug.Log("test");
        }
    }

    public void Damaged(float damage)
    {
        if (curHp > 0.0f)
        {
            curHp -= damage;
            if (!(curHp > 0.0f))
            {
                Die();
            }
        }
    }

    public void Patrol(float patrolSpeed, ref float patrolWaitingT)
    {
        Vector3 direction = patrolPoint[patrolIdx].transform.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        if (direction.sqrMagnitude < 0.1f)
        {
            patrolWaitingT += Time.deltaTime;
            animator.SetBool("Walk", false);
            if (patrolWaitingT > 2.0f)
            {
                patrolWaitingT = 0.0f;
                patrolIdx++;
                patrolIdx %= patrolPoint.Length;
                animator.SetBool("Walk", true);
            }
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = lookRotation;
            transform.position += direction.normalized * Time.deltaTime * patrolSpeed;
        }
    }

    public void Shout()
    {
        monState = monStates["Battle"];
        animator.SetBool("Walk", false);
        animator.SetInteger("State", (int)monState);
        // 플레이어에게 포효알리기
        // 플레이어가 회피가능
        // 회피하지 못하면 움직이지 못함
    }

    public void Die()
    {
        curHp = 0;
        animator.SetTrigger("Die");
        monState = monStates["Die"];
    }
}
