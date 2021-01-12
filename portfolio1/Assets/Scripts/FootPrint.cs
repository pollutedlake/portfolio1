using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    private float durationTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        durationTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        durationTime += Time.deltaTime;
        if (durationTime > 100.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
