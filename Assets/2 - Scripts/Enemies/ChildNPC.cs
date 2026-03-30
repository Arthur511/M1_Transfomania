using System.Collections.Generic;
using UnityEngine;

public class ChildNPC : BaseNPC
{
    //public List<Vector2Int> PathToFollow { get; set; } = new List<Vector2Int>();
    //public int PathIndex { get; set; } = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject. != null)
        {
        }*/

    }

    /*
    public override Vector2Int FindBestCase()
    {
        if (MainGame.Instance.LevelManager.DistanceFromPlayer[CurrentPosition.x, CurrentPosition.y] == 0)
            return CurrentPosition;

        Case[,] map = MainGame.Instance.LevelManager.Map;
        int lessDistance = int.MaxValue;
        Vector2Int bestPosition = new Vector2Int();
        foreach (var dir in _neighborDirection)
        {
            var neighbor = CurrentPosition + dir;

            if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= map.GetLength(0) || neighbor.y >= map.GetLength(1))
            { continue; }

            if (map[neighbor.x, neighbor.y] != null)
            {

                if (MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y] == 0)
                {
                    bestPosition = neighbor;
                    return bestPosition;
                }
                if (lessDistance > MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y])
                {
                    lessDistance = MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y];
                    bestPosition = neighbor;
                }
            }
        }
        return bestPosition;
    }
    */
}