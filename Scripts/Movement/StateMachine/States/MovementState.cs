using System.Collections.Generic;
using System.Linq;

public class MovementBaseState : State
{
    public List<StateTransition> Transitions { get; set; } = new List<StateTransition>();
    public bool IsOverridesMainMovement { get; protected set; }

    public StateTransition GetAvailableTransition()
    {
        return Transitions.FirstOrDefault(x => x.CanDoTransition);
    }

    public virtual bool CanReplaceWith(MovementBaseState otherState)
    {
        return true;
    }
}