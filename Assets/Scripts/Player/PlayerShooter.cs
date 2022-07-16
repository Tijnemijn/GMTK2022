using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private Player me;

    private Vector2 aim;

    public List<GunType> gunTypes;

    [SerializeField] private GameObject bulletPrefab;

    private GunInfo gun;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Player>();
        me.shooter = this;
        StartCoroutine(ShootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (me.input.Shooting)
            {
                for (int i = 0; i < gun.bulletsPerShot; i++)
                {
                    var deviation = Random.Range(-gun.aimDeviation, gun.aimDeviation);
                    var bulletDir = (aim + new Vector2(-aim.x, aim.y) * deviation).normalized; 

                    // todo: create spawn location transform
                    var bullet = Instantiate(
                        bulletPrefab, 
                        me.transform.position + (Vector3)bulletDir * 0.5f, 
                        Quaternion.LookRotation(Vector3.forward, bulletDir)
                        );

                    var controller = bullet.GetComponent<BulletController>();
                    controller.bulletInfo = gun.bulletInfo;
                    controller.velocity = bulletDir * gun.bulletSpeed;                    
                }                

                yield return Utils.WaitNonAlloc(0.2f);
            }
            else yield return null;
        }
    }
    private void Shoot()
    {

    }
    private void Aim()
    {
        Vector2 mousePosition = Utils.GetWorldSpaceMousePosition();
        aim = (mousePosition - (Vector2)me.transform.position).SafeNormalize();
        if (aim == Vector2.zero) aim = Vector2.up;

        var aimRot = Quaternion.LookRotation(Vector3.forward, aim);
        me.SpriteObject.rotation = Quaternion.Slerp(me.SpriteObject.rotation, aimRot, Time.deltaTime * 24f);
    }
}
