using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDeadState : CellStates
{
    protected override void CheckForNextState()
    {
        if (adjacents == 3)
        {
            nextState = alive;
        }
        else
        {
            nextState = dead;
        }
    }
}
