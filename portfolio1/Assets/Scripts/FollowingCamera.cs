using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Character target;
    public Vector3 distanceFromTarget = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 notAimingDist = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 aimingDist = new Vector3(1.0f, 1.0f, -1.0f);
    public Vector3 lookOffset = new Vector3(0.0f, 3.0f ,0.0f);
    public Vector3 newPosition;
    public float verticalAngle;
    public float horizontalAngle;
    private Camera camera;
    private float minFov = 15.0f;
    private float maxFov = 60.0f;
    private float sensitivity = 10f;
    private Vector3 lookPosition;

    // Start is called before the first frame update
    void Start()
    {
        if(camera == null)
        {
            camera = GetComponent<Camera>();
        }
        if (target == null)
        {
            target = FindObjectOfType<Character>();
        }
        Debug.Log(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // 마우스 움직임에 따른 각도 누적
        verticalAngle += Input.GetAxis("Mouse Y");
        horizontalAngle += Input.GetAxis("Mouse X") * 2;

        // 회전이 적용된 상대 위치 구하기
        Vector3 localPosition = Quaternion.Euler(verticalAngle, horizontalAngle, 0.0f) * distanceFromTarget;
        // 타겟으로부터의 위치 구하기
        newPosition = target.transform.position + localPosition;
        // 보는 지점 구하기
        if (Input.GetKey(KeyCode.C))
        {
            distanceFromTarget = Vector3.Lerp(distanceFromTarget, aimingDist, Time.deltaTime * 10.0f);
        }
        else
        {
            distanceFromTarget = Vector3.Lerp(distanceFromTarget, notAimingDist, Time.deltaTime * 10.0f);
            lookPosition = target.transform.position + lookOffset;
        }
        // 위치 및 방향 설정
        transform.position = newPosition;
        transform.LookAt(lookPosition);

        float fov = camera.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        camera.fieldOfView = fov;

        Vector3 aimingLookVec = lookPosition - transform.position;
        aimingLookVec.y = 0.0f;
    }

    public void Aiming()
    {
        distanceFromTarget = Vector3.Lerp(distanceFromTarget, aimingDist, Time.deltaTime * 10.0f);
        //distanceFromTarget = aimingDist;
    }
}
