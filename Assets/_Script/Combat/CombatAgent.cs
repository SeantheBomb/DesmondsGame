using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAgent : MonoBehaviour
{

    public int team = 0;
    
    public DamageInfo Damage(Health health, float healthTaken)
    {
        DamageInfo damage = new DamageInfo();
        damage.source = this;
        damage.healthTaken = healthTaken;
        damage.hitPoint = transform.position;
        health.TakeDamage(damage);
        return damage;
    }

    public DamageInfo[] AttackOverlapCircle(float damage, float radius)
    {
        List<DamageInfo> damageInfos = new List<DamageInfo>();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(var hit in hits)
        {
            if (hit.transform.root == transform.root)
                continue;
            if(hit.TryGetComponent(out Health h))
            {
                if(h.TryGetComponent(out CombatAgent agent))
                {
                    if (team == agent.team) continue;
                }
                damageInfos.Add(Damage(h, damage));
            }
        }
        return damageInfos.ToArray();
    }

    public DamageInfo[] AttackCircleCase(float damage, Vector2 dir, float radius, float range)
    {
        List<DamageInfo> damageInfos = new List<DamageInfo>();
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, dir, range);
        foreach (var hit in hits)
        {
            if (hit.transform.root == transform.root)
                continue;
            if (hit.transform.TryGetComponent(out Health h))
            {
                if (h.TryGetComponent(out CombatAgent agent))
                {
                    if (team == agent.team) continue;
                }
                damageInfos.Add(Damage(h, damage));
            }
        }
        return damageInfos.ToArray();
    }

}

public struct DamageInfo
{
    public CombatAgent source;
    public float healthTaken;
    public Vector2 hitPoint;
}
