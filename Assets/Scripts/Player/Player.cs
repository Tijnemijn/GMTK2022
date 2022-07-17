using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    public float Health { get; set; }
    public bool Alive { get; private set; } = true;

    private static Player instance;
    public static Player Instance => instance ?? throw new System.Exception("Singleton was not initialized");

    internal PlayerInput input;   
    internal PlayerShooter shooter;

    private EntityController controller;
    private SpriteAnimator animator;
    private Rigidbody2D rb;

    private Vector2 motionInput;

    internal new Transform transform;

    private int iframes = 0;
    [SerializeField] private int iframeAmount = 270;
    private int lastFrame;

    [field: SerializeField] public Transform SpriteObject { get; private set; }

    public float Damage(float amount)
    {
        if (!Alive) return amount;
        {
            if (iframes <= 0)
            {
                float diff = amount - Health;
                Health -= amount;

                if (Health <= 0)
                {
                    Die();
                    return 0;
                }
                else
                {
                    iframes = iframeAmount;
                    return diff;
                }
            }
            return amount;
        }
    }
    public void Knockback(Vector2 amount)
    {
        rb.velocity += amount;
    }
    private void Die()
    {
        Alive = false;
        // don't destroy player here
    }

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
        transform = base.transform;
    }

    void Start()
    {
        Health = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<EntityController>();
        motionInput = Vector2.zero;
        
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

        iframes -= Time.frameCount - lastFrame;
        lastFrame = Time.frameCount;
    }

    private void FixedUpdate()
    {
        controller.Move(motionInput);
    }
}
