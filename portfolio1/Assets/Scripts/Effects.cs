﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private float destroyTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime > 1.0f)
        {
            destroyTime = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
