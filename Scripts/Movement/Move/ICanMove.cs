using System;

public interface ICanMove
{
    void Move(float direction);
    void MoveWithCustomSpeed(float direction,float speed);
}

public interface ICanJump
{
    bool CanJump();
    void Jump();
}

public interface ICanCrouch
{
    bool IsCrouching { get; set; }
    bool IsFalling { get; set; }
}

public interface ICanRoll
{
    bool IsRolling { get;}
    bool CanStopRolling { get;  }

    public void Roll(float direction);
    void StopRoll();
}