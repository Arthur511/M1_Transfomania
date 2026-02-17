using UnityEngine;

public class ChildNPC : MonoBehaviour
{


    Vector2Int _startPosition;
    Vector2Int _currentPosition;

    Vector2Int[] _neighborDirection = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindBestCase()
    {

        Case[,] map = MainGame.Instance.LevelManager.Map;
        int lessDistance = int.MaxValue;
        Vector2Int bestPosition = new Vector2Int();
        foreach (var dir in _neighborDirection)
        {
            var neighbor = MainGame.Instance.PlayerController.PlayerPosition + dir;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject. != null)
        {
        }*/
    }

}
