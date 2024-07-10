using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerHealth : MonoBehaviour
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
        bool isTakingDamage = true;
        RpcTakeDamage(info);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RpcTakeDamage(DamageInfo info)
    {
        if (isTakingDamage)
            return;

        isTakingDamage = false;
        
        health.TakeDamage(info);
    }
}
