using System.Collections;
using System.Collections.Generic;
// using UnityEngine;

public class GameState
{
    public GameState(GM gameMaster, GameStateMachine stateMachine, string thisStateName)
    {
        GameMaster = gameMaster;
        StateMachine = stateMachine;
        StateName = thisStateName;
    }

    protected GM GameMaster { get; private set; }
    protected GameStateMachine StateMachine { get; private set; }
    protected string StateName;

    public virtual void EnterState()
    {
        // what happens when the state is entered
        GameMaster.currentStateName = StateName; 
    }
    public virtual void ExitState()
    {
        // what happens when the state is exited
    }
    public virtual void GameLogic()
    {
        // the game logic
    }
}
