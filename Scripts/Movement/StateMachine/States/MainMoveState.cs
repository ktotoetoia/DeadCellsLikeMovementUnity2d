using System;
using UnityEngine;

public class MainMoveState : MovementBaseState
{
    private Func<float> _getDirectionFunc;
    private ICanMove _canMove;
    private float _direction;

    public MainMoveState(ICanMove canMove, Func<float> getDirectionFunc)
    {
        _canMove = canMove;
        _getDirectionFunc = getDirectionFunc;
    }

    public override void OnUpdate()
    {
        _direction = _getDirectionFunc();
    }

    public override void OnFixedUpdate()
    {
        _canMove.Move(_direction);
    }
}