using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerConfigurePlayer : NetworkBehaviour
{

    PlayerMovement movement;
    CombatMeleeAttack attack;
    Camera camera;
    Canvas canvas;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<CombatMeleeAttack>();
        camera = GetComponentInChildren<Camera>();
        canvas = GetComponentInChildren<Canvas>();

        yield return new WaitUntil(()=>Object != null);
        
        if (Object.IsProxy)
        {
            movement.enabled = false;
            attack.isPlayer = false;
            camera.gameObject.SetActive(false);
            canvas.gameObject.SetActive(false);
        }
    }

    public override void Spawned()
    {
        base.Spawned();
        
    }


}
