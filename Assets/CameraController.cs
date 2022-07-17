using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance => instance ?? throw new System.Exception("Singleton not initialized");
    private Vector3 centerPosition;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        centerPosition = transform.position;
    }

    public void Shake(float strength, float duration)
    {
        StartCoroutine(ShakeRoutine(strength, duration));
    }
    private IEnumerator ShakeRoutine(float strength, float duration)
    {
        var t = transform;
        
        float time = duration;
        while (time > 0)
        {
            float tr = time / duration;
            
            t.position = centerPosition 
                + strength * tr * new Vector3(Random.Range(-1, 1), Random.Range(-1, 1));

            yield return null;
            time -= Time.deltaTime;
        }        
    }
}
