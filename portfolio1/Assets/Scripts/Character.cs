using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    InputComponent inputComponent;
    CharacterMovement characterMovement;
    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 input = inputComponent.input;
        characterMovement.Move(input.normalized);
    }
}
