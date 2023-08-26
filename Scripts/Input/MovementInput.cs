using UnityEngine;

public class MovementStateMachineInstantiator : MonoBehaviour
{
    private MovementStateMachine _stateMachine;
    private MovementBaseState _mainMoveState;
    private MovementBaseState _jumpState;
    private MovementBaseState _rollState;
    private MovementBaseState _crouchState;
    private float _horizontal = 1;

    private void Start()
    {
        _mainMoveState = new MainMoveState(GetComponent<ICanMove>(), () => Input.GetAxisRaw(Axis.Horizontal));
        _jumpState = new JumpState(GetComponent<ICanJump>());
        _rollState = new RollState(GetComponent<ICanRoll>(), () => _horizontal);
        _crouchState = new CrouchState(GetComponent<ICanCrouch>(), () => Input.GetKey(KeyCode.S));
        _stateMachine = new MovementStateMachine(_mainMoveState);

        _stateMachine.AddAnyStateTransition(_jumpState, () => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W));
        _stateMachine.AddAnyStateTransition(_rollState, () => Input.GetKeyDown(KeyCode.LeftShift));
        _stateMachine.AddAnyStateTransition(_crouchState, () => Input.GetKeyDown(KeyCode.S));
    }

    private void Update()
    {
        float a = Input.GetAxisRaw(Axis.Horizontal);

        _horizontal = a != 0 ? a : _horizontal;

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
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