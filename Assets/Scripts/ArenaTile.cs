using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArenaTileType
{
    Heater,
    Ice,
    Mud
}
public class ArenaTile : MonoBehaviour
{
    [field: SerializeField] public ArenaTileType type { get; private set; }
    [SerializeField] private SpriteAnimator animator;
    [SerializeField] private float cooldownTime = 0.4f;
    private float cooldown = 0;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (type == ArenaTileType.Heater && cooldown <= 0)
            ApplyHeaterEffect(other);
    }
    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }
    public void SwitchToType(ArenaTileType type)
    {
        this.type = type;
        switch (type)
        {
            case ArenaTileType.Heater:
                animator.SwitchAnimation("ArenaHeaterTile");
                break;
            case ArenaTileType.Ice:
                animator.SwitchAnimation("ArenaIceTile");
                break;
            case ArenaTileType.Mud:
                animator.SwitchAnimation("ArenaMudTile");
                break;
        }
        animator.JumpToFrame(0);        
    }

    private void ApplyHeaterEffect(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(5f);
            cooldown = cooldownTime;
        }
        else if (other.TryGetComponent(out Player player))
        {
            player.Damage(2.8f, true);
            cooldown = cooldownTime;
        }
    }
}
