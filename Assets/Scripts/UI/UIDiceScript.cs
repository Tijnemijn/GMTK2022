using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDiceScript : MonoBehaviour
{
    
    protected RectTransform rect;
    protected bool opened = false;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void Open()
    {
        if (opened) return;
        opened = true;
        Animate();
    }
    public virtual void ResetAnimation()
    {

    }
    protected virtual void Animate()
    {
        
    }
}
