using System;
using UnityEngine;

public class Level_State_WaitAnim : Level_State_Base
{
    private readonly Func<bool> _isDone;
    Action _onComplete;

    public Level_State_WaitAnim(LevelManager levelManager, Func<bool> isDone, Action onComplete) : base(levelManager)
    {
        _isDone = isDone;
        _onComplete = onComplete;
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (_isDone != null && _isDone())
            _onComplete.Invoke();
    }

    public override void FixedUpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
