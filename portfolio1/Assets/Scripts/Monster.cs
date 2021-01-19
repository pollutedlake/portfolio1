using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Monster의 상태 관련 변수
    public enum State
    {
        Patrol, Battle, Die
    }
    public Dictionary<string, State> monStates = new Dictionary<string, State>();
    public State monState;

    // Monster의 Status 
    public float maxHp;
    public float curHp;
    public float attack;

    // Patrol 관련 변수
    public GameObject[] patrolPoint;
    private int patrolIdx = 0;

    public ObjectManager objectManager;
    public Animator animator;
    public Character character;
    public CapsuleCollider capsuleCollider;
    public UIMgr uiMgr;
    public AudioSource roarSound;       // 포효 소리
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    public void SetUp()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        if (uiMgr == null)
        {
            uiMgr = FindObjectOfType<UIMgr>();
        }
        if (animator == null)
        {
            animator = transform.GetComponent<Animator>();
        }
        if(capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }
        /*if (character == null)
        {
            character = FindObjectOfType<Character>();
        }*/
        if(roarSound == null)
        {
            roarSound = GetComponent<AudioSource>();
        }
        monStates.Add("Patrol", State.Patrol);
        monStates.Add("Battle", State.Battle);
        monStates.Add("Die", State.Die);
    }

    /// <summary>
    /// 데미지를 받을 때 호출될 함수
    /// </summary>
    /// <param name="damage"> 받을 데미지 </param>
    public void TakeDamage(float damage)
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
    /// <summary>
    /// 현재 Patrol지점까지 Patrol하는 함수
    /// </summary>
    /// <param name="patrolSpeed"></param>
    /// <param name="patrolWaitingT"></param>
    public void Patrol(float patrolSpeed, ref float patrolWaitingT, ref float footPrintT, FootPrint footPrintPrefab)
    {
        Vector3 direction = patrolPoint[patrolIdx].transform.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
        {

            if ((transform.forward - direction.normalized).sqrMagnitude < 0.01f)
            {
                animator.SetBool("Turn", false);
                animator.SetBool("Walk", true);
            }
            else
            {
                transform.forward = Vector3.Lerp(transform.forward, direction.normalized, Time.deltaTime * 3.0f);
            }
        }
        else
        {
            // Patrol지점까지 도달하면 일정 시간 대기하다가 다음 Patrol지점을 구한다.
            if (direction.sqrMagnitude < 0.1f)
            {
                patrolWaitingT += Time.deltaTime;
                animator.SetBool("Walk", false);
                if (patrolWaitingT > 2.0f)
                {
                    animator.SetBool("Turn", true);
                    patrolWaitingT = 0.0f;
                    patrolIdx++;
                    patrolIdx %= patrolPoint.Length;
                }
            }
            else
            {
                transform.position += direction.normalized * Time.deltaTime * patrolSpeed;
                footPrintT += Time.deltaTime;
                if(footPrintT > 10.0f)
                {
                    FootPrint footPrint = Instantiate(footPrintPrefab);
                    footPrint.transform.position = new Vector3(this.gameObject.transform.position.x, -5.8f, this.gameObject.transform.position.z);
                    footPrint.transform.rotation = this.transform.rotation;
                    footPrint.transform.Rotate(new Vector3(1, 0, 0), 90);
                    footPrintT = 0.0f;
                }
            }
        }
    }

    // 포효 시 호출하는 함수
    public void Shout()
    {
        monState = monStates["Battle"];
        animator.SetBool("Walk", false);
        animator.SetInteger("State", (int)monState);
        StartCoroutine("GetRoared");
    }

    // Character가 일정 범위 안에 있고 맞을 수 있는 상태라면 포효를 맞고 움직이지 못한다.
    private IEnumerator GetRoared()
    {
        yield return new WaitForSeconds(1.0f);
        if ((transform.position - character.transform.position).sqrMagnitude < 900.0f)
        {
            if (character.canHit)
            {
                character.Ducking();
            }
        }
    }

    public void Die()
    {
        curHp = 0;
        animator.SetTrigger("Die");
        monState = monStates["Die"];
    }

    // 포효 시 포효 소리를 출력하기 위해 호출되는 Event함수
    public void Roar()
    {
        roarSound.Play();
    }
}
