using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{    
    public BulletInfo bulletInfo;
    public Vector2 velocity;

    [SerializeField] private float damage;
    [SerializeField] private float falloffMultiplier = 1;
    [SerializeField] private float lifetime;

    private Rigidbody2D rb;

    private void Start()
    {
        damage = bulletInfo.damage;
        lifetime = bulletInfo.lifetime;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }
    private void Update()
    {
        falloffMultiplier = Mathf.Lerp(falloffMultiplier, bulletInfo.minDamageMultiplier, bulletInfo.falloffSpeed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) DestroyBullet();
    }

    private void DestroyBullet()
    {
        rb.velocity = Vector2.zero;
        
        // TODO: show some animation
        
        // destroy object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: deal damage / apply knockback
        
        // destroy bullet
        if (damage <= 0) DestroyBullet();
    }
}

[System.Serializable]
public struct BulletInfo
{    
    public float damage;
    public float falloffSpeed;
    public float minDamageMultiplier;
    public float lifetime;

    public BulletInfo(float damage, float falloffSpeed, float minDamageMultiplier, float lifetime)
    {
        this.damage = damage;
        this.falloffSpeed = falloffSpeed;
        this.minDamageMultiplier = minDamageMultiplier;
        this.lifetime = lifetime;
    }
}