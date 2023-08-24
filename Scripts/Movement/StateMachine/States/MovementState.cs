public class MovementBaseState : State
{
    public bool IsOverridesMainMovement { get; protected set; }

    public virtual bool CanReplaceWith(MovementBaseState otherState)
    {
        return true;
    }
}