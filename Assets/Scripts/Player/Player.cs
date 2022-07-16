using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal PlayerInput input;   
    internal PlayerShooter shooter;

    private EntityController controller;

    private Vector2 motionInput;

    internal new Transform transform;

    [field: SerializeField] public Transform SpriteObject { get; private set; }

    void Start()
    {
        controller = GetComponent<EntityController>();
        motionInput = Vector2.zero;

        transform = base.transform;
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
