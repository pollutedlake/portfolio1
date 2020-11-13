using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public Vector2 input;       // 입력받을 character가 움직일 방향
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public bool isRun;

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis(horizontalAxis);
        input.y = Input.GetAxis(verticalAxis);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }
}
