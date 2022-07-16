using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2 SafeNormalize(this Vector2 v) => v == Vector2.zero ? Vector2.zero : v.normalized;
}
