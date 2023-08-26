public class JumpState : MovementBaseState
{
    private ICanJump _canJump;
    private bool _jumped;

    public JumpState(ICanJump canJump)
    {
        _canJump = canJump;
    }

    public override void OnEnter()
    {
        _jumped = false;
    }

    public override void OnFixedUpdate()
    {
        if (_jumped)
        {
            return;
        }

        if (_canJump.CanJump())
        {
            _canJump.Jump();
        }

        _jumped = true;
    }
}