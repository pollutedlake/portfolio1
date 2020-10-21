using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject spine1;
    public GameObject drawPosition;
    public GameObject sheathPosition;
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

    }
}
