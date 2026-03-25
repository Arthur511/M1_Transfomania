using UnityEngine;

public class NPC_StateManager
{
    public NPC_State_Base CurrentState { get; private set; }

    public NPC_StateManager(NPC_State_Base initState)
    {
        CurrentState = initState;
        CurrentState.EnterState();
    }


    /*
    public void Initialize(Level_State_Base initState)
    {
        CurrentState = initState;
        CurrentState.EnterState();
    }
    */


    public void SwitchState(NPC_State_Base newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
