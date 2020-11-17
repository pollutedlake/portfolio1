using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Character target;
    public Vector3 distanceFromTarget = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 lookOffset;
    public Vector3 newPosition;
    public float verticalAngle;
    public float horizontalAngle;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<Character>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // 마우스 움직임에 따른 각도 누적
        verticalAngle += Input.GetAxis("Mouse Y") / 2;
        horizontalAngle += Input.GetAxis("Mouse X") * 2;

        // 회전이 적용된 상대 위치 구하기
        Vector3 localPosition = Quaternion.Euler(verticalAngle, horizontalAngle, 0.0f) * distanceFromTarget;
        // 타겟으로부터의 위치 구하기
        newPosition = target.transform.position + localPosition;
        // 보는 지점 구하기
        Vector3 lookPosition = target.transform.position + lookOffset;
        // 위치 및 방향 설정
        transform.position = newPosition;
        transform.LookAt(lookPosition);
    }
}
