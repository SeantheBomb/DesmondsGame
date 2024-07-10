using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJoinRoomSpawnPrefab : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{

    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private bool AssignToPlayer = true;
    [SerializeField] private bool MasterClientOnly = false;
    [SerializeField] private bool SpawnAtPosition = false;
    private static Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();


    //void OnEnable()
    //{
    //    if (MasterClientOnly) MultiplayerGameManager.OnNewMasterClient += OnNewMasterClient;
    //}


    //void OnDisable()
    //{
    //    if (MasterClientOnly) MultiplayerGameManager.OnNewMasterClient -= OnNewMasterClient;
    //}


    public void PlayerJoined(PlayerRef player)
    {
        if (MasterClientOnly && Runner.IsSharedModeMasterClient == false)
            return;
        if (player == Runner.LocalPlayer)
        {
            //Vector3 spawnPosition = new Vector3((player.RawEncoded % Runner.Config.Simulation.PlayerCount) * 3, 1, 0);
            NetworkObject networkPlayerObject = Runner.Spawn(_playerPrefab, SpawnAtPosition ? transform.position : Vector3.zero, Quaternion.identity, AssignToPlayer ? player : null);
            if (AssignToPlayer) _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            Runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public NetworkObject GetPlayerObject(PlayerRef player)
    {
        return _spawnedCharacters[player];
    }

    //private void OnNewMasterClient(Photon.Realtime.Player player)
    //{
    //    if (player.IsLocal)
    //    {
    //        PlayerRef p = PlayerRef.FromIndex(player.ActorNumber);
    //        NetworkObject networkPlayerObject = Runner.Spawn(_playerPrefab, SpawnAtPosition ? transform.position : Vector3.zero, Quaternion.identity, AssignToPlayer ? p : null);
    //        if (AssignToPlayer) _spawnedCharacters.Add(p, networkPlayerObject);
    //    }
    //}


}