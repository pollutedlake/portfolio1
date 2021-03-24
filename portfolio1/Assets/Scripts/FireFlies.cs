using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    public Character character;
    public Vector3 characterPosition;
    public GameObject target;       // FireFly가 쫓는 대상
    public UIMgr uiMgr;
    public Navigator navigator;

    //public int index;

    // target이 InteractiveObject일 경우 포물선으로 날아가는데 필요한 변수들
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    private void Start()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
        if (target.CompareTag("InteractiveObject"))
        {
            StartCoroutine(DetectInteractiveObject());
        }
        if (uiMgr == null)
        {
            uiMgr = FindObjectOfType<UIMgr>();
        }
        if (!target.CompareTag("InteractiveObject"))
        {
            navigator = FindObjectOfType<Navigator>();
        }
    }

    private void Update()
    {
        if (target.CompareTag("InteractiveObject"))
        {
            
            Vector3 distance = target.transform.position - transform.position;
            // target에 다다르면 색을 바꿔주고 objectText(UI)를 생성해 어떤 InteractiveObject인지 플레이어에게 보여주고 FireFly는 Destory한다.
            if (distance.sqrMagnitude < 0.1f)
            {
                target.GetComponent<InteractiveObject>().ChangeColor();
                uiMgr.ShowObjectName(target.GetComponent<InteractiveObject>());
                Destroy(gameObject);
            }
            
        }
    }


    // target에 FireFly가 포물선으로 날아가는 함수
    IEnumerator DetectInteractiveObject()
    {
        // target까지의 거리를 계산한다.
        float target_Distance = Vector3.Distance(transform.position, target.transform.position);

        // 특정한 각도로 target까지 날아가는데 필요한 속도를 계산한다.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // x, y축으로 속도를 추출한다.
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // 날아가는 시간을 계산한다.
        float flightDuration = target_Distance / Vx;

        // target을 향하도록 한다.
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
