using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private Player me;

    private Vector2 aim;

    [SerializeField] public List<GunType> gunTypes;

    private GunInfo gun;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Player>();
        me.shooter = this;
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
                yield return Utils.WaitNonAlloc(0.2f);
            }
            else yield return null;
        }
    }

    private void Aim()
    {
        Vector2 mousePosition = Utils.GetWorldSpaceMousePosition();
        aim = (mousePosition - (Vector2)me.transform.position).SafeNormalize();
        Quaternion q = Quaternion.LookRotation(Vector3.forward, aim);
        me.SpriteObject.rotation = Quaternion.Slerp(me.SpriteObject.rotation, q, Time.deltaTime * 24f);
    }
}
