using UnityEngine;

public class Level_StateManager
{
    public Level_State_Base CurrentState { get; private set; }

    /*
        private Level_StatePause _statePause = new Level_StatePause(); 
        private Level_State_PlayerTurn _statePlayerTurn = new();
        private Level_State_AITurn _stateAITurn = new();
    */

    public void Initialize(Level_State_Base initState)
    {
        CurrentState = initState;
        CurrentState.EnterState();
    }


    public void SwitchState(Level_State_Base newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
