using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Range(6f, 8f)]
    [SerializeField] float _speedCamera;
    [SerializeField] Vector3 _offset;

    public void CameraMovement(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos + _offset, _speedCamera*Time.deltaTime);
    }
}
