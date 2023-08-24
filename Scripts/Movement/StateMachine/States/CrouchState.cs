using System;

public class CrouchState : MovementBaseState
{
    private ICanCrouch _canCrouch;
    private Func<bool> _checkCrouching;

    public CrouchState(ICanCrouch canCrouch, Func<bool> isCrouching)
    {
        _canCrouch = canCrouch;
        _checkCrouching = isCrouching;
    }

    public override void OnUpdate()
    {
        IsOverridesMainMovement = _canCrouch.IsCrouching || _canCrouch.IsFalling;
        _canCrouch.IsCrouching = _checkCrouching();
    }

    public override void OnExit()
    {
        _canCrouch.IsCrouching = false;
    }

    public override bool CanReplaceWith(MovementBaseState otherState)
    {
        return !_canCrouch.IsFalling;
    }
}