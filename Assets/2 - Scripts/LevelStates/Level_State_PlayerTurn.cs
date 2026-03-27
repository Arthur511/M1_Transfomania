using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static CaseTypeData;
using UnityEngine.SceneManagement;

public class Level_State_PlayerTurn : Level_State_Base
{
    public Level_State_PlayerTurn(LevelManager levelManager) : base(levelManager) { }

    bool _isPlayerMoving = false;
    bool _isCameraMoving = false;
    Vector3 _targetPosition;
    MainGame main;

    public override void EnterState()
    {
        main = MainGame.Instance;
        main.HideButton.interactable = true;
#if UNITY_EDITOR
        Debug.Log($"[Level - StateMachine] Level enter in Player Turn State");
#endif
    }

    public override void UpdateState()
    {
        if (!_isPlayerMoving)
            OnClick();
    }

    public override void FixedUpdateState()
    {
        if (_isPlayerMoving)
        {

            main.PlayerController.MoveCharacter(_targetPosition);
            if ((main.PlayerController.transform.position - new Vector3(0, 1, 0) - _targetPosition).sqrMagnitude < 0.01f)
            {
                if (_isPlayerMoving)
                {
                    main.PlayerController.MoveCharacter(_targetPosition);
                    if ((main.PlayerController.transform.position - new Vector3(0, 1, 0) - _targetPosition).sqrMagnitude < 0.01f)
                    {
                        _isPlayerMoving = false;

                        Case currentCase = main.LevelManager.Map[main.PlayerController.PlayerPosition.x, main.PlayerController.PlayerPosition.y];

                        if (currentCase != null && currentCase.CaseTypeData.CaseType == TypeOfCases.Door)
                        {
                            main.SetLevel();
                        }
                        else
                        {
                            main.LevelManager.NextTurn();
                            _levelManager.CanPlayerMoveTo();
                        }
                    }
                }


                //main.LevelManager.NextTurn();
                //_isPlayerMoving = false;
                //_levelManager.CanPlayerMoveTo();

            }
        }

        if (_isCameraMoving)
        {
            //main.CameraFollow.SetCurrentOffset(main.PlayerController);
            //main.CameraFollow.CameraMovement(_targetPosition);
            if ((main.CameraFollow.transform.position - _targetPosition + main.CameraFollow.Offset).sqrMagnitude < 0.01f)
                _isCameraMoving = false;
        }
    }

    public override void ExitState()
    {
#if UNITY_EDITOR
        Debug.Log($"[Level - StateMachine] Level exit Player Turn State");
#endif
    }

    #region methods
    private void OnClick()
    {
        MainGame main = MainGame.Instance;
        /* #region PC_INTERACTION
         if (Mouse.current.leftButton.wasPressedThisFrame)
         {
             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
             if (Physics.Raycast(ray, out hit))
             {
                 if (1 << hit.collider.gameObject.layer == main.BoxLayer.value)
                 {
                     Vector2Int clickPos;
                     if (hit.transform.GetComponent<Case>() == null)
                         return;
                     clickPos = hit.transform.GetComponent<Case>().CasePosition;
                     if (main.PlayerController.CanMoveAtPosition(clickPos))
                     {
                         main.HideButton.interactable = false;
                         _isPlayerMoving = true;
                         _isCameraMoving = true;
                         _targetPosition = hit.transform.position;
                         main.PlayerController.PlayerPosition = clickPos;

                         _levelManager.DistanceFromPlayer = _levelManager.CalculateDistanceFromCase(main.PlayerController);
                         _levelManager.DebugDistanceMap(_levelManager.CalculateDistanceFromCase(main.PlayerController));

                         _levelManager.ClearMatOnCases();
                     }
                     if (hit.transform.GetComponent<Case>().CaseTypeData.CaseType == TypeOfCases.Door)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                     
                 }
             }
         }
         #endregion*/

        #region MOBILE_INTERACTION

        //Debug.Log("CLLLLIIIIICCCCCCKKKKKK !!!");
        if (Touchscreen.current != null)
        {
            //Debug.Log("Touch !!!");
            //Touch touch = Input.GetTouch(0);
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Debug.Log("Touch start !!!");

                /*if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;*/
                RaycastHit hit;
                Ray ray = main.CameraFollow.gameObject.GetComponent<Camera>().ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Touch something !!!");
                    if (((1 << hit.collider.gameObject.layer) & main.BoxLayer.value) != 0)
                    {
                        Debug.Log("Touch cell !!!");

                        Vector2Int clickPos;
                        Case touchCase = hit.transform.GetComponent<Case>();
                        if (hit.transform.GetComponent<Case>() == null)
                            return;


                        clickPos = hit.transform.GetComponent<Case>().CasePosition;
                        if (main.PlayerController.CanMoveAtPosition(clickPos))
                        {
                            main.HideButton.interactable = false;
                            _isPlayerMoving = true;
                            _isCameraMoving = true;
                            _targetPosition = hit.transform.position;
                            main.PlayerController.PlayerPosition = clickPos;

                            _levelManager.DistanceFromPlayer = _levelManager.CalculateDistanceFromPlayer(main.PlayerController);
                            _levelManager.DebugDistanceMap(_levelManager.CalculateDistanceFromPlayer(main.PlayerController));

                            _levelManager.ClearMatOnCases();
                        }

                    }
                }
            }
        }
        #endregion
    }

    /*public void HidePlayer()
    {
        MainGame main = MainGame.Instance;
        main.PlayerController.IsHiding = !main.PlayerController.IsHiding;
        main.PlayerController.HideButton.GetComponentInChildren<TextMeshProUGUI>().text = main.PlayerController.IsHiding ? "Unhide" : "Hide";
        main.LevelManager.NextTurn();
    }*/

    #endregion


}
