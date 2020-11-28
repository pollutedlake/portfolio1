using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기 위치에 관련된 변수
    public GameObject rightHand;
    public GameObject spine1;
    public GameObject drawPosition;
    public GameObject sheathPosition;

    private Animator charAnimator;
    private CapsuleCollider capsuleCollider;

    // 무기를 뽑는 함수
    public void DrawWeapon()
    {
        transform.parent = rightHand.transform;
        transform.localPosition = drawPosition.transform.localPosition;
        transform.localRotation = drawPosition.transform.localRotation;
    }

    // 무기를 넣는 함수
    public void SheathWeapon()
    {
        transform.parent = spine1.transform;
        transform.localPosition = sheathPosition.transform.localPosition;
        transform.localRotation = sheathPosition.transform.localRotation;
    }

    // 무기를 넣고 뽑을 때 위치 초기화
    void Start()
    {
        if (charAnimator == null)
        {
            charAnimator = FindObjectOfType<Character>().GetComponent<Animator>();
        }
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }
        if (drawPosition == null)
        {
            
        }
        if (sheathPosition == null)
        {
            
        }
        transform.position = sheathPosition.transform.position;
        transform.rotation = sheathPosition.transform.rotation;
    }


    // 캐릭터 공격 시 capsuleCollder를 켰다가 아닐 때는 끈다.
    void Update()
    {
        if (charAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            capsuleCollider.enabled = true;
        }
        else
        {
            capsuleCollider.enabled = false;
        }
    }
}
