using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GM gameMaster, GameStateMachine stateMachine, string thisStateName) : base(gameMaster, stateMachine, thisStateName)
    {
    }
}
