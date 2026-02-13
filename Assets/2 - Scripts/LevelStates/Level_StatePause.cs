using UnityEngine;

public class Level_StatePause : Level_State_Base
{
    public Level_StatePause(LevelManager levelManager) : base(levelManager) { }

    public override void EnterState()
    {
        #if UNITY_EDITOR
            Debug.Log($"[Level - StateMachine] Level enter in Pause State");
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
            Debug.Log($"[Level - StateMachine] Level exit Pause State");
        #endif
    }
}
