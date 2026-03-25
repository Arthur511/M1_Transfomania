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
            SceneManager.LoadScene("Scene_Arthur2");
        }

        if (MainGame.Instance.PlayerController.IsHiding)
        {
            /*
            _npc.PathToFollow = BuildPathToStart(child);
            _npc.PathIndex = 0;
            */
        }

        _npc.IsAIMoving = true;
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdateState()
    {
        /*
        if (MainGame.Instance.PlayerController.IsHiding)
        {

            foreach (ChildNPC child in main.LevelManager.Children)
            {
                if (!child.IsAIMoving) continue;
                if (child.PathIndex >= child.PathToFollow.Count)
                {
                    child.IsAIMoving = false;
                    continue;
                }
                Vector2Int targetCell = child.PathToFollow[child.PathIndex];
                Vector3 targetPos = MainGame.Instance.LevelManager.Map[targetCell.x, targetCell.y].transform.position;

                child.MoveCharacter(targetPos);
                if ((child.gameObject.transform.position - targetPos).sqrMagnitude < 0.1f)
                {
                    child.transform.position = targetPos;
                    child.CurrentPosition = targetCell;
                    child.PathIndex++;

                    if (child.PathIndex >= child.PathToFollow.Count)
                    {
                        child.IsAIMoving = false;
                    }
                }
            }
        }
        */



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
        throw new System.NotImplementedException();
    }
}
