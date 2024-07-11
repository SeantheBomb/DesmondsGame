using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]
public class ConnectionManager : MonoBehaviour
{

    NetworkRunner runner;


    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        runner = GetComponent<NetworkRunner>();
        runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            MatchmakingMode = Fusion.Photon.Realtime.MatchmakingMode.FillRoom,
            PlayerCount = 4
        });

        SceneManager.activeSceneChanged += OnSceneLoad;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene arg0, Scene arg1)
    {
        runner.Shutdown();
    }
}
