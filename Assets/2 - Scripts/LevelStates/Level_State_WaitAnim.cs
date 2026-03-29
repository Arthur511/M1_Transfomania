using System;
using UnityEngine;

public class Level_State_WaitAnim : Level_State_Base
{
    private readonly Func<bool> _isDone;
    private readonly Level_State_Base _nextState;

    public Level_State_WaitAnim(LevelManager levelManager, Func<bool> isDone, Level_State_Base nextState): base(levelManager)
    {
        _isDone = isDone;
        _nextState = nextState;
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (_isDone != null && _isDone())
            _levelManager.StateMachine.SwitchState(_nextState);
    }

    public override void FixedUpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
