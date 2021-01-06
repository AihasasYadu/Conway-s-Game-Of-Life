using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CellStates : MonoBehaviour
{
    protected Button cell;
    protected int adjacents;
    protected CellStates nextState;
    protected CellAliveState alive;
    protected CellDeadState dead;

    public CellStates Enter(int adjCells)
    {
        adjacents = adjCells;
        CheckForNextState();
        return nextState;
    }

    protected void Awake()
    {
        cell = GetComponent<Button>();
        alive = GetComponent<CellAliveState>();
        dead = GetComponent<CellDeadState>();
    }

    protected void Start()
    {
        nextState = this;
    }

    protected virtual void CheckForNextState() { }
}
