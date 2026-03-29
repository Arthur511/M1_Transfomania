using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Level_State_AITurn : Level_State_Base
{
    public Level_State_AITurn(LevelManager levelManager) : base(levelManager) { }

    MainGame main;

    //Vector2Int[] _neighborDirection = new Vector2Int[]
    //{
    //    new Vector2Int(1, 0),
    //    new Vector2Int(-1, 0),
    //    new Vector2Int(0, 1),
    //    new Vector2Int(0, -1)
    //};

    private static readonly int _idlePlayerHash = Animator.StringToHash("Idle_Robotron");

    public override void EnterState()
    {
        main = MainGame.Instance;

        for (int i = main.LevelManager.Ennemies.Count - 1; i >= 0; i--)
        {
            BaseNPC enemy = main.LevelManager.Ennemies[i];

            if (enemy == null) continue;

            /*
            if (MainGame.Instance.PlayerController.IsHiding)
                enemy.StateMachine.SwitchState(new NPC_State_GoBack(enemy));
            else
                enemy.StateMachine.SwitchState(new NPC_State_Chase(enemy));
            */

            enemy.StateMachine.SwitchState(main.PlayerController.IsHiding ? new NPC_State_GoBack(enemy) : new NPC_State_Chase(enemy));


            enemy.IsAIMoving = true;
        }


        #if UNITY_EDITOR
        Debug.Log($"[Level - StateMachine] Level enter in AI Turn State");
        #endif
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        if (_levelManager.Count_AIFinishToMove < _levelManager.Ennemies.Count)
            return;

        main.LevelManager.Count_AIFinishToMove = 0;

        bool wasHiding = main.PlayerController.IsHiding;
        main.HidePlayer(false);


        if (wasHiding)
        {
            _levelManager.StateMachine.SwitchState(new Level_State_WaitAnim(_levelManager,
                () =>
                {
                    var anim = MainGame.Instance.PlayerController.Anim;
                    return anim == null || anim.IsPlayingState(_idlePlayerHash);
                },
                new Level_State_PlayerTurn(_levelManager) // utiliser NexTurn?
            ));
        }
        else
        {
            main.LevelManager.NextTurn();
        }
    }

    public override void ExitState()
    {
        #if UNITY_EDITOR
        Debug.Log($"[Level - StateMachine] Level exit AI Turn State");
        #endif
    }
}
