using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal PlayerInput input;   
    internal PlayerShooter shooter;

    private EntityController controller;
    private SpriteAnimator animator;

    private Vector2 motionInput;

    internal new Transform transform;

    [field: SerializeField] public Transform SpriteObject { get; private set; }

    void Start()
    {
        controller = GetComponent<EntityController>();
        motionInput = Vector2.zero;

        transform = base.transform;
        animator = SpriteObject.GetComponent<SpriteAnimator>();
    }

    // TODO: Maybe move this to PlayerInput
    void Update()
    {
        motionInput = input.GetMotionInput();
        if(motionInput == new Vector2(0,0))
            animator.SwitchAnimation("PlayerIdle");
        else
            animator.SwitchAnimation("PlayerWalkAnim");
    }

    private void FixedUpdate()
    {
        controller.Move(motionInput);
    }
}
