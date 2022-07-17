using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{   
    [SerializeField] private float movementForce = 2.5f;
    [SerializeField] private float damping = 0.63f;

    private float dampingMultiplier = 1, movementMultiplier = 1;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input) {
        var v = rb.velocity;
        
        var d = Mathf.Min(damping * dampingMultiplier, 0.9f);

        v += input.SafeNormalize() * movementForce * movementMultiplier * 60f * Time.deltaTime;
        v *= Mathf.Pow(1f - d, Time.deltaTime * 10f);
        rb.velocity = v;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ArenaTile tile))
        {
            switch (tile.type)
            {
                case ArenaTileType.Ice:
                    dampingMultiplier = 0.1f;
                    movementMultiplier = 0.3f;
                    break;
                case ArenaTileType.Mud:
                    dampingMultiplier = 1.2f;
                    movementMultiplier = 0.3f;
                    break;
            }            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out ArenaTile tile))
        {
            dampingMultiplier = 1;
            movementMultiplier = 1;
        }
    }
}
