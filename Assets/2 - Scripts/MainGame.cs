using System;
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
    public New_CameraFollow CameraFollow => _cameraFollow;
    public LayerMask BoxLayer => _boxLayer; 
    public Button HideButton => _hideButton.GetComponent<Button>();

    [Header("References")]
    [SerializeField] New_CameraFollow _cameraFollow;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] AnimatorManager _animatorManager;
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
        //_cameraFollow.gameObject.transform.position = _playerController.transform.position + (_cameraFollow.LevelCenter.position - _playerController.transform.position) / 2 + _cameraFollow.Offset;
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
       
    }
    public void HidePlayer()
    {
        _playerController.IsHiding = !_playerController.IsHiding;
        string hideTrigger = _playerController.IsHiding ? "Unhiding" : "Hiding";
        _animatorManager.PlayAnimation(_playerController.PlayerAnimator, hideTrigger);
        _hideButton.GetComponentInChildren<TextMeshProUGUI>().text = _playerController.IsHiding ? "Unhide" : "Hide";
        HideButton.interactable = false;
        LevelManager.NextTurn();
    }

}