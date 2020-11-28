using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadButt : MonoBehaviour
{

    private Animator rhinoAnimator;
    private CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (rhinoAnimator == null)
        {
            rhinoAnimator = FindObjectOfType<Rhino>().GetComponent<Animator>();
        }
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 박치기 공격 시 capsuleCollider 켰다가 아닐 때는 끈다.
        if (rhinoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            capsuleCollider.enabled = true;
        }
        else
        {
            capsuleCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player에 맞고 Player가 맞을 수 있는 상태이면 데미지를 준다.
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.GetComponent<Character>().canHit)
            {
                return;
            }
            Character character = other.GetComponent<Character>();
            // 플레이어가 데미지를 받을 방향을 계산한다.
            Vector3 damagedVec = new Vector3(other.ClosestPoint(capsuleCollider.center).x - capsuleCollider.center.x, character.transform.position.y, other.ClosestPoint(capsuleCollider.center).z - capsuleCollider.center.z);
            
            character.TakeDamage(10.0f, damagedVec);
        }
    }
}