using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunDiceScript : UIDiceScript
{
    public List<GunType> gunTypes;
    public GunType type;
    public GunInfo gun;
    public TextMeshProUGUI gunTypeText;
    public TextMeshProUGUI gunInfoText;


    protected override void Animate()
    {
        rect.LeanMoveY(20, 0.1f)
            .setEaseOutQuad()
            .setOnComplete(_ => rect.LeanMoveY(0, 0.1f).setEaseInQuad());

        rect.LeanSize(new Vector2(400, 250), 0.5f)
            .setEaseInOutCubic();

        LeanTween.value(gunTypeText.gameObject, v => gunTypeText.alpha = v, 0, 1, 0.5f);
        LeanTween.value(gunInfoText.gameObject, v => gunInfoText.alpha = v, 0, 1, 0.5f);

        StartCoroutine(RollValue());
    }
    public IEnumerator RollValue()
    {
        for (int i = 0; i < 20; i++)
        {
            type = gunTypes.GetRandom();
            gunTypeText.text = $"{type.name}";
            gun = type.GenerateGunInfo();
            gunInfoText.text = 
                $"DMG: {gun.bulletInfo.damage:F2}\n" +
                $"Firerate: {gun.fireRate:F2}\n" +
                $"Speed: {gun.bulletSpeed:F2}\n" +
                $"Recoil: {gun.aimDeviation:F2}";
            yield return Utils.WaitNonAlloc(0.04f);
        }
    }
}
