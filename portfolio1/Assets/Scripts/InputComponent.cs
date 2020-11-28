using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    // 입력될 Character가 움직일 방향과 관련된 변수
    public Vector2 input;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    // Character 상태와 관련된 변수
    public bool isRun = false;
    public bool isRoll = false;
    public bool mouseLBtn = false;
    private Animator animator;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRoll = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mouseLBtn = true;
        }
    }
}
