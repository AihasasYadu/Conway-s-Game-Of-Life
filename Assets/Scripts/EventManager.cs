using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoSingletonGeneric<EventManager>
{
    public delegate void Executable();
    public static event Executable GenChange;
    public static event Executable ResetGame;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GenChange();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }
}
