using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;

    /// <summary>
    /// character 이동함수
    /// </summary>
    /// <param name="direction"> character가 이동할 방향</param>
    /// <returns></returns>
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
