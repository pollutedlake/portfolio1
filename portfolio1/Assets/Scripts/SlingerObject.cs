using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingerObject : MonoBehaviour
{
    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (projectilePrefab == null)
        {
            Destroy(this.gameObject);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Projectile Interact(GameObject leftHand)
    {
        return PickUp(leftHand);
    }

    public Projectile PickUp(GameObject leftHand)
    {
        Projectile projectile = Instantiate(projectilePrefab, leftHand.transform);
        projectile.transform.localPosition = new Vector3(0.0f, 0.05f, 0.0f);
        return projectile;
    }
}
