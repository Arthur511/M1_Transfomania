using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour
{
    public Vector2Int CurrentPosition { get; set; }
    public Vector2Int StartPosition { get; set; }
    public bool IsAIMoving { get; set; }
    public Vector3 TargetPosition { get; set; }
    [SerializeField] float _speed;

    [SerializeField] Vector2Int _startPosition;
    protected Vector2Int[] _neighborDirection = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };
    public Vector2Int[] GetNeighborDirection() {  return _neighborDirection; }


    #region Animation
    [Header("Animation")]
    public AIAnimator Anim => _anim;
    private AIAnimator _anim;
    #endregion

    protected void Awake()
    {
        _anim = GetComponent<AIAnimator>();
        StateMachine = new NPC_StateManager(new NPC_State_Wait(this));
    }


    protected void Update()
    {
#if UNITY_EDITOR
        Debug.Log($"Current AI state : {StateMachine.CurrentState.ToString()}");
#endif

        StateMachine.CurrentState.UpdateState();
    }

    protected void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdateState();

    }



    public NPC_StateManager StateMachine { get; private set; }
    public List<Vector2Int> PathToFollow { get; set; } = new List<Vector2Int>();
    public int PathIndex { get; set; } = 0;



    public void Initialize(Vector2Int instPositionMap, Vector3 instPositionWorld)
    {
        transform.position = MainGame.Instance.LevelManager.Map[instPositionMap.x, instPositionMap.y].transform.position;
        CurrentPosition = instPositionMap;

        StartPosition = instPositionMap;
    }

    public void MoveCharacter(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), _speed * Time.deltaTime);
    }


    // We use children
    public virtual Vector2Int FindBestCase()
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

    public void FacePos(Vector3 target)
    {
        Vector3 dir = new Vector3(target.x - transform.position.x, 0f, target.z - transform.position.z);
        if (dir.sqrMagnitude > 0.001f * 0.001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }


    public void Die()
    {
        MainGame.Instance.LevelManager.Ennemies.Remove(this);
        Destroy(this.gameObject);
    }
}
