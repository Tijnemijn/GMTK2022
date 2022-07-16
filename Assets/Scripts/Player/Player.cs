using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal PlayerInput input;   

    private EntityController controller;

    private Vector2 motionInput;

    void Start()
    {
        controller = GetComponent<EntityController>();
        motionInput = Vector2.zero;
    }

    // TODO: Maybe move this to PlayerInput
    void Update()
    {
        motionInput = input.GetMotionInput();
    }

    private void FixedUpdate()
    {
        controller.Move(motionInput);
    }
}
