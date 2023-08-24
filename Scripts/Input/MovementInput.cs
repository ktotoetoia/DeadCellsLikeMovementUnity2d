using UnityEngine;

public class MovementInput : MonoBehaviour
{
    private MovementStateMachine _stateMachine;
    private MovementBaseState _mainMoveState;
    private MovementBaseState _jumpState;
    private MovementBaseState _rollState;
    private MovementBaseState _crouchState;
    private ICanMove _canMove;
    private ICanJump _canJump;
    private ICanRoll _canRoll;
    private ICanCrouch _canCrouch;
    private float _horizontal = 1;

    private void Start()
    {
        TryGetComponentOrLog(out _canMove);
        TryGetComponentOrLog(out _canJump);
        TryGetComponentOrLog(out _canRoll);
        TryGetComponentOrLog(out _canCrouch);

        _mainMoveState = new MainMoveState(_canMove, () => Input.GetAxisRaw(Axis.Horizontal));
        _jumpState = new JumpState(_canJump);
        _rollState = new RollState(_canRoll, () => _horizontal);
        _crouchState = new CrouchState(_canCrouch, () => Input.GetKey(KeyCode.S));
        _stateMachine = new MovementStateMachine(_mainMoveState);
    }

    private void Update()
    {
        float a = Input.GetAxisRaw(Axis.Horizontal);

        _horizontal = a != 0 ? a : _horizontal;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.CurrentMovement = _jumpState;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _stateMachine.CurrentMovement = _crouchState;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _stateMachine.CurrentMovement = _rollState;
        }

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void TryGetComponentOrLog<T>(out T component)
    {
        if (!TryGetComponent(out component))
        {
            Debug.LogWarning("No component of type: " + typeof(T));
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle()
        {
            fontSize = 50,
        };

        style.normal.textColor = Color.white;

        GUI.Label(new Rect(Vector2.zero,Vector2.one* 100),new GUIContent(_stateMachine.CurrentMovement?.ToString()),style);
    }
}