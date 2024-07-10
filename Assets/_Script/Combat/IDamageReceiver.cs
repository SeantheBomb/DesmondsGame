using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver 
{

    float Priority { get; }

    DamageInfo ReceiveDamage(Health health, DamageInfo damageInfo);

}
