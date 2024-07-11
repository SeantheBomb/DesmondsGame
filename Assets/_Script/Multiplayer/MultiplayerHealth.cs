using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public class MultiplayerHealth : NetworkBehaviour
{

    [SerializeField] Health health;


    // Start is called before the first frame update
    void Start()
    {
        health ??= GetComponent<Health>();

        health.OnTakeDamage += OnTakeDamage;
    }

    bool isTakingDamage;
    private void OnTakeDamage(DamageInfo info)
    {
        RpcTakeDamage(info.healthTaken, info.hitPoint, info.source.GetComponent<NetworkObject>());
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RpcTakeDamage(float damage, Vector3 point, NetworkId sourceId)
    {        
        health.TakeDamage(new DamageInfo { healthTaken = damage, hitPoint = point, source = Runner.TryGetNetworkedBehaviourFromNetworkedObjectRef<MultiplayerCombatAgent>(sourceId).agent});
    }
}
