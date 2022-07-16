using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI healthMeter;
    private float maxSize;
    // Start is called before the first frame update
    void Start()
    {
        maxSize = healthBar.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        float healthRatio = Player.Instance.Health / Player.Instance.MaxHealth;
        healthBar.rectTransform.sizeDelta = new Vector2(healthRatio * maxSize, healthBar.rectTransform.sizeDelta.y);
        healthBar.rectTransform.anchoredPosition = Vector2.right * maxSize * healthRatio / 2;
        healthMeter.text = $"{Player.Instance.Health:F0} / {Player.Instance.MaxHealth:F0}";
    }
}
