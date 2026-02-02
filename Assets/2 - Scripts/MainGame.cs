using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGame : MonoBehaviour
{
    [SerializeField] LayerMask _boxLayer;
    [SerializeField] GameObject _player;
    [Range(5f, 10f)]
    [SerializeField] float _speedPlayerMove;
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
            MoveCharacter();
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


    private void MoveCharacter()
    {
        _player.transform.position = Vector3.Lerp(_player.transform.position, _targetPosition, _speedPlayerMove * Time.deltaTime);
        if ((_player.transform.position - _targetPosition).sqrMagnitude < 0.01f)
            _isPlayerMoving = false;
    }


}
