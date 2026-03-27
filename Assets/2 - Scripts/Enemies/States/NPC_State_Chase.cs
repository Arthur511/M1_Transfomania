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
            if (MainGame.Instance.PlayerController.LollipopCount > 0)
            {
                MainGame.Instance.PlayerController.ChangeLolipopCount(false);
                _npc.Die();
                return;
            }

            MainGame.Instance.PlayerController.Die();
            return;
        }

        _npc.IsAIMoving = true;
    }

    public override void UpdateState()
    {
        
    }

    public override void FixedUpdateState()
    {
        if (_npc.IsAIMoving)
        {
            _npc.MoveCharacter(_npc.TargetPosition);
            if ((_npc.gameObject.transform.position - _npc.TargetPosition).sqrMagnitude < 0.1f * 0.1f)
            {
                _npc.IsAIMoving = false;
                MainGame.Instance.LevelManager.Count_AIFinishToMove += 1;
                _npc.StateMachine.SwitchState(new NPC_State_Wait(_npc));
            }
        }
    }

    public override void ExitState()
    {
        
    }
}
