using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;

    public Vector3 Move(Vector3 direction)
    {
        Vector3 velocity = direction * speed * Time.deltaTime;

        if (direction.sqrMagnitude > 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }

        transform.position += velocity;
        return velocity;
    }
}
