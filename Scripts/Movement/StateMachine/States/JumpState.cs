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
        _jumped = !_canJump.CanJump();
    }

    public override void OnFixedUpdate()
    {
        if (_jumped)
        {
            return;
        }

        _jumped = _canJump.TryJump();
    }
}