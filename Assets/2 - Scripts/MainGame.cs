using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    public PlayerController PlayerController => _playerController;
    public LevelManager LevelManager => _levelManager;
    public CameraFollow CameraFollow => _cameraFollow;
    public LayerMask BoxLayer => _boxLayer; 
    public Button HideButton => _hideButton.GetComponent<Button>();

    [Header("References")]
    [SerializeField] CameraFollow _cameraFollow;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelManager _levelManager;
    [Header("Variables")]
    [SerializeField] LayerMask _boxLayer;
    [SerializeField] GameObject _hideButton;

    //bool _isPlayerMoving = false;
    //bool _isCameraMoving = false;
    //Vector3 _targetPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _cameraFollow.gameObject.transform.position = _playerController.transform.position + (_cameraFollow.LevelCenter.position - _playerController.transform.position) / 2 + _cameraFollow.Offset;
        _levelManager.CanPlayerMoveTo();
    }
    // Update is called once per frame
    void Update()
    {
        //if (Level_State.PlayerTurn)
        //OnClick();

    }

    private void FixedUpdate()
    {
        /*if (_isPlayerMoving)
        {

            _playerController.MoveCharacter(_targetPosition);
            if ((_playerController.transform.position - new Vector3(0, 1, 0) - _targetPosition).sqrMagnitude < 0.01f)
            {
                _isPlayerMoving = false;
                _levelManager.CanPlayerMoveTo();
            }
        }

        if (_isCameraMoving)
        {
            _cameraFollow.SetCurrentOffset(_playerController);
            _cameraFollow.CameraMovement(_targetPosition);
            if ((_cameraFollow.transform.position - _targetPosition + _cameraFollow.Offset).sqrMagnitude < 0.01f)
                _isCameraMoving = false;
        }*/
    }

    /*private void OnClick()
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

                        _levelManager.DistanceFromPlayer = _levelManager.CalculateDistanceFromCase(_playerController);
                        _levelManager.DebugDistanceMap(_levelManager.CalculateDistanceFromCase(_playerController));

                        _levelManager.ClearMatOnCases();
                    }

                }
            }
        }
    }

    public void HidePlayer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerController.IsHiding = !_playerController.IsHiding;
            _hideButton.GetComponentInChildren<TextMeshProUGUI>().text = _playerController.IsHiding ? "Unhide" : "Hide";
        }
    }*/
    public void HidePlayer()
    {
        _playerController.IsHiding = !_playerController.IsHiding;
        _hideButton.GetComponentInChildren<TextMeshProUGUI>().text = _playerController.IsHiding ? "Unhide" : "Hide";
        HideButton.interactable = false;
        LevelManager.NextTurn();
    }

}