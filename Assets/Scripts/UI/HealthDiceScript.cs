using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDiceScript : UIDiceScript
{
    [SerializeField] private Player player;
    public TextMeshProUGUI valueText;
    public int value;
    public bool used;
    protected override void Animate()
    {
        rect.LeanMoveY(20, 0.1f)
            .setEaseOutQuad()
            .setOnComplete(_ => rect.LeanMoveY(0, 0.1f).setEaseInQuad());
        
        rect.LeanSize(new Vector2(220, 100), 0.5f)
            .setEaseInOutCubic();
        LeanTween.value(valueText.gameObject, v => valueText.alpha = v, 0, 1, 0.5f); 

        StartCoroutine(RollValue());
    }
    public override void ResetAnimation()
    {
        rect.LeanSize(new Vector2(100, 100), 0f);
        valueText.alpha = 0;
        used = false;
        opened = false;
    }
    public IEnumerator RollValue()
    {
        for (int i = 0; i < 20; i++)
        {
            value = Random.Range(0, 15);
            valueText.text = $"+ {value}";
            yield return Utils.WaitNonAlloc(0.04f);
        }

        player.Health = Mathf.Min(player.MaxHealth, player.Health + value);
        used = true;
    }
}
