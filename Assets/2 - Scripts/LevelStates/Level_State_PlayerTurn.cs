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

            Vector3 playerPos = main.PlayerController.transform.position;
            float dist = (new Vector3(playerPos.x - _targetPosition.x, 0f, playerPos.z - _targetPosition.z)).sqrMagnitude;

            if (dist < 0.1f * 0.1f)
            {
                _isPlayerMoving = false;
                main.PlayerController.Anim?.PlayIdle();

                BaseNPC enemyAtPos = main.LevelManager.Ennemies.Find(e => e != null && e.CurrentPosition == main.PlayerController.PlayerPosition);
                if (enemyAtPos != null)
                {
                    enemyAtPos.FacePos(main.PlayerController.transform.position);
                    enemyAtPos.Anim.PlayPickingUp();

                    _levelManager.StateMachine.SwitchState(new Level_State_WaitAnim(
                        _levelManager,
                        () =>
                        {
                            var anim = main.PlayerController.Anim;
                            return anim == null || anim.IsAnimationComplete();
                        },
                        () => enemyAtPos.AttackInteraction()
                    ));
                    return;
                }

                if (enemyAtPos != null)
                {
                    // Le NPC regarde le joueur et joue l'animation de ramassage
                    enemyAtPos.FacePos(main.PlayerController.transform.position);
                    enemyAtPos.Anim.PlayPickingUp();

                    // On attend la fin de l'animation puis on résout l'interaction
                    _levelManager.StateMachine.SwitchState(new Level_State_WaitAnim(
                        _levelManager,
                        () =>
                        {
                            var anim = main.PlayerController.Anim;
                            return anim == null || anim.IsAnimationComplete();
                        },
                        () => enemyAtPos.AttackInteraction()
                    ));
                    return;
                }
                // -----

                Case currentCase = main.LevelManager.Map[main.PlayerController.PlayerPosition.x, main.PlayerController.PlayerPosition.y];

                if (currentCase != null && currentCase.CaseTypeData.CaseType == TypeOfCases.Door)
                {
                    SoundManager.PlaySound(SoundType.SUCESS, 5f);
                    main.SetLevel();
                }
                else
                {
                    main.LevelManager.NextTurn();
                    _levelManager.CanPlayerMoveTo();
                }
            }
        }

        if (_isCameraMoving)
        {

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


    private void OnClick()
    {
        if (Touchscreen.current == null) 
            return;
        if (Touchscreen.current.primaryTouch.phase.ReadValue() != UnityEngine.InputSystem.TouchPhase.Began)
            return;

        RaycastHit hit;
        Ray ray = main.CameraFollow.gameObject.GetComponent<Camera>().ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        if (!Physics.Raycast(ray, out hit))
            return;
        if (((1 << hit.collider.gameObject.layer) & main.BoxLayer.value) == 0)
            return;

        Case touchCase = hit.transform.GetComponent<Case>();
        if (hit.transform.GetComponent<Case>() == null)
            return;

        Vector2Int clickPos = touchCase.CasePosition;
        if (!main.PlayerController.CanMoveAtPosition(clickPos))
            return;

        main.HideButton.interactable = false;
        _isPlayerMoving = true;
        _isCameraMoving = true;
        _targetPosition = hit.transform.position;

        main.PlayerController.PlayerPosition = clickPos;
        main.PlayerController.Anim?.PlayWalk();

        FacePos(main.PlayerController.transform, _targetPosition);

        _levelManager.DistanceFromPlayer = _levelManager.CalculateDistanceFromPlayer(main.PlayerController);
        _levelManager.ClearMatOnCases();
    }


    private static void FacePos(Transform thingToLook, Vector3 target)
    {
        Vector3 dir = new Vector3(target.x - thingToLook.position.x, 0f, target.z - thingToLook.position.z);
        if (dir.sqrMagnitude > 0.001f)
            thingToLook.rotation = Quaternion.LookRotation(dir);
    }
}
