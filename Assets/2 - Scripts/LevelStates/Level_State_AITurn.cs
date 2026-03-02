using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Level_State_AITurn : Level_State_Base
{
    public Level_State_AITurn(LevelManager levelManager) : base(levelManager) { }

    MainGame main;

    public override void EnterState()
    {
        main = MainGame.Instance;
        foreach (ChildNPC child in main.LevelManager.Children)
        {
            child.CurrentPosition = child.FindBestCase();
            child.TargetPosition = main.LevelManager.Map[child.CurrentPosition.x, child.CurrentPosition.y].transform.position;
            
            Debug.Log($"npc : {child.CurrentPosition}, player : {MainGame.Instance.PlayerController.PlayerPosition}");
            if (child.CurrentPosition == MainGame.Instance.PlayerController.PlayerPosition)
            {
                SceneManager.LoadScene("Scene_Arthur2");
            }

            child.IsAIMoving = true;
        }
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

        if (HasAllChildrenMoved())
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
        foreach (ChildNPC npc in main.LevelManager.Children)
        {
            if (npc.IsAIMoving)
                return false;
        }
        return true;
    }

    #endregion

}
