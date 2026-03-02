using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
                main.LevelManager.NextTurn();
                _isPlayerMoving = false;
                _levelManager.CanPlayerMoveTo();
            }
        }

        if (_isCameraMoving)
        {
            main.CameraFollow.SetCurrentOffset(main.PlayerController);
            main.CameraFollow.CameraMovement(_targetPosition);
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

                 }
             }
         }
         #endregion*/

        #region MOBILE_INTERACTION

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;
                Camera.main.backgroundColor = Color.red;
                RaycastHit hit;
                Ray ray = main.CameraFollow.gameObject.GetComponent<Camera>().ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    Camera.main.backgroundColor = Color.blue;
                    Debug.Log("Raycast a touché : " + hit.collider.name);
                    if (((1 << hit.collider.gameObject.layer) & main.BoxLayer.value) != 0)
                    {
                        Camera.main.backgroundColor = Color.green;
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
