using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{

    [Range(5f, 10f)]
    [SerializeField] float _speedPlayerMove;


    public void MoveCharacter(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, _speedPlayerMove * Time.deltaTime);
    }

}
