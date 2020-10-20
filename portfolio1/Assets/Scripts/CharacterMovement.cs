using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0f;

    public void Move(Vector3 direction)
    {
        Vector3 velocity = direction * speed * Time.deltaTime;
        transform.position += velocity;
    }
}
