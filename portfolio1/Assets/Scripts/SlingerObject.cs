using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingerObject : InteractiveObject
{
    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (projectilePrefab == null)
        {
            Destroy(this.gameObject);
        }
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
    }

    public Projectile Interact(GameObject leftHand)
    {
        return PickUp(leftHand);
    }

    // 슬링어를 줍는 함수(슬링어를 생성해 왼손 위에 위치시킨다.)
    public Projectile PickUp(GameObject leftHand)
    {
        Projectile projectile = Instantiate(projectilePrefab, leftHand.transform);
        projectile.transform.localPosition = new Vector3(0.0f, 0.05f, 0.0f);
        return projectile;
    }
}
