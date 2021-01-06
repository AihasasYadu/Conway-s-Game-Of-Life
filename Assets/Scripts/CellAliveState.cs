using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAliveState : CellStates
{
    protected override void CheckForNextState()
    {
        if (adjacents < 2 || adjacents > 3)
        {
            nextState = dead;
        }
        else if (adjacents >= 2 && adjacents <= 3)
        {
            nextState = alive;
        }
    }
}
