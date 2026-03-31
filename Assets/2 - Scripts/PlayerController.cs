using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator PlayerAnimator;
    public Vector2Int PlayerPosition { get { return _playerPosition; } set { _playerPosition = value; } }
    public bool IsHiding { get; set; }
    //public GameObject HideButton => _hideButton;
    public int BoxPositionX => _boxPositionX;
    public int BoxPositionZ => _boxPositionZ;

    [UnityEngine.Range(5f, 10f)]
    [SerializeField] float _speedPlayerMove;
    [SerializeField] LayerMask _lollipopLayer;
    int _boxPositionX;
    int _boxPositionZ;
    Vector2Int _playerPosition;
    //[SerializeField] GameObject _hideButton;
    public int LollipopCount => _lollipopCount;
    int _lollipopCount = 0;


    #region Animation
    [Header("Animation")]
    public PlayerAnimator Anim => _anim;
    private PlayerAnimator _anim;
    #endregion

    private void Awake()
    {
        _anim = GetComponent<PlayerAnimator>();

    }


    public void Update()
    {

    }

    public void MoveCharacter(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), _speedPlayerMove * Time.deltaTime);
    }

    public bool CanMoveAtPosition(Vector2Int clickPos)
    {
        if (clickPos == _playerPosition + new Vector2Int(1, 0)
            || clickPos == _playerPosition + new Vector2Int(-1, 0)
            || clickPos == _playerPosition + new Vector2Int(0, 1)
            || clickPos == _playerPosition + new Vector2Int(0, -1))
            return true;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == _lollipopLayer.value)
        {
            Destroy(other.gameObject);
            ChangeLolipopCount(true);
            SoundManager.PlaySound(SoundType.LOLIPOP, 1f);
        }
    }

    /*
    public void HidePlayer(bool hide)
    {
        MainGame mainGame = MainGame.Instance;
        IsHiding = hide;
        string hideTrigger = IsHiding ? "Unhiding" : "Hiding";
        //mainGame.AnimatorManager.PlayAnimation(PlayerAnimator, hideTrigger);
        mainGame.HideButton.GetComponentInChildren<TextMeshProUGUI>().text = IsHiding ? "Unhide" : "Hide";
        mainGame.HideButton.interactable = false;
    }
    */


    public void ChangeLolipopCount(bool increase)
    {
        _lollipopCount += increase? 1 : -1;
        MainGame.Instance.UIManager.UpdateLolipopText(_lollipopCount);
    }

    public void ChangeLolipopCount(int newValue)
    {
        _lollipopCount = newValue;
        MainGame.Instance.UIManager.UpdateLolipopText(_lollipopCount);
    }


    public void Die()
    {
        MainGame.Instance.LevelManager.OnPlayerDie();
        SoundManager.PlaySound(SoundType.CHILDGRAB, 0.2f);

    }
}
