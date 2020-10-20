using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    private InputComponent inputComponent;
    private CharacterMovement characterMovement;

    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(animator== null)
        {
            animator = GetComponent<Animator>();
        }
        if (inputComponent == null)
        {

            inputComponent = GetComponent<InputComponent>();
        }
        if (characterMovement == null)
        {

            characterMovement = GetComponent<CharacterMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = inputComponent.input;
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        Vector3 direction = (forward * input.y + right * input.x).normalized;
        Vector3 velocity = characterMovement.Move(direction.normalized);
        if (velocity.sqrMagnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterMovement.speed = runSpeed;
            animator.SetBool("Run", true);
        }
        else{
            characterMovement.speed = walkSpeed;
            animator.SetBool("Run", false);
        }
    }
}
