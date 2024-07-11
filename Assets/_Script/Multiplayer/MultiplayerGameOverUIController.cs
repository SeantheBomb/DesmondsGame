using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerGameOverUIController : MonoBehaviour
{

    private void OnEnable()
    {
        foreach (var b in GetComponentsInChildren<Button>())
            b.interactable = true;
    }


    public void Respawn()
    {
        MultiplayerSpawnController.LocalPlayer.RpcSpawn();
    }
}
