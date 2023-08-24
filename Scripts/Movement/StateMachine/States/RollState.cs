using System;

public class RollState : MovementBaseState
{
    private Func<float> _getRollDirection;
    private ICanRoll _canRoll;
    private bool _rolled;

    public RollState(ICanRoll canRoll, Func<float> getRollDirection)
    {
        _canRoll = canRoll;
        _getRollDirection = getRollDirection;
        IsOverridesMainMovement = true;
    }

    public override void OnEnter()
    {
        _rolled = false;
    }

    public override void OnUpdate()
    {
        if (!_rolled)
        {
            _canRoll.Roll(_getRollDirection());
            _rolled = true;
        }

        IsOverridesMainMovement = _canRoll.IsRolling;
    }

    public override void OnExit()
    {
        _canRoll.StopRoll();
    }

    public override bool CanReplaceWith(MovementBaseState otherState)
    {
        return _canRoll.CanStopRolling;
    }
}