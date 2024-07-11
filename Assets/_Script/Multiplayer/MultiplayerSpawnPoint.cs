using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplayerSpawnPoint : MonoBehaviour
{

    public static List<MultiplayerSpawnPoint> spawnPoints
    {
        get; protected set;
    }


    private void OnEnable()
    {
        spawnPoints ??= new List<MultiplayerSpawnPoint>();
        spawnPoints.Add(this);
    }

    private void OnDisable()
    {
        spawnPoints.Remove(this);
    }

    public static Vector3 GetSpawnPoint(Func<MultiplayerSpawnPoint, bool> query = null)
    {
        List<MultiplayerSpawnPoint> select;

        if(query == null)
        {
            select = spawnPoints;
        }
        else
        {
            select = spawnPoints.Where(query).ToList();
        }

        int index = UnityEngine.Random.Range(0, select.Count);
        return select[index].transform.position;

    }

}
