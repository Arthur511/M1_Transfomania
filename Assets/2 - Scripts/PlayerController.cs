using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    [Range(5f, 10f)]
    [SerializeField] float _speedPlayerMove;

    int _boxPositionX;
    int _boxPositionZ;
    public int BoxPositionX => _boxPositionX;
    public int BoxPositionZ => _boxPositionZ;

    private Vector2Int _playerPosition;
    public Vector2Int PlayerPosition { get { return _playerPosition; } set { _playerPosition = value; }}


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Checkcase();
        }
    }



    public void MoveCharacter(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), _speedPlayerMove * Time.deltaTime);
    }


    public bool CanMove(Vector2Int clickPos)
    {
        if (clickPos == _playerPosition + new Vector2Int(1, 0)
            || clickPos == _playerPosition + new Vector2Int(-1, 0)
            || clickPos == _playerPosition + new Vector2Int(0, 1)
            || clickPos == _playerPosition + new Vector2Int(0, -1))
            return true;
        return false;
    }


    public void SetBoxPosition()
    {

    }

}
