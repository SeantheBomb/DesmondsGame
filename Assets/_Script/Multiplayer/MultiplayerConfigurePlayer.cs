using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerConfigurePlayer : NetworkBehaviour
{

    PlayerMovement movement;
    CombatMeleeAttack attack;
    Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<CombatMeleeAttack>();
        camera = GetComponentInChildren<Camera>();
    }

    public override void Spawned()
    {
        base.Spawned();
        if (Object.IsProxy)
        {
            movement.enabled = false;
            attack.isPlayer = false;
            camera.enabled = false;
        }
    }


}
