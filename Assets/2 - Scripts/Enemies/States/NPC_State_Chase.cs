using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_State_Chase : NPC_State_Base
{
    public NPC_State_Chase(BaseNPC npc) : base(npc) { }


    public override void EnterState()
    {
        _npc.CurrentPosition = _npc.FindBestCase();
        _npc.TargetPosition = MainGame.Instance.LevelManager.Map[_npc.CurrentPosition.x, _npc.CurrentPosition.y].transform.position;
        
        if (_npc.CurrentPosition == MainGame.Instance.PlayerController.PlayerPosition)
        {
            _npc.FacePos(_npc.TargetPosition);
            _npc.Anim.PlayPickingUp();

            MainGame.Instance.LevelManager.StateMachine.SwitchState(new Level_State_WaitAnim(MainGame.Instance.LevelManager,
                () =>
                {
                    var anim = MainGame.Instance.PlayerController.Anim;
                    return anim == null || anim.IsAnimationComplete();
                },
                () => EnemyAttackInteraction()
            ));

            return;
        }

        _npc.FacePos(_npc.TargetPosition);
        _npc.Anim.PlayWalk();
        _npc.IsAIMoving = true;
    }

    public override void UpdateState()
    {
        
    }

    public override void FixedUpdateState()
    {
        if (!_npc.IsAIMoving)
            return;

        _npc.FacePos(_npc.TargetPosition);
        _npc.MoveCharacter(_npc.TargetPosition);

        Vector3 npcPos = _npc.transform.position;
        float dist = (new Vector3(npcPos.x - _npc.TargetPosition.x, 0f, npcPos.z - _npc.TargetPosition.z)).sqrMagnitude;

        if (dist < 0.1f * 0.1f)
        {
            _npc.IsAIMoving = false;
            MainGame.Instance.LevelManager.Count_AIFinishToMove += 1;
            _npc.StateMachine.SwitchState(new NPC_State_Wait(_npc));
        }
    }

    public override void ExitState()
    {
        
    }



    private void EnemyAttackInteraction()
    {
        if (MainGame.Instance.PlayerController.LollipopCount > 0)
        {
            MainGame.Instance.PlayerController.ChangeLolipopCount(false);
            _npc.Die();
            MainGame.Instance.LevelManager.StateMachine.SwitchState(new Level_State_PlayerTurn(MainGame.Instance.LevelManager));
            return;
        }

        MainGame.Instance.PlayerController.Die();
    }
}
