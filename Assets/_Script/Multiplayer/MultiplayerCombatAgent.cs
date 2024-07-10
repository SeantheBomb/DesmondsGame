using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCombatAgent : NetworkBehaviour
{

    [SerializeField] CombatAgent agent;


    Dictionary<string, IAttack> attacks;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        agent ??= GetComponent<CombatAgent>();
        PopulateAttacks();

        yield return new WaitUntil(()=>Object !=  null);

        agent.team = Object.InputAuthority.AsIndex;
    }


    void PopulateAttacks()
    {
        attacks = new Dictionary<string, IAttack>();
        foreach(var a in GetComponentsInChildren<IAttack>())
        {
            attacks.Add(a.GetType().Name, a);
            a.OnAttack += OnAttack;
            Debug.Log($"MultiplayerCombatAgent: {name} registers attack {a.GetType().Name}", gameObject);
        }
    }

    private void OnAttack(string type)
    {
        RpcAttack(type);
        Debug.Log($"MultiplayerCombatAgent: {name} attack {type} triggers RPC", gameObject);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.Proxies)]
    private void RpcAttack(string type)
    {
        attacks[type].StartAttack(true);
        Debug.Log($"MultiplayerCombatAgent: {name} attack {type} triggered by RPC", gameObject);
    }
}
