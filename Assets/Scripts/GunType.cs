using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Type")]
public class GunType : ScriptableObject
{
    public IntValueRange bulletsPerShot;
    [Space]
    public FloatValueRange bulletSpeed;
    [Space]
    public FloatValueRange bulletDamage;
    [Space]
    public FloatValueRange fireRate;
    
    public float bulletLifetime;
    public float bulletFalloffSpeed;
    public float bulletMinDamageMultiplier;

    public float aimDeviation;
    public float knockbackStrength;

    public GunInfo GenerateGunInfo()
    {
        BulletInfo b = new BulletInfo();
        
        b.damage = bulletDamage.Sample();        
        b.lifetime = bulletLifetime;
        b.falloffSpeed = bulletFalloffSpeed;
        b.minDamageMultiplier = bulletMinDamageMultiplier;
        b.knockbackStrength = knockbackStrength;

        GunInfo result = new GunInfo();
        
        result.bulletInfo = b;
        result.aimDeviation = aimDeviation;

        result.bulletsPerShot = bulletsPerShot.Sample();
        result.bulletSpeed = bulletSpeed.Sample();
        result.fireRate = fireRate.Sample();
        
        result.startingDamage = b.damage;
        result.startingFireRate = result.fireRate;

        return result;
    }
}

[System.Serializable]
public struct GunInfo
{
    public string Type;
    public float startingFireRate;
    public float startingDamage;
    public int bulletsPerShot;
    public float fireRate;
    public float bulletSpeed;
    public float aimDeviation;
    public BulletInfo bulletInfo;
}

[System.Serializable]
public struct IntValueRange
{
    public int min;
    public int max;

    public int Sample() => Random.Range(min, max);
}

[System.Serializable]
public struct FloatValueRange
{
    public float min;
    public float max;

    public float Sample() => Random.Range(min, max);
}