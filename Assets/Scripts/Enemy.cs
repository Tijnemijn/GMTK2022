using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // TODO: encapsulate
    [field: SerializeField] public float MaxHealth { get; private set; }
    public float Health { get; private set; }

    public float damagePerHit;
    public float hitRate;
    public float attackRange;
    public float knockbackStrength;

    public bool Alive { get; private set; } = true;



    [field: SerializeField] public Transform SpriteObject { get; private set; }
    [SerializeField] public EnemySpawner Spawner;
    private GameObject spawner;

    private Rigidbody2D rb;
    private EntityController controller;
    private Transform target;
    
    internal new Transform transform;    

    public float Damage(float amount)
    {
        if (!Alive) return amount;
        
        StartCoroutine(DamageRoutine());
        
        float diff = amount - Health;
        Health -= amount;

        if (Health <= 0)
        {
            Die();
            return diff;
        }        
        else
        {
            return 0;
        }
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
        rb.AddForce(amount * 5, ForceMode2D.Impulse);
    }

    public void Die()
    {
        Alive = false;
        Spawner.enemyAmount--;
        Destroy(gameObject);
    }

    private void Start()
    {
    
        Health = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<EntityController>();
        target = Player.Instance.transform;
        transform = base.transform;

        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine() 
    {
        while (true)
        {
            var diff = target.position - transform.position;            
            if (!Player.Instance.IsInvincible && diff.magnitude <= attackRange)
            {
                
                // deal damage
                Player.Instance.Damage(damagePerHit);
                Player.Instance.Knockback(diff.normalized * knockbackStrength);
                Knockback(-diff.normalized * knockbackStrength);
                // cooldown
                yield return new WaitForSeconds(1f / hitRate);
            }
            else
            {
                yield return null;
            }
        }
    }
    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject != this.gameObject) return;
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(base.transform.position, Vector3.back, attackRange);
    }
#endif
    private void Update()
    {
        Vector2 delta = (Vector2)(target.position - transform.position);
        Vector2 dir = delta.SafeNormalize();
        controller.Move(dir);

        SpriteObject.rotation = Quaternion.Slerp(SpriteObject.rotation, Quaternion.LookRotation(Vector3.forward, dir), 24f * Time.deltaTime);
    }
}
