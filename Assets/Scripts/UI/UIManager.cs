using UnityEngine;

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

    public bool IsPaused { get; private set; }

}