using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    public PlayerController PlayerController => _playerController;
    public LevelManager LevelManager => _levelManager;
    public AnimatorManager AnimatorManager => _animatorManager;
    public New_CameraFollow CameraFollow => _cameraFollow;
    public UIManager UIManager => _uiManager;

    public LayerMask BoxLayer => _boxLayer; 
    public Button HideButton => _hideButton.GetComponent<Button>();

    [Header("References")]
    [SerializeField] New_CameraFollow _cameraFollow;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] AnimatorManager _animatorManager;
    [SerializeField] UIManager _uiManager;
    [Header("Variables")]
    [SerializeField] LayerMask _boxLayer;
    [SerializeField] GameObject _hideButton;

    //bool _isPlayerMoving = false;
    //bool _isCameraMoving = false;
    //Vector3 _targetPosition;


    [SerializeField] private TextAsset[] Levels;
    private int _currentLevelIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        _levelManager.Initialize();
        SetLevel(0);

    }
    private void Start()
    {
        //_cameraFollow.gameObject.transform.position = _playerController.transform.position + (_cameraFollow.LevelCenter.position - _playerController.transform.position) / 2 + _cameraFollow.Offset;
        //_levelManager.CanPlayerMoveTo();
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



    public void ToggleHidePlayer()
    {
        HidePlayer(!_playerController.IsHiding);
        LevelManager.NextTurn();
    }


    public void HidePlayer(bool hide)
    {
        _playerController.IsHiding = hide;
        string hideTrigger = _playerController.IsHiding ? "Unhiding" : "Hiding";
        _animatorManager.PlayAnimation(_playerController.PlayerAnimator, hideTrigger);
        _hideButton.GetComponentInChildren<TextMeshProUGUI>().text = hideTrigger;
        HideButton.interactable = false;
    }


    /// <summary>
    /// Load next level
    /// </summary>
    public void SetLevel()
    {
        _currentLevelIndex++; 
        if (_currentLevelIndex >= Levels.Length)
        {
            return;
        }
        TextAsset nextMap = Levels[_currentLevelIndex];

        LevelManager.LoadNewLevel(nextMap);
    }

    /// <summary>
    /// Load level by is index
    /// </summary>
    public void SetLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < Levels.Length)
        {
            _currentLevelIndex = levelIndex;
            LevelManager.LoadNewLevel(Levels[_currentLevelIndex]);
        }
        else
        {
            Debug.LogWarning("This level not exist !");
        }
    }


    public void OnPlayerDie()
    {
        SetLevel(_currentLevelIndex);
    }
}