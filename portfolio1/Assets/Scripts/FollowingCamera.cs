using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowingCamera : MonoBehaviour
{
    public Character target;
    public Vector3 distanceFromTarget = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 notAimingDist = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 aimingDist = new Vector3(0.5f, 2.0f, -1.5f);
    public Vector3 lookOffset = new Vector3(0.0f, 3.0f ,0.0f);
    public Vector3 newPosition;
    public float verticalAngle;
    public float horizontalAngle;
    private Camera camera;
    private float minFov = 15.0f;
    private float maxFov = 60.0f;
    private float sensitivity = 10f;
    private Vector3 lookPosition;
    public RawImage aimImg;

    private Ray cameraRay;

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
        verticalAngle = Mathf.Clamp(verticalAngle, -90.0f, 65.0f);
        // 타겟으로부터의 각도 구하기
        Quaternion angle = Quaternion.Euler(verticalAngle, horizontalAngle, 0.0f);
        cameraRay = new Ray(target.transform.position + lookOffset, -transform.forward);
        RaycastHit hitInfo;
        bool isGround = Physics.Raycast(cameraRay, out hitInfo, (transform.position - target.transform.position).magnitude, 1 << 9);
        if (isGround)
        {
            transform.position = hitInfo.point;
        }
        if (Input.GetKey(KeyCode.C) && target.animator.GetCurrentAnimatorStateInfo(0).IsTag("Move"))
        {
            aimImg.gameObject.SetActive(true);
            float fov = camera.fieldOfView;
            if (fov > 40.0f)
            {
                fov -= Time.deltaTime * 20.0f;
            }
            else
            {
                fov = 40.0f;
            }
            camera.fieldOfView = fov;
            // 타겟으로부터의 위치 구하기
            distanceFromTarget = Vector3.Lerp(distanceFromTarget, aimingDist, Time.deltaTime * sensitivity);
            lookPosition = target.transform.position + lookOffset + angle * aimingDist - angle * notAimingDist;
        }
        else
        {
            aimImg.gameObject.SetActive(false);
            float fov = camera.fieldOfView;
            if (fov < 47.0f)
            {
                fov += Time.deltaTime * 20.0f;
            }
            else
            {
                fov = 47.0f;
            }
            camera.fieldOfView = fov;
            // 타겟으로부터의 위치 구하기
            //distanceFromTarget = Vector3.Lerp(distanceFromTarget, notAimingDist, Time.deltaTime * sensitivity);
            // 보는 지점 구하기
            lookPosition = target.transform.position + lookOffset;
        }
        // 회전이 적용된 상대 위치 구하기
        Vector3 localPosition = angle * distanceFromTarget;
        newPosition = target.transform.position + localPosition;
        // 위치 및 방향 설정
        transform.position = newPosition;
        transform.LookAt(lookPosition);

        //float fov = camera.fieldOfView;
        //fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        //fov = Mathf.Clamp(fov, minFov, maxFov);
        //camera.fieldOfView = fov;
    }

    public void Aiming()
    {
        distanceFromTarget = Vector3.Lerp(distanceFromTarget, aimingDist, Time.deltaTime * 10.0f);
        //distanceFromTarget = aimingDist;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraRay);
    }
}
