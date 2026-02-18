using UnityEngine;

public class ChildNPC : MonoBehaviour
{

    public Vector2Int CurrentPosition { get; set; }
    public bool IsAIMoving { get; set; }
    public Vector3 TargetPosition { get; set; }

    [SerializeField] float _speedChild;

    [SerializeField] Vector2Int _startPosition;
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
        transform.position = MainGame.Instance.LevelManager.Map[_startPosition.x, _startPosition.y].transform.position;
        CurrentPosition = _startPosition;
    }

    public void MoveCharacter(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), _speedChild * Time.deltaTime);
    }
    public Vector2Int FindBestCase()
    {

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
                    { continue; }
                if (lessDistance > MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y])
                {
                    lessDistance = MainGame.Instance.LevelManager.DistanceFromPlayer[neighbor.x, neighbor.y];
                    bestPosition = neighbor;
                }
            }
        }
        return bestPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject. != null)
        {
        }*/
    }

}
