using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public bool IsOpen { get; private set; } = false;
    
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform backdrop;
    [SerializeField] private GameObject closeButton;

    [Space]
    [SerializeField] private HealthDiceScript health;
    [SerializeField] private GunDiceScript gun;


    public void Open()
    {
        if (IsOpen) return;
        UIManager.Instance.currentWindow = this;
        
        IsOpen = true;
        content.gameObject.SetActive(true);
        backdrop.gameObject.SetActive(true);
            
        LeanTween.cancel(content);
        LeanTween.scale(content, Vector3.one, 0.3f)
            .setEaseOutBack();
        
        LeanTween.cancel(backdrop);
        LeanTween.alpha(backdrop, 0.5f, 0.5f);
        
        closeButton.SetActive(false);
    }

    public void Close()
    {
        if (!IsOpen) return;
        UIManager.Instance.currentWindow = null;

        
        gun.used = false;
        gun.ResetAnimation();
        health.used = false;
        health.ResetAnimation();
        IsOpen = false;

        LeanTween.cancel(content);
        LeanTween.scale(content, Vector3.zero, 0.3f)
            .setEaseInBack()
            .setOnComplete(() => content.gameObject.SetActive(false));
        
        LeanTween.cancel(backdrop);
        LeanTween.alpha(backdrop, 0, 0.5f)
            .setOnComplete(() => backdrop.gameObject.SetActive(false));
    }

    private void Start()
    {        
        content.gameObject.SetActive(false);
        backdrop.gameObject.SetActive(false);
        
        content.localScale = Vector3.zero;
        LeanTween.alpha(backdrop, 0, 0);
        
    }

    private void Update()
    {
        if (gun.used && health.used && IsOpen)
        {
            closeButton.SetActive(true);
        }
    }
    
}