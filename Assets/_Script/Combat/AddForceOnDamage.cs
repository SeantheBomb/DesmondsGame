using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class AddForceOnDamage : MonoBehaviour, IDamageReceiver
{

    public float force;

    Rigidbody2D rb;

    public float Priority => 0;

    public DamageInfo ReceiveDamage(Health health, DamageInfo damageInfo)
    {
        Vector2 dir = rb.position - damageInfo.hitPoint;
        rb.AddForce(dir.normalized * force, ForceMode2D.Impulse);
        Debug.Log($"Health: {name} add force {force} in direction {dir.normalized}");
        return damageInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


}
