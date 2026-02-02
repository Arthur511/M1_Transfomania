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

    public void MoveCharacter(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), _speedPlayerMove * Time.deltaTime);
    }

    public void SetBoxPosition()
    {

    }

}
