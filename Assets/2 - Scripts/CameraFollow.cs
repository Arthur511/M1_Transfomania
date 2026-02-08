using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Range(1f, 3f)]
    [SerializeField] float _speedCamera;
    [SerializeField] Vector3 _offset;
    public Vector3 Offset => _offset;
    [SerializeField] Transform _levelCenter;
    public void SetCurrentOffset(PlayerController player)
    {
        Vector3 distance = _levelCenter.position - player.transform.position;
        _offset.x = distance.x/2;
        _offset.z = distance.z/2  - 15;
    }

    public void CameraMovement(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos + _offset, _speedCamera*Time.deltaTime);
    }
}
