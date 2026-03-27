using UnityEngine;

public class New_CameraFollow : MonoBehaviour
{
    [Header("Targets")]
    public Transform _player;
    private Vector3 _gridCenter;
    private bool _isGridCenterSet = false;

    [Header("Movement")]
    public Vector3 Offset = new Vector3(0, 10, -10);
    private Vector3 _speed;

    [Header("Zoom")]
    public float MinZoom = 10f;
    public float MaxZoom = 25f;
    public float ZoomLimiter = 50f;

    [Header("Quality of transform")]
    public float Smoothness = 0.125f;


    void LateUpdate()
    {
        if (_player == null || !_isGridCenterSet)
        {
            TryGetTargets();
            return;
        }

        ProcessMovement();
        ProcessZoom();
    }

    private void TryGetTargets()
    {
        if (MainGame.Instance != null)
        {
            if (MainGame.Instance.PlayerController != null)
                _player = MainGame.Instance.PlayerController.transform;

            if (MainGame.Instance.LevelManager != null && MainGame.Instance.LevelManager.Map != null)
            {
                _gridCenter = MainGame.Instance.LevelManager.GridCenter;
                _isGridCenterSet = true;
            }
        }
    }

    public void ResetTarget()
    {
        _isGridCenterSet = false;
    }


    private void ProcessMovement()
    {
        Vector3 middlePoint = (_player.position + _gridCenter) / 2f;
        Vector3 pos = middlePoint + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref _speed, Smoothness);
    }


    private void ProcessZoom()
    {
        float distance = Vector3.Distance(_player.position, _gridCenter);
        float newZoom = Mathf.Lerp(MinZoom, MaxZoom, distance / ZoomLimiter);

        Offset.y = newZoom;

        Vector3 midpoint = ( _player.position + _gridCenter) / 2f;
        transform.LookAt(midpoint);
    }



    private void OnDrawGizmosSelected()
    {
        if (_player == null || !_isGridCenterSet) return;

        Gizmos.color = new Color(1f, 0f, 0f, 1f);

        Vector3 midpoint = (_player.position + _gridCenter) / 2f;

        Gizmos.DrawSphere(midpoint, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_gridCenter, 1);
    }
}

