using System.Collections;
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
    public CapsuleCollider capsuleCollider;
    public UIMgr uiMgr;
    public AudioSource roarSound;

    // Start is called before the first frame update
    public void setUp()
    {
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
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
        if(roarSound == null)
        {
            roarSound = GetComponent<AudioSource>();
        }
        monStates.Add("Patrol", State.Patrol);
        monStates.Add("Battle", State.Battle);
        monStates.Add("Die", State.Die);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine("GetRoared");
    }

    private IEnumerator GetRoared()
    {
        yield return new WaitForSeconds(1.0f);
        if ((transform.position - character.transform.position).sqrMagnitude < 900.0f)
        {
            if (character.capsuleCollider.enabled)
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

    public void Roar()
    {
        roarSound.Play();
    }
}
