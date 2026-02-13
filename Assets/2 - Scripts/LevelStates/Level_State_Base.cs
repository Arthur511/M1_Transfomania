using UnityEngine;

public abstract class Level_State_Base
{
    protected LevelManager _levelManager;

    public Level_State_Base(LevelManager levelManager)
    {
        this._levelManager = levelManager;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}
