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
        IsOverridesMainMovement = false;

        if (!_jumped)
        {
            _jumped = _canJump.TryJump();

            IsOverridesMainMovement = true;
        }
    }
}