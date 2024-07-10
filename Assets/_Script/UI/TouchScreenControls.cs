using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScreenControls : MonoBehaviour
{

    public TouchScreenButton up, down, left, right, attack;

    public PlayerMovement playerMovement;
    public CombatMeleeAttack meleeAttack;




    // Update is called once per frame
    void Update()
    {
        if (up.isPressed)
        {
            playerMovement.MoveUp();
        }
        if (down.isPressed)
        {
            playerMovement.MoveDown();
        }
        if (left.isPressed)
        {
            playerMovement.MoveLeft();
        }
        if(right.isPressed)
        {
            playerMovement.MoveRight();
        }
        if(attack.isPressed)
        {
            meleeAttack.StartAttack();
        }
    }
}
