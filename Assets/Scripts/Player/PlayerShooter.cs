using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private Player me;

    private Vector2 lookDir;

    private int lastWave = 0;

    public List<GunType> gunTypes;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [Space] 
    [SerializeField] private GameObject shotgunSprite;
    [SerializeField] private GameObject sniperSprite;
    [SerializeField] private GameObject arSprite;

    public GunInfo gun;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Player>();
        me.shooter = this;
        StartCoroutine(ShootRoutine());
        gun = gunTypes.GetRandom().GenerateGunInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastWave != enemySpawner.wave)
        {
            gun.bulletInfo.damage = gun.startingDamage * Mathf.Pow(1.01f, enemySpawner.wave) * (0.98f *enemySpawner.wave); //increase damage each wave
        }
        Aim();
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (me.input.Shooting)
            {
                
                Shoot();
                yield return Utils.WaitNonAlloc(1f / gun.fireRate);
            }
            else yield return null;
        }
    }
    private void Shoot()
    {
        for (int i = 0; i < gun.bulletsPerShot; i++)
        {

            var deviation = Random.Range(-gun.aimDeviation, gun.aimDeviation);

            Vector2 mousePosition = Utils.GetWorldSpaceMousePosition();
            var aim = (mousePosition - (Vector2)bulletSpawn.position).SafeNormalize();

            var bulletDir = (aim + new Vector2(-aim.y, aim.x) * deviation).normalized;
            me.Knockback(-bulletDir * gun.bulletInfo.knockbackStrength * 0.02f);
            // todo: create spawn location transform
            var bullet = Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                Quaternion.LookRotation(Vector3.forward, bulletDir)
                );

            var controller = bullet.GetComponent<BulletController>();
            controller.bulletInfo = gun.bulletInfo;
            controller.velocity = bulletDir * gun.bulletSpeed;
        }

    }
    private void Aim()
    {
        Vector2 mousePosition = Utils.GetWorldSpaceMousePosition();
        lookDir = (mousePosition - (Vector2)me.transform.position).SafeNormalize();
        if (lookDir == Vector2.zero) lookDir = Vector2.up;

        var aimRot = Quaternion.LookRotation(Vector3.forward, lookDir);
        me.SpriteObject.rotation = Quaternion.Slerp(me.SpriteObject.rotation, aimRot, Time.deltaTime * 24f);
    }

    public void ChangeSprite(string type)
    {
        if (type == "Sniper")
        {
            sniperSprite.SetActive(true);
            arSprite.SetActive(false);
            shotgunSprite.SetActive(false);
        }
        else if (type == "AR")
        {
            sniperSprite.SetActive(false);
            arSprite.SetActive(true);
            shotgunSprite.SetActive(false);
        }
        else
        {
            sniperSprite.SetActive(false);
            arSprite.SetActive(false);
            shotgunSprite.SetActive(true);
        }
    }
}
