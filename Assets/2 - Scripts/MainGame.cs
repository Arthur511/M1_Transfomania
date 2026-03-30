using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    public PlayerController PlayerController => _playerController;
    public LevelManager LevelManager => _levelManager;
    public New_CameraFollow CameraFollow => _cameraFollow;
    public UIManager UIManager => _uiManager;

    public LayerMask BoxLayer => _boxLayer; 
    public Button HideButton => _hideButton.GetComponent<Button>();

    [Header("References")]
    [SerializeField] New_CameraFollow _cameraFollow;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] UIManager _uiManager;
    [Header("Variables")]
    [SerializeField] LayerMask _boxLayer;
    [SerializeField] GameObject _hideButton;


    [SerializeField] private Level[] Levels;
    private int _currentLevelIndex = 0;


    void Awake()
    {
        Instance = this;
        _levelManager.Initialize();
        SetLevel(0);
        _uiManager.UpdateHideButton(false);
    }


    void Update()
    {


    }

    private void FixedUpdate()
    {
       
    }



    public void ToggleHidePlayer()
    {
        HidePlayer(!_playerController.IsHiding);
        _levelManager.OnPlayerHideToggled();
        //LevelManager.NextTurn();
    }


    public void HidePlayer(bool hide)
    {
        _playerController.IsHiding = hide;
        _playerController.Anim?.SetIsHiding(hide);

        _uiManager.UpdateHideButton(hide);
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
        Level nextLevel = Levels[_currentLevelIndex];

        LevelManager.LoadNewLevel(nextLevel);
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