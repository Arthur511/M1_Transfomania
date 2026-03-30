using System.Collections.Generic;
using UnityEngine;

public class NPC_State_GoBack : NPC_State_Base
{
    public NPC_State_GoBack(BaseNPC npc) : base(npc) {}


    public override void EnterState()
    {
        _npc.Anim.PlayWalk();
        _npc.PathToFollow = BuildPathToStart(_npc);
        _npc.PathIndex = 0;
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        if (!_npc.IsAIMoving) return;

        if (_npc.PathIndex >= _npc.PathToFollow.Count)
        {
            FinishMovement();
            return;
        }

        Vector2Int targetCell = _npc.PathToFollow[_npc.PathIndex];
        Vector3 targetPos = MainGame.Instance.LevelManager.Map[targetCell.x, targetCell.y].transform.position;

        _npc.FacePos(targetPos);
        _npc.MoveCharacter(targetPos);

        Vector3 npcPos = _npc.transform.position;
        float dist = (new Vector3(npcPos.x - targetPos.x, 0f, npcPos.z - targetPos.z)).sqrMagnitude;

        if (dist < 0.1f * 0.1f)
        {
            _npc.transform.position = targetPos;
            _npc.CurrentPosition = targetCell;
            _npc.PathIndex++;

            if (_npc.PathIndex >= _npc.PathToFollow.Count)
            {
                FinishMovement();
            }
        }
    }

    public override void ExitState()
    {

    }

    private void FinishMovement()
    {
        _npc.IsAIMoving = false;
        MainGame.Instance.LevelManager.Count_AIFinishToMove += 1;
        _npc.StateMachine.SwitchState(new NPC_State_Wait(_npc));
    }


    private List<Vector2Int> BuildPathToStart(BaseNPC child)
    {
        Vector2Int startPos = child.CurrentPosition;
        Vector2Int targetPos = child.StartPosition;

        if (startPos == targetPos)
            return new List<Vector2Int>();

        Case[,] map = MainGame.Instance.LevelManager.Map;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(startPos);
        cameFrom[startPos] = startPos;
        bool targetFound = false;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == targetPos)
            {
                targetFound = true;
                break;
            }

            foreach (var dir in child.GetNeighborDirection())
            {
                Vector2Int neighbor = current + dir;

                if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= map.GetLength(0) || neighbor.y >= map.GetLength(1))
                    continue;

                if (map[neighbor.x, neighbor.y] != null && !cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        List<Vector2Int> path = new List<Vector2Int>();
        if (targetFound)
        {
            Vector2Int current = targetPos;
            while (current != startPos)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();
        }
        return path;
    }


}
