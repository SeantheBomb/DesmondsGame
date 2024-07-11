using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSpawnController : NetworkBehaviour
{

    public static MultiplayerSpawnController LocalPlayer
    {
        get; protected set;
    }

    [SerializeField] Health health;
    [SerializeField] GameObject gameOverScreen;

    [Networked]
    bool isAlive
    {
        get;set;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        health ??= GetComponent<Health>();
        health.OnDeath += OnDeath;

        yield return new WaitUntil(() => Object != null);
        if (Object.IsProxy)
            yield break;

        LocalPlayer = this;

        gameOverScreen = Instantiate(gameOverScreen);
        gameOverScreen.SetActive(false);
        RpcSpawn();
    }

    private void OnDeath(DamageInfo info)
    {
        isAlive = false;
        gameObject.SetActive(false);
        if (Object.IsProxy)
            return;
        gameOverScreen.SetActive(true);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RpcSpawn()
    {
        gameObject.SetActive(true);
        isAlive = true;

        if (Object.IsProxy)
            return;
        gameOverScreen.SetActive(false);
        transform.position = MultiplayerSpawnPoint.GetSpawnPoint();
        health.Heal(health.maxHealth - health.health);
    }

}
