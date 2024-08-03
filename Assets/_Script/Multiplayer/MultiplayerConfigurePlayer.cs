using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerConfigurePlayer : NetworkBehaviour
{

    PlayerMovement movement;
    CombatMeleeAttack attack;
    Camera camera;

    [SerializeField] GameObject[] enableLocal, enableProxy;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<CombatMeleeAttack>();
        camera = GetComponentInChildren<Camera>();
        //canvas = GetComponentInChildren<Canvas>();

        yield return new WaitUntil(()=>Object != null);
        
        if (Object.IsProxy)
        {
            movement.enabled = false;
            attack.GetComponent<CombatAgent>().type = AgentType.Proxy;
            camera.gameObject.SetActive(false);
        }
        EnableComponents();
    }

    public override void Spawned()
    {
        base.Spawned();
        
    }

    void EnableComponents()
    {
        foreach (GameObject go in enableLocal)
            go.SetActive(Object.IsProxy == false);
        foreach (GameObject go in enableProxy)
            go.SetActive(Object.IsProxy);
    }


}
