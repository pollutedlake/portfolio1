using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject spine1;
    public GameObject drawPosition;
    public GameObject sheathPosition;
    private Animator charAnimator;
    private CapsuleCollider capsuleCollider;

    public void DrawWeapon()
    {
        transform.parent = rightHand.transform;
        transform.localPosition = drawPosition.transform.localPosition;
        transform.localRotation = drawPosition.transform.localRotation;
    }

    public void SheathWeapon()
    {
        transform.parent = spine1.transform;
        transform.localPosition = sheathPosition.transform.localPosition;
        transform.localRotation = sheathPosition.transform.localRotation;
    }

    // Start is called before the first frame update
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

    // Update is called once per frame
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
