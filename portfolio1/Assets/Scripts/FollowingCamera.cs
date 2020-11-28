using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowingCamera : MonoBehaviour
{
    public Character target;        // 카메라가 가리키는 플레이어

    // 카메라의 위치에 관한 변수
    public Vector3 distanceFromTarget = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 notAimingDist = new Vector3(0.0f, 2.0f, -5.0f);
    public Vector3 aimingDist = new Vector3(0.5f, 2.0f, -1.5f);

    // 마우스 입력에 관한 변수
    public float verticalAngle;
    public float horizontalAngle;

    private Camera camera;

    // 줌인 줌아웃 최솟값과 최댓값
    private float minFov = 15.0f;
    private float maxFov = 60.0f;

    private float sensitivity = 10f;        // 보간 보정값

    // 카메라가 보는 지점에 관한 변수
    private Vector3 lookPosition;
    public Vector3 lookOffset = new Vector3(0.0f, 3.0f, 0.0f);

    public RawImage aimImg;     // 과녁 UI

    private Ray cameraRay;      // 카메라가 지형에 닿는지 확인하기 위한 Ray

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
        verticalAngle = Mathf.Clamp(verticalAngle, -90.0f, 65.0f);      // 상하 각도 제한

        // 타겟으로부터의 각도 구하기
        Quaternion angle = Quaternion.Euler(verticalAngle, horizontalAngle, 0.0f);

        // 캐릭터가 움직일 수 있는 상태이고 조준 키(C)를 눌렀다면 aimImg활성화, 줌, (카메라 위치, 보는 위치) 변동
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
        // 다시 원상 복구
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
            distanceFromTarget = Vector3.Lerp(distanceFromTarget, notAimingDist, Time.deltaTime * sensitivity);
            // 보는 지점 구하기
            lookPosition = target.transform.position + lookOffset;
        }
        // 회전이 적용된 상대 위치 구하기
        Vector3 localPosition = angle * distanceFromTarget;
        Vector3 newPosition = target.transform.position + localPosition;
        // 위치 및 방향 설정
        transform.position = newPosition;
        transform.LookAt(lookPosition);

        // 카메라가 이동 시 땅에 닿는다면 닿은 위치를 카메라 위치로 지정해서 땅을 통과하지 않는다.
        cameraRay = new Ray(target.transform.position + lookOffset, -transform.forward);
        RaycastHit hitInfo;
        bool isGround = Physics.Raycast(cameraRay, out hitInfo, (transform.position - target.transform.position).magnitude, 1 << 9);
        if (isGround)
        {
            transform.position = hitInfo.point;
        }

        // 카메라 줌 기능
        //float fov = camera.fieldOfView;
        //fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        //fov = Mathf.Clamp(fov, minFov, maxFov);
        //camera.fieldOfView = fov;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraRay);
    }
}
