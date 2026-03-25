using System.Collections.Generic;
using UnityEngine;

public class NPC_State_GoBack : NPC_State_Base
{
    public NPC_State_GoBack(BaseNPC npc) : base(npc) {}


    public override void EnterState()
    {
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

        _npc.MoveCharacter(targetPos);

        if ((_npc.gameObject.transform.position - targetPos).sqrMagnitude < 0.1f * 0.1f)
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


    //public int[,] CalculateDistanceFromCase(Vector2Int startCase)
    //{
    //    Case[,] map = MainGame.Instance.LevelManager.Map;

    //    int[,] values = new int[map.GetLength(0), map.GetLength(1)];
    //    Queue<Vector2Int> posCases = new Queue<Vector2Int>();
    //    bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)];

    //    posCases.Enqueue(startCase);
    //    visited[startCase.x, startCase.y] = true;

    //    while (posCases.Count > 0)
    //    {
    //        var currentPos = posCases.Dequeue();
    //        foreach (var dir in _npc.GetNeighborDirection())
    //        {
    //            var neighbor = currentPos + dir;

    //            if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= map.GetLength(0) || neighbor.y >= map.GetLength(1))
    //            { continue; }

    //            if (visited[neighbor.x, neighbor.y])
    //            { continue; }

    //            if (map[neighbor.x, neighbor.y] != null)
    //            {
    //                values[neighbor.x, neighbor.y] = values[currentPos.x, currentPos.y] + 1;
    //                visited[neighbor.x, neighbor.y] = true;
    //                posCases.Enqueue(neighbor);
    //            }
    //        }
    //    }
    //    return values;
    //}


    //private List<Vector2Int> BuildPathToStart(BaseNPC child)
    //{
    //    var map = MainGame.Instance.LevelManager.Map;

    //    int[,] dist = CalculateDistanceFromCase(child.StartPosition);
    //    var path = new List<Vector2Int>();
    //    Vector2Int current = child.CurrentPosition;

    //    while (current != child.StartPosition)
    //    {
    //        Vector2Int best = current;
    //        int bestDist = dist[current.x, current.y];

    //        foreach (var dir in _npc.GetNeighborDirection())
    //        {
    //            var neighbor = current + dir;

    //            if (neighbor.x < 0 || neighbor.y < 0 ||
    //                neighbor.x >= map.GetLength(0) || neighbor.y >= map.GetLength(1))
    //                continue;

    //            if (map[neighbor.x, neighbor.y] != null && dist[neighbor.x, neighbor.y] < bestDist)
    //            {
    //                bestDist = dist[neighbor.x, neighbor.y];
    //                best = neighbor;
    //            }
    //        }

    //        if (best == current) break;

    //        path.Add(best);
    //        current = best;
    //    }

    //    return path;
    //}



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
