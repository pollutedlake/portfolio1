using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 발사된 Projectile 관련 변수
    public SphereCollider sphereCollider;
    public Projectile projectile;
    public Aimimg aimimg;
    private bool shoot = false;     // 발사된 Projectile인지 확인하는 변수
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (aimimg == null)
        {
            aimimg = FindObjectOfType<UIMgr>().transform.GetChild(0).GetComponent<Aimimg>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 발사된 상태라면 목표지점까지 이동 후 사라진다.
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
        // 발사체를 생성하고 변수초기화
        projectile = Instantiate(this);
        projectile.destination = aimimg.targetPos;
        projectile.transform.position = this.transform.position;
        projectile.sphereCollider.enabled = true;
        projectile.shoot = true;
    }
}
