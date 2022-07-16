using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player me;

    private void Start()
    {
        me = GetComponent<Player>();
        me.input = this;
    }

    public Vector2 GetMotionInput()
    {
        return new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );
    }
}
