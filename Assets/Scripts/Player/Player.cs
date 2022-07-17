using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    public float Health { get; set; }
    public bool Alive { get; private set; } = true;

    public bool CanShoot => !UIManager.Instance.IsWindowOpened && Alive;

    public bool CanMove => !UIManager.Instance.IsWindowOpened && Alive;

    private static Player instance;
    public static Player Instance => instance ?? throw new System.Exception("Singleton was not initialized");

    internal PlayerInput input;   
    internal PlayerShooter shooter;

    private EntityController controller;
    private SpriteAnimator animator;
    private Rigidbody2D rb;

    private Vector2 motionInput;

    internal new Transform transform;


    [SerializeField] private float stunTime = 1f;
    public bool IsInvincible { get; private set; }

    [field: SerializeField] public Transform SpriteObject { get; private set; }

    public AudioSource shootSFX;
    public AudioSource stepSFX;
    public AudioSource hurtSFX;

    public float Damage(float amount, bool noStun = false)
    {        
        if (!Alive) return amount;

        if (noStun)
        {
            StartCoroutine(DamageRoutine());
        }
        else
        {
            hurtSFX.Play();
            IsInvincible = true;
            StartCoroutine(StunRoutine());            
            CameraController.Instance.Shake(0.3f, 0.4f);
        }

        float diff = amount - Health;

        Health -= amount;        
        if (Health <= 0)
        {
            Die();
            return 0;
        }
        else
        {
            return diff;
        }
        return amount;
    }
    private IEnumerator StunRoutine()
    {
        var sprite = SpriteObject.GetComponent<SpriteRenderer>();        
        sprite.color = Color.red;
        yield return Utils.WaitNonAlloc(0.1f);
        sprite.color = new Color(1, 1, 1, 0.5f);
        yield return Utils.WaitNonAlloc(stunTime);
        sprite.color = Color.white;
        IsInvincible = false;
    }
    private IEnumerator DamageRoutine()
    {
        var sprite = SpriteObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return Utils.WaitNonAlloc(0.1f);
        sprite.color = Color.white;
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

        StartCoroutine(StepSFXLoop());
    }

    // TODO: Maybe move this to PlayerInput
    void Update()
    {
        motionInput = input.GetMotionInput();
        if(motionInput != Vector2.zero && CanMove)
            animator.SwitchAnimation("PlayerWalkAnim");
        else
        {
            animator.SwitchAnimation("PlayerIdle");            
        }          
    }
    private IEnumerator StepSFXLoop()
    {
        while (true)
        {
            if (Alive && motionInput != Vector2.zero)
            {
                stepSFX.Play();
                yield return Utils.WaitNonAlloc(0.2f);
            }
            else yield return null;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(CanMove ? motionInput : Vector2.zero);
    }
}
