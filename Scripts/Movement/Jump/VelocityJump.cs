using UnityEngine;

public class VelocityJump : MovementComponent, ICanJump
{
    [SerializeField] private float _jumpForce;

    public bool TryJump()
    {
        bool jumped = CanJump();

        if (jumped)
        {
            Jump();
        }

        return jumped;
    }

    public void Jump()
    {
        _slider.ResetNormal();

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    public bool CanJump()
    {
        return _slider.IsOnSurface;
    }
}