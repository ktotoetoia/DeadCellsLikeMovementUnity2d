using System;

public class StateTransition
{
    public Func<bool> Condition { get; set; }
    public MovementBaseState From { get; set; }
    public MovementBaseState To { get; set; }
    public bool CanDoTransition 
    {
        get
        {
            return Condition();
        }      
    }

    public StateTransition(MovementBaseState from, MovementBaseState to) : this(from, to, () => true)
    {

    }

    public StateTransition(MovementBaseState from, MovementBaseState to, Func<bool> condition)
    {
        from?.Transitions.Add(this);

        From = from;
        To = to;
        Condition = condition;
    }
}