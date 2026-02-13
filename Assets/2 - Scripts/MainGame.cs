using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    public PlayerController PlayerController => _playerController;

    [Header("References")]
    [SerializeField] CameraFollow _cameraFollow;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelManager _levelManager;
    [Header("Variable")]
    [SerializeField] LayerMask _boxLayer;
    //[SerializeField] GameObject _player;

    bool _isPlayerMoving = false;
    bool _isCameraMoving = false;
    Vector3 _targetPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _cameraFollow.gameObject.transform.position = (_cameraFollow.LevelCenter.position - _playerController.transform.position)/2 + _cameraFollow.Offset;
        _levelManager.CanMove();
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
            if ((_playerController.transform.position - new Vector3(0, 1, 0) - _targetPosition).sqrMagnitude < 0.01f)
            {
                Debug.Log("Fin mouvement");
                _isPlayerMoving = false;
                _levelManager.CanMove();
            }
        }

        if (_isCameraMoving)
        {
            _cameraFollow.SetCurrentOffset(_playerController);
            _cameraFollow.CameraMovement(_targetPosition);
            if ((_cameraFollow.transform.position - _targetPosition + _cameraFollow.Offset).sqrMagnitude < 0.01f)
                _isCameraMoving = false;
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
                    Vector2Int clickPos;
                    if (hit.transform.GetComponent<Case>() == null)
                        return;
                    clickPos = hit.transform.GetComponent<Case>().CasePosition;
                    if (_playerController.CanMoveAtPosition(clickPos))
                    {
                        _isPlayerMoving = true;
                        _isCameraMoving = true;
                        _targetPosition = hit.transform.position;
                        _playerController.PlayerPosition = clickPos;
                        _levelManager.ClearMatOnCases();
                    }

                }
            }
        }
    }
}