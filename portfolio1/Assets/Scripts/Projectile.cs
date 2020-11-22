using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public Projectile projectile;
    public Aiming aiming;
    private bool shoot = false;
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (aiming == null)
        {
            aiming = FindObjectOfType<UIMgr>().transform.GetChild(0).GetComponent<Aiming>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 10.0f);
            if ((transform.position - destination).sqrMagnitude < 0.01f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Shoot()
    {
        projectile = Instantiate(this);
        projectile.destination = aiming.targetPos;
        projectile.transform.position = this.transform.position;
        projectile.sphereCollider.enabled = true;
        projectile.shoot = true;
    }
}
