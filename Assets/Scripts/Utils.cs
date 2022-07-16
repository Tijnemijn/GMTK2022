using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2 SafeNormalize(this Vector2 v) => v == Vector2.zero ? Vector2.zero : v.normalized;

    /// <summary>
    /// Gets a random element from a list.
    /// </summary>
    public static T GetRandom<T>(this IList<T> ts) => ts[Random.Range(0, ts.Count)];



    private static readonly Dictionary<float, WaitForSeconds> wfsCache = new Dictionary<float, WaitForSeconds>();

    /// <summary>
    /// Non-allocating WaitForSeconds
    /// </summary>
    public static WaitForSeconds WaitNonAlloc(float time)
    {
        if (wfsCache.TryGetValue(time, out WaitForSeconds wait)) return wait;

        wfsCache[time] = new WaitForSeconds(time);
        return wfsCache[time];
    }

    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }

    private static Camera camera;

    /// <summary>
    /// Cached reference to Camera.main to reduce extern calls.
    /// </summary>
    public static Camera Camera
    {
        get
        {
            if (camera == null) camera = Camera.main;
            return camera;
        }
    }

    public static Vector2 GetWorldSpaceMousePosition()
    {
       return (Vector2) Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
