using UnityEngine;

public class Level_State_AITurn : Level_State_Base
{
    public Level_State_AITurn(LevelManager levelManager) : base(levelManager) { }

    public override void EnterState()
    {
        #if UNITY_EDITOR
            Debug.Log($"[Level - StateMachine] Level enter in AI Turn State");
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
            Debug.Log($"[Level - StateMachine] Level exit AI Turn State");
        #endif
    }
}
