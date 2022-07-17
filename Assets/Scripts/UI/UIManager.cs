using TMPro;
using System.Collections;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    [HideInInspector] public Window currentWindow;
    public bool IsWindowOpened => currentWindow != null;

    [SerializeField] private TextMeshProUGUI countdown; 

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }
    public event Action OnCountDownComplete;
    private IEnumerator Countdown()
    {
        for (int i = 3; i >= 1; i--)
        {
            countdown.text = $"{i}";
            yield return Utils.WaitNonAlloc(1f);
        }
        countdown.text = "";
        OnCountDownComplete?.Invoke();
    }
}