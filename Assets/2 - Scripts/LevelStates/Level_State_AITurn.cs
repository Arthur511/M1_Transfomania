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

    int[,] DistanceFromStartPosition { get; set; }

    bool aaa = false;

    // New npc gestion

    public override void EnterState()
    {
        main = MainGame.Instance;

        // On parcourt la liste à l'envers
        for (int i = main.LevelManager.Ennemies.Count - 1; i >= 0; i--)
        {
            BaseNPC ennemies = main.LevelManager.Ennemies[i];

            if (ennemies == null) continue;

            if (MainGame.Instance.PlayerController.IsHiding)
                ennemies.StateMachine.SwitchState(new NPC_State_GoBack(ennemies));
            else
                ennemies.StateMachine.SwitchState(new NPC_State_Chase(ennemies));

            ennemies.IsAIMoving = true;
        }

        //main = MainGame.Instance;
        //foreach (ChildNPC child in main.LevelManager.Ennemies)
        //{
        //    //child.CurrentPosition = child.FindBestCase();
        //    //child.TargetPosition = main.LevelManager.Map[child.CurrentPosition.x, child.CurrentPosition.y].transform.position;

        //    //Debug.Log($"npc : {child.CurrentPosition}, player : {MainGame.Instance.PlayerController.PlayerPosition}");
        //    //if (child.CurrentPosition == MainGame.Instance.PlayerController.PlayerPosition)
        //    //{
        //    //    SceneManager.LoadScene("Scene_Arthur2");
        //    //}

        //    if (MainGame.Instance.PlayerController.IsHiding)
        //    {
        //        //child.PathToFollow = BuildPathToStart(child);
        //        //child.PathIndex = 0;
        //        child.StateMachine.SwitchState(new NPC_State_GoBack(child));
        //    }
        //    else
        //    {
        //        child.StateMachine.SwitchState(new NPC_State_Chase(child));

        //    }

        //    child.IsAIMoving = true;
        //}
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


        //if (MainGame.Instance.PlayerController.IsHiding)
        //{

        //foreach (ChildNPC child in main.LevelManager.Children)
        //{
        //    if (!child.IsAIMoving) continue;
        //    if (child.PathIndex >= child.PathToFollow.Count)
        //    {
        //        child.IsAIMoving = false;
        //        continue;
        //    }
        //    Vector2Int targetCell = child.PathToFollow[child.PathIndex];
        //    Vector3 targetPos = MainGame.Instance.LevelManager.Map[targetCell.x, targetCell.y].transform.position;

        //    child.MoveCharacter(targetPos);
        //    if ((child.gameObject.transform.position - targetPos).sqrMagnitude < 0.1f)
        //    {
        //        child.transform.position = targetPos;
        //        child.CurrentPosition = targetCell;
        //        child.PathIndex++;

        //        if (child.PathIndex >= child.PathToFollow.Count)
        //        {
        //            child.IsAIMoving = false;
        //        }
        //    }
        //}
        //}
        //else
        //{
        /*
        foreach (ChildNPC child in main.LevelManager.Children)
        {

            if (child.IsAIMoving)
            {
                child.MoveCharacter(child.TargetPosition);
                if ((child.gameObject.transform.position - child.TargetPosition).sqrMagnitude < 0.01f)
                {
                    child.IsAIMoving = false;
                }
            }
        }
        */
        //}


        //if (HasAllChildrenMoved())

        if (_levelManager.Count_AIFinishToMove < _levelManager.Ennemies.Count)
            return;

        main.LevelManager.Count_AIFinishToMove = 0;
        main.HidePlayer(false);
        main.LevelManager.NextTurn();

    }

    public override void ExitState()
    {
#if UNITY_EDITOR
        Debug.Log($"[Level - StateMachine] Level exit AI Turn State");
#endif
    }


    #region METHODS
    /*public void MoveCharacter(ChildNPC child, Vector3 targetPos)
    {
        child.gameObject.transform.position = Vector3.Lerp(child.gameObject.transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), _speedPlayerMove * Time.deltaTime);
    }

    public void FindBestCase(ChildNPC child)
    {
        Case[,] map = MainGame.Instance.LevelManager.Map;
        int lessDistance = int.MaxValue;
        Vector2Int bestPosition = new Vector2Int();
        foreach (var dir in main.LevelManager.NeighborDirection)
        {
            var neighbor = child.gameObject.transform.position + dir;

            if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= map.GetLength(0) || neighbor.y >= map.GetLength(1))
            { continue; }

            if (map[neighbor.x, neighbor.y] != null)
            {
                if (lessDistance > MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y])
                {
                    lessDistance = MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y];
                    bestPosition = neighbor;
                }
            }
        }
    }*/
    bool HasAllChildrenMoved()
    {
        bool hasChildrenMoved = true;
        foreach (ChildNPC npc in main.LevelManager.Ennemies)
        {
            if (npc.IsAIMoving)
                return false;
        }
        return true;
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
    //        foreach (var dir in _neighborDirection)
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

    //private List<Vector2Int> BuildPathToStart(ChildNPC child)
    //{
    //    var map = MainGame.Instance.LevelManager.Map;

    //    int[,] dist = CalculateDistanceFromCase(child.StartPosition);
    //    var path = new List<Vector2Int>();
    //    Vector2Int current = child.CurrentPosition;

    //    while (current != child.StartPosition)
    //    {
    //        Vector2Int best = current;
    //        int bestDist = dist[current.x, current.y];

    //        foreach (var dir in _neighborDirection)
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

    #endregion

}
