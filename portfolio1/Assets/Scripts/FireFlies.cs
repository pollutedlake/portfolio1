﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    public Character character;
    public    Vector3 characterPosition;
    private void Start()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
    }

    private void Update()
    {
        characterPosition = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        if ((characterPosition - transform.position).magnitude < 0.1f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
