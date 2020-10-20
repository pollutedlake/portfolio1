using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public Vector3 input;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis(horizontalAxis);
        input.z = Input.GetAxis(verticalAxis);
    }
}
