using UnityEngine;
using UnityEngine.InputSystem;

public class Level_State_AITurn : Level_State_Base
{
    public Level_State_AITurn(LevelManager levelManager) : base(levelManager) { }

    MainGame main;

    public override void EnterState()
    {
        main = MainGame.Instance;

#if UNITY_EDITOR
        Debug.Log($"[Level - StateMachine] Level enter in AI Turn State");
        #endif
    }

    public override void UpdateState()
    {
        #region DEBUG
        if (Keyboard.current.qKey.IsPressed())
        {
            main.LevelManager.NextTurn();
        }
        #endregion

    }

    public override void FixedUpdateState()
    {
        #if UNITY_EDITOR
            Debug.Log("AI Turn !!!");
        #endif
    }

    public override void ExitState()
    {
        #if UNITY_EDITOR
            Debug.Log($"[Level - StateMachine] Level exit AI Turn State");
        #endif
    }
}
