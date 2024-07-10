using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health) )]
public class OnDeathLoadScene : MonoBehaviour
{

    public string SceneName;
    public float Delay = 3f;

    Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += OnDeath;
    }

    private void OnDeath(DamageInfo info)
    {
        Invoke(nameof(LoadScene), Delay);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
