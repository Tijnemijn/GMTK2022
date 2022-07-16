using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{   
    [SerializeField] private float movementForce = 2.5f;
    [SerializeField] private float damping = 0.63f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input) {
        var v = rb.velocity;
        v += input.SafeNormalize() * movementForce * 60f * Time.deltaTime;
        v *= Mathf.Pow(1f - damping, Time.deltaTime * 10f);
        rb.velocity = v;
    }
}
