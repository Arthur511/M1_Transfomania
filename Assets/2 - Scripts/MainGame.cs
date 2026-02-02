using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraFollow _cameraFollow;
    [SerializeField] PlayerController _playerController;
    
    [Header("Variable")]    
    [SerializeField] LayerMask _boxLayer;
    //[SerializeField] GameObject _player;
    
    bool _isPlayerMoving = false;
    Vector3 _targetPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    private void FixedUpdate()
    {
        if (_isPlayerMoving)
        {
            _playerController.MoveCharacter(_targetPosition);
            _cameraFollow.CameraMovement(_targetPosition);
            if ((transform.position - _targetPosition).sqrMagnitude < 0.01f)
                _isPlayerMoving = false;
        }

    }

    private void OnClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
            {
                if (1 << hit.collider.gameObject.layer == _boxLayer.value)
                {
                    _isPlayerMoving = true;
                    _targetPosition = hit.transform.position;
                }
            }

        }
    }
}
