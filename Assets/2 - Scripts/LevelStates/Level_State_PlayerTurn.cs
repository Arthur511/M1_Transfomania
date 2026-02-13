using UnityEngine;

public class Level_State_PlayerTurn : Level_State_Base
{
    public Level_State_PlayerTurn(LevelManager levelManager) : base(levelManager) { }

    public override void EnterState()
    {
        #if UNITY_EDITOR
            Debug.Log($"[Level - StateMachine] Level enter in Player Turn State");
        #endif
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override void ExitState()
    {
        #if UNITY_EDITOR
            Debug.Log($"[Level - StateMachine] Level exit Player Turn State");
        #endif
    }
}
