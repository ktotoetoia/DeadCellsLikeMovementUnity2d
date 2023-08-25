using System;
using System.Collections.Generic;
using System.Linq;

public class MovementStateMachine
{
    private MovementBaseState _currentMovement;

    public MovementBaseState MainMovement { get; set; }
    public MovementBaseState CurrentMovement
    {
        get
        {
            return _currentMovement;
        }
        set
        {
            if(!_currentMovement?.CanReplaceWith(value) ?? false)
            {
                return;
            }

            _currentMovement?.OnExit();

            _currentMovement = value;

            value.OnEnter();
        }
    }
    
    public List<StateTransition> AnyStateTransitions { get; set; } = new List<StateTransition>();

    public MovementStateMachine(MovementBaseState mainMovement)
    {
        MainMovement = mainMovement;
    }

    public void Update()
    {
        TryDoTransition(_currentMovement?.GetAvailableTransition());
        TryDoTransition(AnyStateTransitions.FirstOrDefault(x => x.CanDoTransition));
        GetStateToUpdate(x => x.OnUpdate());
    }

    private void TryDoTransition(StateTransition transition)
    {
        if(transition != default)
        {
            CurrentMovement = transition.To;
        }
    }

    public void FixedUpdate()
    {
        GetStateToUpdate(x => x.OnFixedUpdate());
    }

    private void GetStateToUpdate(Action<MovementBaseState> stateAction)
    {
        if (!(CurrentMovement?.IsOverridesMainMovement ?? false))
        {
            stateAction(MainMovement);
        }

        if (CurrentMovement != default)
        {
            stateAction(CurrentMovement);
        }
    }

    public void AddAnyStateTransition(MovementBaseState to, Func<bool> condition)
    {
        AnyStateTransitions.Add(new StateTransition(default, to, condition));
    }
}