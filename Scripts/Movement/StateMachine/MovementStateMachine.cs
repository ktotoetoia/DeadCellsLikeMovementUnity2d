using System;

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
            if (!_currentMovement?.CanReplaceWith(value)??false)
            {
                return;
            }

            _currentMovement?.OnExit();

            _currentMovement = value;

            value.OnEnter();
        }
    }

    public MovementStateMachine(MovementBaseState mainMovement)
    {
        MainMovement = mainMovement;
    }

    public void Update()
    {
        GetStateToUpdate(x => x.OnUpdate());
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

        if (CurrentMovement != null)
        {
            stateAction(CurrentMovement);
        }
    }
}