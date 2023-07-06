using System.Collections;
using System.Collections.Generic;
// using UnityEngine;

public class GameStateMachine
{
    public GameState currentState { get; private set; }

    public void InitializeGameStateMachine(GameState initialState)
    {
        currentState = initialState;
        currentState.EnterState();
    }
    public void ChangeState(GameState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
